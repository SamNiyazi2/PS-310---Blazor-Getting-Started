using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
//using ps_310_BethanysPieShopHRM.Server.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;



using ps_310_BethanysPieShopHRM.App.Services;



namespace ps_310_BethanysPieShopHRM.Server
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddRazorPages();
            services.AddServerSideBlazor();


            // 04/12/2022 10:22 pm - SSN - [20220412-2221] - dataAPIUrl
            // Was "https://localhost:44340/"
            var dataAPIUrl = configuration["dataAPIUrl"];
            if (string.IsNullOrEmpty(dataAPIUrl))
            {
                throw new Exception("ps-310-20220412-2024-S - Missing env var dataAPIUrl");
            }

            services.AddHttpClient<IEmployeeDataService, EmployeeDataService>(client => client.BaseAddress = new Uri(dataAPIUrl));
            services.AddHttpClient<ICountryDataService, CountryDataService>(client => client.BaseAddress = new Uri(dataAPIUrl));
            services.AddHttpClient<IJobCategoryDataService, JobCategoryDataService>(client => client.BaseAddress = new Uri(dataAPIUrl));




        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });
        }
    }
}
