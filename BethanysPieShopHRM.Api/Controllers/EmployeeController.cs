using BethanysPieShopHRM.Api.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

// 05/10/2021 03:34 am - SSN - [20210510-0323] - [003] - M03-03 - Demo: Exploring the API
//using BethanysPieShopHRM.Shared;
using ps_310_BethantysPieShopHRM.Shared;

namespace BethanysPieShopHRM.Api.Controllers
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

            // Needed to add httpContextAccessor to the services in startup. Not for WebHostEncironment.
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

            employee.ImageName = $"https://{currentUrl}/uploads/{employee.ImageName}";

            var createdEmployee = _employeeRepository.AddEmployee(employee);

            return Created("employee", createdEmployee);
        }

        private string saveFileToDisk(Employee employee)
        {
            // Created directory wwwroot\uploads
            // Add UseStaticFiles to startup configure
            string currentUrl = HttpContext.Request.Host.Value;
            var path = $"{_webHostEnvironment.WebRootPath}\\uploads\\{employee.ImageName}";
            var fileStream = System.IO.File.Create(path);
            fileStream.Write(employee.ImageContent, 0, employee.ImageContent.Length);
            fileStream.Close();
            return currentUrl;
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

            var employeeToUpdate = _employeeRepository.GetEmployeeById(employee.EmployeeId);

            if (employeeToUpdate == null)
                return NotFound();

            _employeeRepository.UpdateEmployee(employee);

            return NoContent(); //success
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
