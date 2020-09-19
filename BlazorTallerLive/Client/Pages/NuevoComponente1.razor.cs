using BlazorTallerLive.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace BlazorTallerLive.Client.Pages
{
    public partial class NuevoComponente1
    {
        void Saludar()
        {
            Console.WriteLine("Hola mundo");
        }
        List<Post> PostsList = new List<Post>();
        async Task GetPost()
        {
            var response = await Client.GetAsync("https://jsonplaceholder.typicode.com/users/1/posts");
            var data = await response.Content.ReadAsStringAsync();
            PostsList = JsonConvert.DeserializeObject<List<Post>>(data);
            Console.WriteLine(PostsList.Count);
        }

    }
}
