using BlazorTallerLive.Shared;
using Microsoft.AspNetCore.Components;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorTallerLive.Client.Pages
{
    public partial class MiComponente
    {
        public MiComponente()
        {
            InputAttributes = new Dictionary<string, object>()
            {
                {"maxlength","10" },
                {"placeholder","texto desde propiedad" }
            };
        }
        protected override async Task OnInitializedAsync()
        {
            Console.WriteLine("Ha inicializado");
            await GetImage();
        }
        protected override async Task OnParametersSetAsync()
        {
            Console.WriteLine("Se han colocado los parametros");
        }
        [Parameter]
        public string Nombre { get; set; }
        [Parameter]
        public RenderFragment ContenidoUno { get; set; }
        [Parameter]
        public RenderFragment ContenidoDos { get; set; }

        Photo PhotoLocal = new Photo();
        public Dictionary<string,object> InputAttributes { get; set; }

        async Task GetImage()
        {
            var responseWeb = await Client.GetAsync("https://jsonplaceholder.typicode.com/photos/10");
            var data = await responseWeb.Content.ReadAsStringAsync();
            var obj = JsonConvert.DeserializeObject<Photo>(data);
            PhotoLocal = obj;
        }
    }
}
