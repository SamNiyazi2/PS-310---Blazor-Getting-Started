using ps_310_BethanysPieShopHRM.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// 05/10/2021 06:09 am - SSN - [20210510-0606] - [001] - M03-10 - Demo: Adding more component

namespace ps_310_BethanysPieShopHRM.App_BEFORECONVERSION.Services
{
public    interface ICountryDataService
    {
        Task<IEnumerable<Country>> GetAllCountries();
        
        Task<Country> GetCountryById(int countryId);
    
    }
}
