
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.Extensions.Logging;
using Microsoft.JSInterop;
using ps_310_BethanysPieShopHRM.Shared;
using ps_310_BethanysPieShopHRM.App.Services;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

// 05/10/2021 05:38 am - SSN - [20210510-0536] - [001] - M03-09 - Demo: Adding the add employee form

namespace ps_310_BethanysPieShopHRM.App.Pages
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


        // 04/10/2022 03:03 am - SSN
        [Inject]
        public ILogger<EmployeeEdit> logger { get; set; }

        [Parameter]
        public string EmployeeId { get; set; }

        //public string CountryId { get; set; }

        //public string JobCategoryId { get; set; }


        public Employee Employee { get; set; } = new Employee();

        public List<Country> Countries { get; set; } = new List<Country>();

        public List<JobCategory> JobCategories { get; set; } = new List<JobCategory>();



        protected string Message = string.Empty;
        protected string StatusClass = string.Empty;
        protected bool Saved;

        // 04/06/2022 03:30 am - SSN
        protected MarkupString MarkupMessage = new MarkupString();


        // 04/06/2022 08:01 am - SSN - Add EditContext
        public bool FormReady { get; set; }

        EditContext editContext { get; set; }

        ValidationMessageStore messageStore { get; set; }

        async protected override Task OnInitializedAsync()
        {


            Countries = (await CountryDataService.GetAllCountries()).ToList();

            JobCategories = (await JobCategoryDataService.GetAllJobCategories()).ToList();


            if (int.TryParse(EmployeeId, out int _employeeID))
            {
                Employee = (await EmployeeDataService.GetEmployeeDetails(_employeeID)) ?? new Employee();
            }

            //CountryId = Employee.CountryId.ToString();
            //JobCategoryId = Employee.JobCategoryId.ToString();

            editContext = new EditContext(Employee);
            editContext.OnFieldChanged += OnFieldChanged;
            messageStore = new ValidationMessageStore(editContext);



            FormReady = true;
         
            // return base.OnInitializedAsync();

        }


        string getFieldName(dynamic e)
        {
            try
            {
                return e.FieldIdentifier.FieldName;
            }
            catch (Exception)
            {
                // Do nothing.
                // throw;
                return "";
            }

        }


        protected void OnFieldChanged(object sender, EventArgs e)
        {
            string fieldName = getFieldName(e);


            EditContext _editContext = sender as EditContext;

            if (messageStore != null && _editContext != null && !string.IsNullOrWhiteSpace(fieldName))
            {
                messageStore.Clear(_editContext.Field(fieldName));
            }

        }


        async protected override Task OnAfterRenderAsync(bool firstRender)
        {
            await jSRuntime.InvokeVoidAsync("ssnSetFocus", firstRender);
        }



        protected async Task HandleValidSubmit()
        {

            // 04/06/2022 09:07 pm - SSN - Tested OK


            //messageStore.Add(editContext.Field("FirstName"), "Firt name error");
            //messageStore.Add(editContext.Field("Email"), "Email  error");
            //messageStore.Add(editContext.Field("Country"), "Country error");
            //messageStore.Add(editContext.Field("CountryId"), "Country ID error");
            //editContext.NotifyValidationStateChanged();

            //if (!editContext.Validate())
            //{
            //    return;
            //}


            Saved = false;

            // 04/07/2022 12:40 am - SSN 
            // Employee.CountryId = int.Parse(CountryId);
            //if (int.TryParse(CountryId, out int tempCountryId))
            //{
            //    Employee.CountryId = tempCountryId;
            //}
            //if (int.TryParse(JobCategoryId, out int tempJobCategoryId))
            //{
            //    Employee.JobCategoryId = tempJobCategoryId;
            //}
            if ( Employee.JobCategoryId <= 0)
            {
                messageStore.Add(editContext.Field("JobCategoryId"), "Category selection is required");
            }

            if ( Employee.CountryId<=0)
            {
            messageStore.Add(editContext.Field("CountryId"), "Country selection is required");
            }

            editContext.NotifyValidationStateChanged();

            if (!editContext.Validate())
            {
                return;
            }


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
                    if (addedEmployee.GetType() == typeof(Employee))
                    {
                        StatusClass = "alert-success";
                        Message = "New employee added successfully.";
                        Saved = true;

                    }
                    else if (addedEmployee.GetType() == typeof(ErrorMessagesList))
                    {

                        var response = addedEmployee as ErrorMessagesList;

                        if (response != null && response.GetType() == typeof(ErrorMessagesList))
                        {
                            MarkupMessage = new MarkupString(ErrorMessagesList.createHtmlErrorMessageList(response));
                        }
                        else
                        {

                            Message = "Something went wrong adding the new employee. Please try again. (1101)";
                        }

                        StatusClass = "alert-danger";
                        Saved = false;
                    }
                    else
                    {
                        StatusClass = "alert-danger";
                        Message = "Something went wrong adding the new employee. Please try again. (1102)";
                        Saved = false;

                    }



                }
                else
                {
                    StatusClass = "alert-danger";
                    Message = "Something went wrong adding the new employee. Please try again. (1103)";
                    Saved = false;
                }
            }
            else
            {
                // 04/09/2022 11:47 pm - SSN - [20220409-2151] - [007] - Add RowVersion to Employee
                var response = await EmployeeDataService.UpdateEmployee(Employee);

                #region [20220409-2151]
                Type responseType = response.GetType();

                if (responseType.Name == "HttpResponseMessage") // API return Nocontent.
                {
                    System.Net.Http.HttpResponseMessage httpResponseMessage = response as System.Net.Http.HttpResponseMessage;

                    string content = httpResponseMessage.Content.ReadAsStringAsync().Result;
                    logger.Log(LogLevel.Information, "20220410-0306 - Testing-101");
                    logger.Log(LogLevel.Warning, "20220410-0306 - Testing-102");
                    logger.Log(LogLevel.Debug, "20220410-0306 - Testing-103");

                    if (httpResponseMessage != null && httpResponseMessage.IsSuccessStatusCode && string.IsNullOrWhiteSpace(content))
                    {

                        StatusClass = "alert-success";
                        Message = "Employee updated successfully.";
                        Saved = true;
                    }
                    else
                    {
                        StatusClass = "alert-danger";
                        Message = "Something went wrong adding the new employee. Please try again. (3301)";
                        Saved = false;

                        logger.Log(LogLevel.Warning, $"20220410-0307 - Failed to save employee record. Returned success status. [{EmployeeId}] [{content}]");

                    }
                }
                else if (responseType == typeof(ErrorMessagesList))
                {

                    var errorMessages = response as ErrorMessagesList;

                    if (errorMessages != null)
                    {
                        MarkupMessage = new MarkupString(ErrorMessagesList.createHtmlErrorMessageList(errorMessages));
                    }
                    else
                    {
                        Message = "Something went wrong adding the new employee. Please try again. (2201)";
                    }

                    StatusClass = "alert-danger";
                    Saved = false;

                    logger.LogWarning($"20220410-0308 - Failed to save employee record. Error messages: {EmployeeId}", errorMessages);

                }
                else
                {
                    StatusClass = "alert-danger";
                    Message = "Something went wrong adding the new employee. Please try again. (2202)";
                    Saved = false;

                    logger.LogError($"20220410-0309 - Failed to save employee record. {EmployeeId}");

                }

                #endregion [20220409-2151]

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
