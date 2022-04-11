using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using ps_310_BethanysPieShopHRM.ComponentsLibrary.Map;
using ps_310_BethanysPieShopHRM.Shared;
using ps_310_BethanysPieShopHRM.App_BEFORECONVERSION.Services;


// 05/10/2021 02:29 am - SSN - [20210510-0229] - [001] - M02-09 - Demo: Creating your first app 

namespace ps_310_BethanysPieShopHRM.App_BEFORECONVERSION.Pages
{
    public partial class EmployeeDetail
    {

        [Parameter]
        public string EmployeeId { get; set; }

        public Employee employee { get; set; }


        [Inject]
        public IEmployeeDataService employeeDataService { get; set; }


        public List<Marker> MapMarkers { get; set; } = new List<Marker>();


        async protected override Task OnInitializedAsync()
        {

            int.TryParse(EmployeeId, out int _employeeId);

            employee = await employeeDataService.GetEmployeeDetails(_employeeId);


            MapMarkers.Add(new Marker { Description = $"{employee.FirstName} {employee.LastName}", ShowPopup = false, X = employee.Longitude, Y = employee.Latitude });


            // return base.OnInitializedAsync();
        }







    }
}
