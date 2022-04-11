﻿using ps_310_BethanysPieShopHRM.Shared;
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

        Task<Employee> AddEmployee(Employee employee);

        Task UpdateEmployee(Employee employee);

        Task DeleteEmployee(int employeeId);

        Task<IEnumerable<EmployeeTemp>> GetLongEmployeeList();

        Task<IEnumerable<EmployeeTemp>> GetLongEmployeeList(int startIndex, int count);

    }
}