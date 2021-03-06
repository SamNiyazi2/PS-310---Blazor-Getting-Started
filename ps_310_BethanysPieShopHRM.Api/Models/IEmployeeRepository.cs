
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// 05/10/2021 03:36 am - SSN - [20210510-0323] - [005] - M03-03 - Demo: Exploring the API
// using ps_310_BethanysPieShopHRM.Shared;
using ps_310_BethanysPieShopHRM.Shared;


namespace ps_310_BethanysPieShopHRM.Api.Models
{
    public interface IEmployeeRepository
    {
        IEnumerable<Employee> GetAllEmployees();
        Employee GetEmployeeById(int employeeId);
        Employee AddEmployee(Employee employee);
        Employee UpdateEmployee(Employee employee);
        void DeleteEmployee(int employeeId);
        IEnumerable<EmployeeTemp> GetLongEmployeList(int fileVersion_Short_Long);
        IEnumerable<EmployeeTemp> GetLongEmployeList(int fileVersion_Short_Long, int startIndex, int count);

    }
}
