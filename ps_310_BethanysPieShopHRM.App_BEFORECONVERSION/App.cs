 
using System.Collections.Generic;
using System.Threading.Tasks;



using System.Reflection;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.AspNetCore.Components.WebAssembly.Services;
using Microsoft.JSInterop;
using Microsoft.AspNetCore.Components;




namespace ps_310_BethanyPieShopHRM.App_BEFORECONVERSION
{
    public partial class App
    {

        [Inject]
        public IJSRuntime jSRuntime { get; set; }

        private List<Assembly> lazyLoadedAssemblies = new List<Assembly>();

        private async Task OnNavigateAsync(NavigationContext args)
        {
            if (args.Path.Contains("employeedetail"))
            {
                var assemblies = await new LazyAssemblyLoader(jSRuntime).LoadAssembliesAsync(new string[] { "BethanysPieShopHRM.ComponentsLibrary.dll" });
                lazyLoadedAssemblies.AddRange(assemblies);
            }
        }

    }
}
