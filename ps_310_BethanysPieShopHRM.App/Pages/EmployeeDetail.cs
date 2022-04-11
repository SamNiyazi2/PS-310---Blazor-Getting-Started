using ps_310_BethanysPieShopHRM.ComponentsLibrary.Map;
using Microsoft.AspNetCore.Components;
using ps_310_BethanysPieShopHRM.Shared;
using ps_310_BethanysPieShopHRM.App.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// 05/10/2021 02:29 am - SSN - [20210510-0229] - [001] - M02-09 - Demo: Creating your first app 

namespace ps_310_BethanysPieShopHRM.App.Pages
{
    public partial class EmployeeDetail
    {

        [Parameter]
        public string EmployeeId { get; set; }

        public Employee Employee { get; set; }


        [Inject]
        public IEmployeeDataService employeeDataService { get; set; }


        public List<Marker> MapMarkers { get; set; } = new List<Marker>();


        async protected override Task OnInitializedAsync()
        {

            int.TryParse(EmployeeId, out int _employeeId);

            Employee = await employeeDataService.GetEmployeeDetails(_employeeId);


            MapMarkers.Add(new Marker { Description = $"{Employee.FirstName} {Employee.LastName}", ShowPopup = false, X = Employee.Longitude, Y = Employee.Latitude });


            // return base.OnInitializedAsync();
        }







    }
}
