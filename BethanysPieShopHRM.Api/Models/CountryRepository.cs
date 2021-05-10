

using System.Collections.Generic;
using System.Linq;

// 05/10/2021 03:33 am - SSN - [20210510-0323] - [001] - M03-03 - Demo: Exploring the API

// using BethanysPieShopHRM.Shared;
using ps_310_BethantysPieShopHRM.Shared;


namespace BethanysPieShopHRM.Api.Models
{
    public class CountryRepository : ICountryRepository
    {
        private readonly AppDbContext _appDbContext;

        public CountryRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public IEnumerable<Country> GetAllCountries()
        {
            return _appDbContext.Countries;
        }

        public Country GetCountryById(int countryId)
        {
            return _appDbContext.Countries.FirstOrDefault(c => c.CountryId == countryId);
        }
    }
}
