using ps_310_BethanysPieShopHRM.Api.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// 05/10/2021 03:34 am - SSN - [20210510-0323] - [003] - M03-03 - Demo: Exploring the API
//using ps_310_BethanysPieShopHRM.Shared;
using ps_310_BethanysPieShopHRM.Shared;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;

namespace ps_310_BethanysPieShopHRM.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : Controller
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;
        //        private readonly IHttpContextAccessor _httpContextAccessor;

        public EmployeeController(IEmployeeRepository employeeRepository, IWebHostEnvironment webHostEnvironment) //, IHttpContextAccessor httpContextAccessor)
        {
            this._employeeRepository = employeeRepository;
            this._webHostEnvironment = webHostEnvironment;

            // Needed to add httpContextAccessor to the services in startup. Not for WebHostEnvironment.
            // this._httpContextAccessor = httpContextAccessor;
            // Did not work. Using local HttpContext.
        }

        [HttpGet]
        public IActionResult GetAllEmployees()
        {
            return Ok(_employeeRepository.GetAllEmployees());
        }

        [HttpGet("{id}")]
        public IActionResult GetEmployeeById(int id)
        {
            return Ok(_employeeRepository.GetEmployeeById(id));
        }

        [HttpPost]
        public IActionResult CreateEmployee([FromBody] Employee employee)
        {
            if (employee == null)
                return BadRequest();

            if (employee.FirstName == string.Empty || employee.LastName == string.Empty)
            {
                ModelState.AddModelError("Name/FirstName", "The name or first name shouldn't be empty");
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            string currentUrl = saveFileToDisk(employee);

            // 04/05/2022 04:19 am - SSN - We don't need to add full path.
            //    employee.ImageName = $"https://{currentUrl}/uploads/{employee.ImageName}";

            // 04/05/2022 06:06 am - SSN - Try/catch
            Employee createdEmployee = null;
            try
            {
                createdEmployee = _employeeRepository.AddEmployee(employee);
            }
            catch (DbUpdateException ex1)
            {
                ErrorMessagesList errorMessagesList = new ErrorMessagesList();

                errorMessagesList.AddEntry("Error_101a", ex1);

                foreach (DbEntityValidationResult error in ex1.Data)
                {
                    foreach (DbValidationError validationError in error.ValidationErrors)
                    {
                        errorMessagesList.AddEntry(validationError.PropertyName, validationError.ErrorMessage);
                    }
                }

                return BadRequest(errorMessagesList);
            }
            catch (DbEntityValidationException ex2)
            {
                ErrorMessagesList errorMessagesList = new ErrorMessagesList();
                errorMessagesList.AddEntry("Error_102", ex2.InnerException);

                foreach (DbEntityValidationResult error in ex2.EntityValidationErrors)
                {
                    foreach (DbValidationError validationError in error.ValidationErrors)
                    {
                        errorMessagesList.AddEntry(validationError.PropertyName, validationError.ErrorMessage);
                    }
                }

                return BadRequest(errorMessagesList);
            }
            catch (System.Exception ex)
            {
                ErrorMessagesList errorMessagesList = new ErrorMessagesList();

                errorMessagesList.AddEntry("Error_103", ex);

                return BadRequest(errorMessagesList);
            }

            return Created("employee", createdEmployee);
        }

        private string saveFileToDisk(Employee employee)
        {
            // Created directory wwwroot\uploads
            // Add UseStaticFiles to startup configure
            // 04/05/2022 03:43 am - SSN - Check for null
            if (!string.IsNullOrEmpty(employee.ImageName))
            {
                string currentUrl = HttpContext.Request.Host.Value;
                var path = $"{_webHostEnvironment.WebRootPath}\\uploads\\{employee.ImageName}";
                var fileStream = System.IO.File.Create(path);
                fileStream.Write(employee.ImageContent, 0, employee.ImageContent.Length);
                fileStream.Close();
                return currentUrl;
            }
            return null;

        }

        [HttpPut]
        public IActionResult UpdateEmployee([FromBody] Employee employee)
        {
            if (employee == null)
                return BadRequest();

            if (employee.FirstName == string.Empty || employee.LastName == string.Empty)
            {
                ModelState.AddModelError("Name/FirstName", "The name or first name shouldn't be empty");
            }



            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var employeeToUpdate2 = _employeeRepository.GetEmployeeById(employee.EmployeeId);

            if (employeeToUpdate2 == null)
                return NotFound();






            // 04/10/2022 01:49 am - SSN - [20220409-2151] - [010] - Add RowVersion to Employee

            #region [20220409-2151] - [010] 

            Employee updatedEmployee = null;

            try
            {
                // _employeeRepository.UpdateEmployee(employee);
                updatedEmployee = _employeeRepository.UpdateEmployee(employee);

                return NoContent(); //success
            }
            catch (DbUpdateConcurrencyException ex)
            {

                ErrorMessagesList errorMessagesList = new ErrorMessagesList();

                // https://docs.microsoft.com/en-us/ef/core/saving/concurrency
                foreach (var entry in ex.Entries)
                {
                   
                    if (entry.Entity is Employee)
                    {
                        var proposedValues = entry.CurrentValues;
                        var databaseValues = entry.GetDatabaseValues();
                        var originalValues = entry.OriginalValues;

                        foreach (var property in proposedValues.Properties)
                        {
                            var proposedValue = proposedValues[property];
                            var databaseValue = databaseValues[property]; 

                            // TODO: decide which value should be written to database
                            // proposedValues[property] = <value to be saved>;

                            if (property.Name.ToUpper() != "ROWVERSION" && proposedValue != null && proposedValue != null)
                            {
                                if (!databaseValue.Equals(proposedValue))
                                {
                                    errorMessagesList.AddEntry(property.Name, $"Conflict: Your input [{proposedValue}] wasn't saved. Original value was changed to [{databaseValue}]. ");
                                }
                            }

                        }

                        // Refresh original values to bypass next concurrency check
                        // entry.OriginalValues.SetValues(databaseValues);

                    }

                }

                return BadRequest(errorMessagesList);

            }
            catch (DbUpdateException ex1)
            {
                ErrorMessagesList errorMessagesList = new ErrorMessagesList();

                errorMessagesList.AddEntry("Error_104", ex1);

                foreach (DbEntityValidationResult error in ex1.Data)
                {
                    foreach (DbValidationError validationError in error.ValidationErrors)
                    {
                        errorMessagesList.AddEntry(validationError.PropertyName, validationError.ErrorMessage);
                    }
                }

                return BadRequest(errorMessagesList);
            }
            catch (DbEntityValidationException ex2)
            {
                ErrorMessagesList errorMessagesList = new ErrorMessagesList();
                errorMessagesList.AddEntry("Error_105", ex2.InnerException);

                foreach (DbEntityValidationResult error in ex2.EntityValidationErrors)
                {
                    foreach (DbValidationError validationError in error.ValidationErrors)
                    {
                        errorMessagesList.AddEntry(validationError.PropertyName, validationError.ErrorMessage);
                    }
                }

                return BadRequest(errorMessagesList);
            }
            catch (System.Exception ex)
            {
                ErrorMessagesList errorMessagesList = new ErrorMessagesList();

                errorMessagesList.AddEntry("Error_106", ex);

                return BadRequest(errorMessagesList);
            }


            #endregion [20220409-2151] - [010] 




        }

        [HttpDelete("{id}")]
        public IActionResult DeleteEmployee(int id)
        {
            if (id == 0)
                return BadRequest();

            var employeeToDelete = _employeeRepository.GetEmployeeById(id);
            if (employeeToDelete == null)
                return NotFound();

            _employeeRepository.DeleteEmployee(id);

            return NoContent();//success
        }




        [HttpGet("long/{fileVersion_Short_Long}")]
        public IActionResult GetLongEmployeeList(int fileVersion_Short_Long)
        {
            return Ok(_employeeRepository.GetLongEmployeList(fileVersion_Short_Long));
        }

        [HttpGet("long/{fileVersion_Short_Long}/{startindex}/{count}")]
        public IActionResult GetLongEmployeeList(int fileVersion_Short_Long, int startIndex, int count)
        {
            return Ok(_employeeRepository.GetLongEmployeList(fileVersion_Short_Long, startIndex, count));
        }



    }
}
