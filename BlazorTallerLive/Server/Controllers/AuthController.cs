using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;

namespace BlazorTallerLive.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        IConfiguration Configuration;
        public AuthController(IConfiguration conf)
        {
            this.Configuration = conf;
        }
        [HttpGet("login")]
        public IActionResult Login(string user,string pass)
        {
            string tokenGenerated = GenerateToken(user, pass);
            if(!string.IsNullOrEmpty(tokenGenerated))
            {
                return Ok(tokenGenerated);
            }
            else
            {
                return BadRequest("error al generar token");
            }
        }
        private string GenerateToken(string user, string pass, string secret = "")
        {
            Claim[] claims = null;
            string tokenAsString = "";

            if ((user == "usuario" && pass == "usuario") || (user == "usuario" && secret == Configuration["AuthSettings:key"]))
            {
                claims = new[]
                {
                    new Claim("Email","usuario@usuario.com"),
                    //new Claim(ClaimTypes.NameIdentifier,"2577"), TokensJWT
                    new Claim(JwtRegisteredClaimNames.UniqueName,"Rik"),
                    new Claim("User","usuario"),
                    new Claim("Rol","Usuario"),
                    new Claim(ClaimTypes.Role,"Usuario")
                };
                IdentityModelEventSource.ShowPII = true;
                var keybuffer = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["AuthSettings:key"]));
                DateTime expireTime = DateTime.Now.AddSeconds(30);
                var token = new JwtSecurityToken(issuer: Configuration["AuthSettings:Issuer"], audience: Configuration["AuthSettings:Audince"], claims, expires: expireTime, signingCredentials: new SigningCredentials(keybuffer, SecurityAlgorithms.HmacSha256));

                tokenAsString = new JwtSecurityTokenHandler().WriteToken(token);


            }

            return tokenAsString;
        }
    }
}
