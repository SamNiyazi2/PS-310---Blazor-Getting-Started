
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using Microsoft.AspNetCore.Hosting;


// 05/10/2021 03:34 am - SSN - [20210510-0323] - [004] - M03-03 - Demo: Exploring the API
// using BethanysPieShopHRM.Shared;
using ps_310_BethantysPieShopHRM.Shared;


namespace BethanysPieShopHRM.Api.Models
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly AppDbContext appDbContext;
        public IWebHostEnvironment webHostEnvironment;

        public EmployeeRepository(AppDbContext _appDbContext, IWebHostEnvironment _webHostEnvironment)
        {
            appDbContext = _appDbContext;
            webHostEnvironment = _webHostEnvironment;
        }

        public IEnumerable<Employee> GetAllEmployees()
        {
            return appDbContext.Employees;
        }

        public Employee GetEmployeeById(int employeeId)
        {
            return appDbContext.Employees.FirstOrDefault(c => c.EmployeeId == employeeId);
        }

        public Employee AddEmployee(Employee employee)
        {
            var addedEntity = appDbContext.Employees.Add(employee);
            appDbContext.SaveChanges();
            return addedEntity.Entity;
        }

        public Employee UpdateEmployee(Employee employee)
        {
            var foundEmployee = appDbContext.Employees.FirstOrDefault(e => e.EmployeeId == employee.EmployeeId);

            if (foundEmployee != null)
            {
                foundEmployee.CountryId = employee.CountryId;
                foundEmployee.MaritalStatus = employee.MaritalStatus;
                foundEmployee.BirthDate = employee.BirthDate;
                foundEmployee.City = employee.City;
                foundEmployee.Email = employee.Email;
                foundEmployee.FirstName = employee.FirstName;
                foundEmployee.LastName = employee.LastName;
                foundEmployee.Gender = employee.Gender;
                foundEmployee.PhoneNumber = employee.PhoneNumber;
                foundEmployee.Smoker = employee.Smoker;
                foundEmployee.Street = employee.Street;
                foundEmployee.Zip = employee.Zip;
                foundEmployee.JobCategoryId = employee.JobCategoryId;
                foundEmployee.Comment = employee.Comment;
                foundEmployee.ExitDate = employee.ExitDate;
                foundEmployee.JoinedDate = employee.JoinedDate;

                appDbContext.SaveChanges();

                return foundEmployee;
            }

            return null;
        }

        public void DeleteEmployee(int employeeId)
        {
            var foundEmployee = appDbContext.Employees.FirstOrDefault(e => e.EmployeeId == employeeId);
            if (foundEmployee == null) return;

            appDbContext.Employees.Remove(foundEmployee);
            appDbContext.SaveChanges();

        }


        IEnumerable<EmployeeTemp> tempList;


        public IEnumerable<EmployeeTemp> GetLongEmployeList(int fileVersion_Short_Long)
        {
            if (tempList == null)
            {

                string fileName = webHostEnvironment.WebRootPath + @"\TestData_12201\TestData.json";

                if (fileVersion_Short_Long > 1)
                {
                    fileName = webHostEnvironment.WebRootPath + @"\TestData_12201\TestData_100k.json";
                }

                string json = System.IO.File.ReadAllText(fileName);
                tempList = JsonSerializer.Deserialize<IEnumerable<EmployeeTemp>>(json);
            }

            return tempList;
        }


        public IEnumerable<EmployeeTemp> GetLongEmployeList(int fileVersion_Short_Long, int startIndex, int count)
        {
            return GetLongEmployeList(fileVersion_Short_Long).Skip(startIndex).Take(count);
        }

    }



}
