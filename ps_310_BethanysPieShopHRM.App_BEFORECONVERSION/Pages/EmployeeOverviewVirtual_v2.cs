using Microsoft.AspNetCore.Components;
using ps_310_BethanysPieShopHRM.Shared;
using ps_310_BethanysPieShopHRM.App_BEFORECONVERSION.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ps_310_BethanysPieShopHRM.App_BEFORECONVERSION.Pages
{
    public partial class EmployeeOverviewVirtual_v2
    {
        public List<EmployeeTemp> employeeTemps { get; set; }

        [Inject]
        public IEmployeeDataService employeeDataService { get; set; }

        private float itemHeight = 50;

        async protected override Task OnInitializedAsync()
        {
            // 2 for long version
            employeeTemps = (await employeeDataService.GetLongEmployeeList(2)).ToList();
        }

    }

}
