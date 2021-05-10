using Microsoft.AspNetCore.Components;
using ps_310_BethantysPieShopHRM.Shared;
using ps_310_BethanyPieShopHRM.App.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// 05/10/2021 02:29 am - SSN - [20210510-0229] - [001] - M02-09 - Demo: Creating your first app 

namespace ps_310_BethanyPieShopHRM.App.Pages
{
    public partial class EmployeeDetail
    {

        [Parameter]
        public string EmployeeId { get; set; }

        public Employee Employee { get; set; }


        [Inject]
        public IEmployeeDataService employeeDataService { get; set; }

        async protected override Task OnInitializedAsync()
        {

            int.TryParse(EmployeeId, out int _employeeId);

            Employee = await employeeDataService.GetEmployeeDetails(_employeeId);

            // return base.OnInitializedAsync();
        }







    }
}
