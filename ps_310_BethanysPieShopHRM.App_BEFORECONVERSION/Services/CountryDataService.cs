using ps_310_BethanysPieShopHRM.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

// 05/10/2021 06:10 am - SSN - [20210510-0606] - [002] - M03-10 - Demo: Adding more component

namespace ps_310_BethanysPieShopHRM.App_BEFORECONVERSION.Services
{
    public class CountryDataService : ICountryDataService
    {
        private readonly HttpClient httpClient;

        public CountryDataService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }


        public async Task<IEnumerable<Country>> GetAllCountries()
        {
            return
                await JsonSerializer.DeserializeAsync<IEnumerable<Country>>(
                    await httpClient.GetStreamAsync("/api/country"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        }

        public async Task<Country> GetCountryById(int countryId)
        {
            return await JsonSerializer.DeserializeAsync<Country>(
                await httpClient.GetStreamAsync($"/api/country/{countryId}"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        }

    }
}
