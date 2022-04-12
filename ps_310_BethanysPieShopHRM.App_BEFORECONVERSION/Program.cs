using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

//using ps_310_BethanysPieShopHRM.App_BEFORECONVERSION.Services;
using ps_310_BethanysPieShopHRM.App.Services;


using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ps_310_BethanysPieShopHRM.App_BEFORECONVERSION
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("app");


            // var config =               host.Services.GetRequiredService<IConfiguration>();

            //  var configuration = builder.Services.BuildServiceProvider().GetRequiredService<IConfiguration>(); 
            //var ApiBaseAddress = configuration["ApiBaseAddress"];
            var dataAPIUrl = builder.Configuration["dataAPIUrl"];


            // 05/10/2021 04:33 am - SSN - [20210510-0409] - [004] - M03-05 - Demo: Accessing data from the REST API
            // builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
            builder.Services.AddHttpClient<IEmployeeDataService, EmployeeDataService>(client => client.BaseAddress = new Uri(dataAPIUrl));

            builder.Services.AddHttpClient<ICountryDataService, CountryDataService>(client => client.BaseAddress = new Uri(dataAPIUrl));
            builder.Services.AddHttpClient<IJobCategoryDataService, JobCategoryDataService>(client => client.BaseAddress = new Uri(dataAPIUrl));

            await builder.Build().RunAsync();
        }
    }
}
