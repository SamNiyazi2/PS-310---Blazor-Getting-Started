using Microsoft.AspNetCore.Components;
using ps_310_BethanysPieShopHRM.Shared;
using ps_310_BethanysPieShopHRM.App.Components;
using ps_310_BethanysPieShopHRM.App.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace ps_310_BethanysPieShopHRM.App.Pages
{
    public partial class EmployeeOverview
    {

        public IEnumerable<Employee> Employees { get; set; }



        [Inject]
        public IEmployeeDataService EmployeeDataService { get; set; }


        protected AddEmployeeDialog AddEmployeeDialog { get; set; }



        async protected override Task OnInitializedAsync()
        {

            Employees = await EmployeeDataService.GetAllEmployees();

            // return  base.OnInitializedAsync();
        }




        protected void QuickAddEmployee()
        {
            AddEmployeeDialog.Show();
        }

        public async void AddEmployeeDialog_OnDialogClose()
        {
            Employees = (await EmployeeDataService.GetAllEmployees()).ToList();
            StateHasChanged();
        }




    }
}
