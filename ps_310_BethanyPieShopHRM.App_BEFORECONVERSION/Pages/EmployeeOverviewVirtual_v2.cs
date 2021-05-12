using Microsoft.AspNetCore.Components;
using ps_310_BethantysPieShopHRM.Shared;
using ps_310_BethanyPieShopHRM.App_BEFORECONVERSION.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ps_310_BethanyPieShopHRM.App_BEFORECONVERSION.Pages
{
    public partial class EmployeeOverviewVirtual_v2
    {
        public List<EmployeeTemp> employeeTemps { get; set; }

        [Inject]
        public IEmployeeDataService employeeDataService { get; set; }

        private float itemHeight = 50;

        async protected override Task OnInitializedAsync()
        {
            employeeTemps = (await employeeDataService.GetLongEmployeeList()).ToList();
        }

    }

}
