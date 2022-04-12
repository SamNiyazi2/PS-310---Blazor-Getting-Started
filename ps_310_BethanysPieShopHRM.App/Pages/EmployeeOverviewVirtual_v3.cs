using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web.Virtualization;
using ps_310_BethanysPieShopHRM.Shared;
using ps_310_BethanysPieShopHRM.App.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ps_310_BethanysPieShopHRM.App.Pages
{
    public partial class EmployeeOverviewVirtual_v3
    {


        [Inject]
        public IEmployeeDataService employeeDataService { get; set; }

        private float itemHeight = 50;

        private int totalNumberOfEmployees = 100000;

        public bool? isLoading { get; set; }
 

        public async ValueTask<ItemsProviderResult<EmployeeTemp>> LoadEmployees(ItemsProviderRequest request)
        {
            if (isLoading == null) isLoading = true;
            StateHasChanged();


            var numberOfEmployees = Math.Min(request.Count, totalNumberOfEmployees - request.StartIndex);
            // 2 for long version
            var employeeListItems = (await employeeDataService.GetLongEmployeeList(2,request.StartIndex, numberOfEmployees)).ToList();
           
            isLoading = false;
           
            StateHasChanged();

            return new ItemsProviderResult<EmployeeTemp>(employeeListItems, totalNumberOfEmployees);

        }


    }

}
