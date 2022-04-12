using ps_310_BethanysPieShopHRM.Shared;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

// 05/10/2021 04:28 am - SSN - [20210510-0409] - [003] - M03-05 - Demo: Accessing data from the REST API

namespace ps_310_BethanysPieShopHRM.App.Services
{
    public class EmployeeDataService : IEmployeeDataService
    {
        private readonly HttpClient httpClient;

        public EmployeeDataService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }


        // 04/05/2022 07:10 am - SSN
        //#  public async Task<Employee> AddEmployee(Employee employee)
        public async Task<object> AddEmployee(Employee employee)
        {

            var employeeJson = new StringContent(JsonSerializer.Serialize(employee), Encoding.UTF8, "application/json");

            var response = await httpClient.PostAsync("api/employee", employeeJson);

            if (response.IsSuccessStatusCode)
            {
                return await JsonSerializer.DeserializeAsync<Employee>(await response.Content.ReadAsStreamAsync());
            }
            else
            {
                // 04/05/2022 07:06 am - SSN
                var returnResult1 = await response.Content.ReadAsStreamAsync();

                ErrorMessagesList.streamToText(returnResult1, out string stringValue);

                var returnResult3 = JsonSerializer.Deserialize<ErrorMessagesList>(stringValue, ErrorMessagesList.getJsonOptions());

                return returnResult3;
            }

        }


        public async Task DeleteEmployee(int employeeId)
        {
            await httpClient.DeleteAsync($"api/employee/{employeeId}");
        }


        public async Task<IEnumerable<Employee>> GetAllEmployees()
        {
            return await JsonSerializer.DeserializeAsync<IEnumerable<Employee>>
                (await httpClient.GetStreamAsync($"api/employee"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        }

        async public Task<Employee> GetEmployeeDetails(int employeeId)
        {
            return await JsonSerializer.DeserializeAsync<Employee>
                (await httpClient.GetStreamAsync($"api/employee/{employeeId}"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        }

        public async Task<object> UpdateEmployee(Employee employee)
        {
            var employeeJson = new StringContent(JsonSerializer.Serialize(employee), Encoding.UTF8, "application/json");

            // 04/09/2022 11:49 pm - SSN - [20220409-2151] - [008] - Add RowVersion to Employee
            // Concurrency check
            #region [20220409-2151] - [008] 
            // await httpClient.PutAsync("api/employee", employeeJson);
            var response = await httpClient.PutAsync("api/employee", employeeJson);

            if (response.IsSuccessStatusCode)
            {
                return response; //NoContent!!! Need object --  await JsonSerializer.DeserializeAsync<Employee>(await response.Content.ReadAsStreamAsync());
            }
            else
            {
                var returnResult1 = await response.Content.ReadAsStreamAsync();

                ErrorMessagesList.streamToText(returnResult1, out string stringValue);

                ErrorMessagesList returnResult3 = JsonSerializer.Deserialize<ErrorMessagesList>(stringValue, ErrorMessagesList.getJsonOptions());

                return returnResult3;
            }
            #endregion [20220409-2151] - [008] 

        }



        public async Task<IEnumerable<EmployeeTemp>> GetLongEmployeeList(int fileVersion_Short_Long)
        {
            return await JsonSerializer.DeserializeAsync<IEnumerable<EmployeeTemp>>
                (await httpClient.GetStreamAsync($"api/employee/long/{fileVersion_Short_Long}"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        }


        public async Task<IEnumerable<EmployeeTemp>> GetLongEmployeeList(int fileVersion_Short_Long, int startIndex, int count)
        {
            return await JsonSerializer.DeserializeAsync<IEnumerable<EmployeeTemp>>(
                await httpClient.GetStreamAsync($"api/employee/long/{fileVersion_Short_Long}/{startIndex}/{count}"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        }


    }
}
