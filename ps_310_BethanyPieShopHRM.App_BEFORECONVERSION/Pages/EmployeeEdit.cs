using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.JSInterop;
using ps_310_BethantysPieShopHRM.Shared;
using ps_310_BethanyPieShopHRM.App_BEFORECONVERSION.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

// 05/10/2021 05:38 am - SSN - [20210510-0536] - [001] - M03-09 - Demo: Adding the add employee form

namespace ps_310_BethanyPieShopHRM.App_BEFORECONVERSION.Pages
{
    public partial class EmployeeEdit
    {

        [Inject]
        public NavigationManager navigationManager { get; set; }

        [Inject]
        public IEmployeeDataService EmployeeDataService { get; set; }


        [Inject]
        public ICountryDataService CountryDataService { get; set; }


        [Inject]
        public IJobCategoryDataService JobCategoryDataService { get; set; }

        [Inject]
        public IJSRuntime jSRuntime { get; set; }



        [Parameter]
        public string EmployeeId { get; set; }

        public string CountryId { get; set; }

        public string JobCategoryId { get; set; }


        public Employee Employee { get; set; } = new Employee();

        public List<Country> Countries { get; set; } = new List<Country>();

        public List<JobCategory> JobCategories { get; set; } = new List<JobCategory>();



        protected string Message = string.Empty;
        protected string StatusClass = string.Empty;
        protected bool Saved;


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

         

        async protected override Task OnAfterRenderAsync(bool firstRender)
        {
            await jSRuntime.InvokeVoidAsync("ssnSetFocus", "someid");
        }


         

        protected async Task HandleValidSubmit()
        {
            Saved = false;
            Employee.CountryId = int.Parse(CountryId);
            Employee.JobCategoryId = int.Parse(JobCategoryId);


            if (selectedFiles != null && selectedFiles.Count > 0)
            {
                var file = selectedFiles[0];
                Stream stream = file.OpenReadStream();
                MemoryStream ms = new MemoryStream();
                await stream.CopyToAsync(ms);
                stream.Close();
                Employee.ImageName = file.Name;
                Employee.ImageContent = ms.ToArray();

            }


            if (Employee.EmployeeId == 0) //new
            {
                var addedEmployee = await EmployeeDataService.AddEmployee(Employee);
                if (addedEmployee != null)
                {
                    StatusClass = "alert-success";
                    Message = "New employee added successfully.";
                    Saved = true;
                }
                else
                {
                    StatusClass = "alert-danger";
                    Message = "Something went wrong adding the new employee. Please try again.";
                    Saved = false;
                }
            }
            else
            {
                await EmployeeDataService.UpdateEmployee(Employee);
                StatusClass = "alert-success";
                Message = "Employee updated successfully.";
                Saved = true;
            }
        }

        protected void HandleInvalidSubmit()
        {
            StatusClass = "alert-danger";
            Message = "There are some validation errors. Please try again.";
        }






        protected async Task DeleteEmployee()
        {
            await EmployeeDataService.DeleteEmployee(Employee.EmployeeId);

            StatusClass = "alert-success";
            Message = "Deleted successfully";

            Saved = true;
        }

        protected void NavigateToOverview()
        {
            navigationManager.NavigateTo("/employeeoverview");
        }



        private IReadOnlyList<IBrowserFile> selectedFiles;

        private void OnInputFileChange(InputFileChangeEventArgs e)
        {
            selectedFiles = e.GetMultipleFiles();
            if (selectedFiles == null)
            {
                Message = "No files were selected";
            }
            else
            {
                Message = selectedFiles.Count == 0 ? "No files were selected" : string.Format($"{selectedFiles.Count} file{0} selected", selectedFiles.Count == 1 ? "" : "s");
            }
        }



    }
}
