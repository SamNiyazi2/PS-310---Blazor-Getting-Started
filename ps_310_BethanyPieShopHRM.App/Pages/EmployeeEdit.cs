using Microsoft.AspNetCore.Components;
using ps_310_BethantysPieShopHRM.Shared;
using ps_310_BethanyPieShopHRM.App.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

// 05/10/2021 05:38 am - SSN - [20210510-0536] - [001] - M03-09 - Demo: Adding the add employee form

namespace ps_310_BethanyPieShopHRM.App.Pages
{
    public partial class EmployeeEdit
    {

        [Inject]
        public IEmployeeDataService EmployeeDataService { get; set; }


        [Inject]
        public ICountryDataService CountryDataService { get; set; }


        [Inject]
        public IJobCategoryDataService JobCategoryDataService { get; set; }

        [Parameter]
        public string EmployeeId { get; set; }

        public string CountryId { get; set; }
        
        public string JobCategoryId { get; set; }


        public Employee Employee { get; set; } = new Employee();

        public List<Country> Countries { get; set; } = new List<Country>();
        
        public List<JobCategory> JobCategories { get; set; } = new List<JobCategory>();


        async protected override Task OnInitializedAsync()
        {
 

            Countries = (await CountryDataService.GetAllCountries()).ToList();
           
            JobCategories = (await JobCategoryDataService.GetAllJobCategories()).ToList();


            if (int.TryParse(EmployeeId, out int _employeeID))
            {
                Employee = (await EmployeeDataService.GetEmployeeDetails(_employeeID)) ?? new Employee();
            }

            CountryId = Employee.CountryId.ToString();
            JobCategoryId = Employee.JobCategoryId.ToString();
            

            // return base.OnInitializedAsync();
        }
    }
}
