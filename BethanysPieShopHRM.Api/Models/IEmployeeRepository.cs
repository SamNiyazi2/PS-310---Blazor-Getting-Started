
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// 05/10/2021 03:36 am - SSN - [20210510-0323] - [005] - M03-03 - Demo: Exploring the API
// using BethanysPieShopHRM.Shared;
using ps_310_BethantysPieShopHRM.Shared;


namespace BethanysPieShopHRM.Api.Models
{
    public interface IEmployeeRepository
    {
        IEnumerable<Employee> GetAllEmployees();
        Employee GetEmployeeById(int employeeId);
        Employee AddEmployee(Employee employee);
        Employee UpdateEmployee(Employee employee);
        void DeleteEmployee(int employeeId);
    }
}
