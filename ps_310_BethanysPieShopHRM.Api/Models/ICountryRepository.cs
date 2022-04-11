
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


// 05/10/2021 03:36 am - SSN - [20210510-0323] - [006] - M03-03 - Demo: Exploring the API
// using BethanysPieShopHRM.Shared;
using ps_310_BethantysPieShopHRM.Shared;


namespace BethanysPieShopHRM.Api.Models
{
    public interface ICountryRepository
    {
        IEnumerable<Country> GetAllCountries();
        Country GetCountryById(int countryId);
    }
}
