﻿using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web.Virtualization;
using ps_310_BethantysPieShopHRM.Shared;
using ps_310_BethanyPieShopHRM.App_BEFORECONVERSION.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ps_310_BethanyPieShopHRM.App_BEFORECONVERSION.Pages
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
            var employeeListItems = (await employeeDataService.GetLongEmployeeList(request.StartIndex, numberOfEmployees)).ToList();
           
            isLoading = false;
           
            StateHasChanged();

            return new ItemsProviderResult<EmployeeTemp>(employeeListItems, totalNumberOfEmployees);

        }


    }

}
