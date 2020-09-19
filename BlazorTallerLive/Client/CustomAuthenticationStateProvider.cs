using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text.Json;
using System.Threading.Tasks;

namespace BlazorTallerLive.Client
{
    public class CustomAuthenticationStateProvider : AuthenticationStateProvider
    {
        private readonly IJSRuntime jSRuntime;
        string TokenGeneral = "";
        enum StorageType
        {
            token,expiry
        }
        public CustomAuthenticationStateProvider(IJSRuntime runtime)
        {
            jSRuntime = runtime;
        }
        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            Console.WriteLine($"entro a get autentication");
            var token = await GetTokenAsync();
            Console.WriteLine($"tomo el token que es {token}");
            var identity = string.IsNullOrEmpty(token) ? new ClaimsIdentity() : new ClaimsIdentity(ParseClaimsFromJWT(token), "JWT");
            return new AuthenticationState(new ClaimsPrincipal(identity));
        }
        public async Task SetTokenAsync(string token, string expiry)
        {
            Console.WriteLine($"entro a set token {token}");
            if (token == null)
            {
                Console.WriteLine("token null");
                var a = await jSRuntime.InvokeAsync<object>("RemoveData", StorageType.token);
                Console.WriteLine(a + " eliminado");
                var b = await jSRuntime.InvokeAsync<object>("RemoveData", StorageType.expiry);
                Console.WriteLine(b + " eliminado");
            }
            else
            {
                Console.WriteLine("token valido");
                TokenGeneral = token;
                var x = await jSRuntime.InvokeAsync<object>("SaveData", StorageType.token, token);//, "token",token
                Console.WriteLine(x + " agrega");
                var y = await jSRuntime.InvokeAsync<object>("SaveData", StorageType.expiry, expiry);
                Console.WriteLine(y + " agrega");
            }
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }
        public async Task<string> GetTokenAsync()
        {
            var expiry = await jSRuntime.InvokeAsync<string>("GetData", StorageType.expiry);
            Console.WriteLine($"expiry {expiry}");
            if (expiry != null)
            {
                if (DateTime.Parse(expiry.ToString()) > DateTime.Now)
                {
                    return await jSRuntime.InvokeAsync<string>("GetData", StorageType.token);
                }
                else
                {
                    await SetTokenAsync(null, null);
                }
            }
            return null;
        }
        private IEnumerable<Claim> ParseClaimsFromJWT(string jwt)
        {
            var payload = jwt.Split('.')[1];
            var jsonBytes = ParseBase64Withoutpadding(payload);
            var keyValuesPairs = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonBytes);
            List<Claim> result = new List<Claim>();
            Claim aux;
            foreach (var data in keyValuesPairs)
            {

                
                aux = new Claim(data.Key, data.Value.ToString());

                // }
                result.Add(aux);
            }
            return result;//keyValuesPairs.Select(kv => new Claim(kv.Key,kv.Value.ToString()));
        }
        private byte[] ParseBase64Withoutpadding(string base64)
        {
            switch (base64.Length % 4)
            {
                case 2: base64 += "=="; break;
                case 3: base64 += "="; break;
            }
            return Convert.FromBase64String(base64);
        }
    }
}
