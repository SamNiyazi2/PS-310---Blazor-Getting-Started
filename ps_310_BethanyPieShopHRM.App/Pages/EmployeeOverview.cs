using Microsoft.AspNetCore.Components;
using ps_310_BethantysPieShopHRM.Shared;
using ps_310_BethanyPieShopHRM.App.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace ps_310_BethanyPieShopHRM.App.Pages
{
    public partial class EmployeeOverview
    {

        public IEnumerable<Employee> Employees { get; set; }



        [Inject]
        public IEmployeeDataService employeeDataService { get; set; }

        async protected override Task OnInitializedAsync()
        {

            Employees = await employeeDataService.GetAllEmployees();

            // return  base.OnInitializedAsync();
        }








    }
}
