using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;


using ps_310_BethanysPieShopHRM.App.Services;


namespace ps_310_BethanysPieShopHRM.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<ps_310_BethanysPieShopHRM.App.App>("app");

            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });



            // 04/12/2022 10:22 pm - SSN - [20220412-2221] - dataAPIUrl
            // Was "https://localhost:44340/"
            var dataAPIUrl = builder.Configuration["dataAPIUrl"];
            if (string.IsNullOrEmpty(dataAPIUrl))
            {
                throw new Exception("ps-310-20220412-2024-C - Missing env var dataAPIUrl");
            }


            builder.Services.AddHttpClient<IEmployeeDataService, EmployeeDataService>(client => client.BaseAddress = new Uri(dataAPIUrl));
            builder.Services.AddHttpClient<ICountryDataService, CountryDataService>(client => client.BaseAddress = new Uri(dataAPIUrl));
            builder.Services.AddHttpClient<IJobCategoryDataService, JobCategoryDataService>(client => client.BaseAddress = new Uri(dataAPIUrl));



            await builder.Build().RunAsync();
        }
    }
}
