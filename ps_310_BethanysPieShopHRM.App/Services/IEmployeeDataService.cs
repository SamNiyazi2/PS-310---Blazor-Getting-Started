using ps_310_BethanysPieShopHRM.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// 05/10/2021 04:25 am - SSN - [20210510-0409] - [002] - M03-05 - Demo: Accessing data from the REST API

namespace ps_310_BethanysPieShopHRM.App.Services
{
    public interface IEmployeeDataService
    {


        //var data = await HttpClient.GetFromJsonAsync<List<Employee>>("https://localhost:44340/");

        Task<IEnumerable<Employee>> GetAllEmployees();

        Task<Employee> GetEmployeeDetails(int employeeId);

        // 04/05/2022 07:11 am - SSN
        // Task<Employee> AddEmployee(Employee employee);
        Task<object> AddEmployee(Employee employee);

        // 04/09/2022 11:47 pm - SSN - [20220409-2151] - [006] - Add RowVersion to Employee
        // Concurrency change
        //Task UpdateEmployee(Employee employee);
        Task<object> UpdateEmployee(Employee employee);

        Task DeleteEmployee(int employeeId);

        Task<IEnumerable<EmployeeTemp>> GetLongEmployeeList(int fileVersion_Short_Long);

        Task<IEnumerable<EmployeeTemp>> GetLongEmployeeList(int fileVersion_Short_Long, int startIndex, int count);

    }
}
