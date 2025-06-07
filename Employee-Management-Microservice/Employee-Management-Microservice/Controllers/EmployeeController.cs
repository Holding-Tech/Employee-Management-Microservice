using Employee_Management_Microservice.Models;
using Employee_Management_Microservice.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace Employee_Management_Microservice.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeController(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllEmployees()
        {
            var employees = await _employeeRepository.GetAllEmployeesAsync();
            return Ok(employees);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetEmployeeById(int id)
        {
            var employee = await _employeeRepository.GetEmployeeByIdAsync(id);
            if (employee == null)
                return NotFound(new { Message = "Employee not found." });

            return Ok(employee);
        }

        [HttpPost]
        public async Task<IActionResult> CreateEmployee([FromBody] Employee employee)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var created = await _employeeRepository.CreateEmployeeAsync(employee);
            return Ok(created); // ✅ This returns 200 OK with the employee data
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEmployee(int id, [FromBody] Employee employee)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var existing = await _employeeRepository.GetEmployeeByIdAsync(id);
            if (existing == null)
                return NotFound(new { Message = "Employee not found." });

            employee.EmployeeId = id;
            await _employeeRepository.UpdateEmployeeAsync(employee);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            var existing = await _employeeRepository.GetEmployeeByIdAsync(id);
            if (existing == null)
                return NotFound(new { Message = "Employee not found." });

            await _employeeRepository.DeleteEmployeeAsync(id);
            return NoContent();
        }

      

        [HttpGet("department/{departmentId}")]
        public async Task<IActionResult> GetEmployeesByDepartment(int departmentId)
        {
            var employees = await _employeeRepository.GetAllEmployeesAsync();
            var result = employees.Where(e => e.DepartmentId == departmentId);
            return Ok(result);
        }

        [HttpGet("search")]
        public async Task<IActionResult> SearchEmployees(string name)
        {
            var employees = await _employeeRepository.GetAllEmployeesAsync();
            var result = employees.Where(e =>
                (!string.IsNullOrEmpty(e.FirstName) && e.FirstName.Contains(name)) ||
                (!string.IsNullOrEmpty(e.LastName) && e.LastName.Contains(name)));

            return Ok(result);
        }

        [HttpGet("exists-by-email")]
        public async Task<IActionResult> CheckIfEmployeeExistsByEmail(string email)
        {
            var employees = await _employeeRepository.GetAllEmployeesAsync();
            var exists = employees.Any(e => e.Email.Equals(email, System.StringComparison.OrdinalIgnoreCase));
            return Ok(new { Exists = exists });
        }
    }
}
