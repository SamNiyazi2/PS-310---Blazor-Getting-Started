using Microsoft.AspNetCore.Components;
using ps_310_BethantysPieShopHRM.Shared;
using ps_310_BethanyPieShopHRM.App.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// 05/10/2021 05:58 pm - SSN - [20210510-1752] - [001] - M04-03 - Demo: Adding the add employee dialog

namespace ps_310_BethanyPieShopHRM.App.Components
{
    public partial class AddEmployeeDialog
    {

        public Employee Employee { get; set; } = new Employee { CountryId = 1, BirthDate = DateTime.Now, JoinedDate = DateTime.Now };

        [Inject]
        public IEmployeeDataService EmployeeDataService { get; set; }

        public bool ShowDialog { get; set; }


        [Parameter]
        public EventCallback<bool> CloseEventCallback { get; set; }



        public void Show()
        {
            ResetDialog();
            ShowDialog = true;
            StateHasChanged();
        }

        public void Close()
        {
            ShowDialog = false;
            StateHasChanged();
        }

        private void ResetDialog()
        {
            Employee = new Employee { CountryId = 1, JobCategoryId = 1, BirthDate = DateTime.Now, JoinedDate = DateTime.Now };
        }

        protected async Task HandleValidSubmit()
        {
            await EmployeeDataService.AddEmployee(Employee);
            ShowDialog = false;

            await CloseEventCallback.InvokeAsync(true);
            StateHasChanged();
        }

    }
}
