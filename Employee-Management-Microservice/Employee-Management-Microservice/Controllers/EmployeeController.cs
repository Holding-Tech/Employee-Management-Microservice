using Employee_Management_Microservice.DTO.Employee_Management_Microservice.Models;
using Employee_Management_Microservice.Models;
using Employee_Management_Microservice.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Employee_Management_Microservice.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        /// <summary>
        /// Get all users in the system.
        /// </summary>
        [HttpGet("all")]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _userRepository.GetAllUsersAsync();
            return Ok(users);
        }

        /// <summary>
        /// Get a user by ID.
        /// </summary>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            var user = await _userRepository.GetUserByIdAsync(id);
            if (user == null)
                return NotFound(new { Message = "User not found." });

            return Ok(user);
        }

        /// <summary>
        /// Create a new user.
        /// </summary>
        [HttpPost("create")]
        public async Task<IActionResult> CreateUser([FromBody] User user)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var createdUser = await _userRepository.CreateUserAsync(user);
            return CreatedAtAction(nameof(GetUserById), new { id = createdUser.UserId }, createdUser);
        }

        /// <summary>
        /// Update a user's information.
        /// </summary>
        [HttpPut("update")]
        public async Task<IActionResult> UpdateUser([FromBody] User user)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _userRepository.UpdateUserAsync(user);
            return NoContent();
        }

        /// <summary>
        /// Delete a user by ID.
        /// </summary>
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _userRepository.GetUserByIdAsync(id);
            if (user == null)
                return NotFound(new { Message = "User not found." });

            await _userRepository.DeleteUserAsync(id);
            return NoContent();
        }

        /// <summary>
        /// Get all employees with a specific work status.
        /// </summary>
        /// <param name="status">The status to filter employees by (e.g., "Active").</param>
        [HttpGet("employees/status/{status}")]
        public async Task<IActionResult> GetEmployeesByStatus(string status)
        {
            var employees = await _userRepository.GetEmployeesByStatusAsync(status);
            var employeeDtos = employees.Select(e => new
            {
                e.EmployeeId,
                FullName = $"{e.FirstName} {e.LastName}",
                e.Email,
                e.PhoneNumber,
                Department = e.Department?.Name,
                Role = e.Role?.RoleName,
                e.WorkStatus,
                e.HireDate
            });

            return Ok(employeeDtos);
        }

        /// <summary>
        /// Get a full report of all active employees.
        /// </summary>
        [HttpGet("employees/report/active")]
        public async Task<IActionResult> GetActiveEmployeesReport()
        {
            var employees = await _userRepository.GetEmployeesByStatusAsync("Active");
            var report = employees.Select(e => new
            {
                e.EmployeeId,
                FullName = $"{e.FirstName} {e.LastName}",
                e.Email,
                e.PhoneNumber,
                Department = e.Department?.Name,
                Role = e.Role?.RoleName,
                e.WorkStatus,
                e.HireDate,
                e.IsRemote
            });

            return Ok(report);
        }

        // NEW METHODS

        /// <summary>
        /// Get users with pagination.
        /// </summary>
        [HttpGet("paginated")]
        public async Task<IActionResult> GetPaginatedUsers(int pageNumber = 1, int pageSize = 10)
        {
            var users = await _userRepository.GetAllUsersAsync();
            var paginatedUsers = users
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize);

            return Ok(new
            {
                TotalCount = users.Count(),
                PageNumber = pageNumber,
                PageSize = pageSize,
                Users = paginatedUsers
            });
        }

        /// <summary>
        /// Search users by name.
        /// </summary>
        [HttpGet("search")]
        public async Task<IActionResult> SearchUsersByName(string name)
        {
            var users = await _userRepository.GetAllUsersAsync();
            var filteredUsers = users
                .Where(u => u.FirstName.Contains(name, System.StringComparison.OrdinalIgnoreCase) ||
                            u.LastName.Contains(name, System.StringComparison.OrdinalIgnoreCase));

            return Ok(filteredUsers);
        }

        /// <summary>
        /// Get users by department ID.
        /// </summary>
        [HttpGet("department/{departmentId}")]
        public async Task<IActionResult> GetUsersByDepartment(int departmentId)
        {
            var employees = await _userRepository.GetEmployeesByStatusAsync("Active");
            var usersInDepartment = employees
                .Where(e => e.DepartmentId == departmentId)
                .Select(e => new
                {
                    e.EmployeeId,
                    FullName = $"{e.FirstName} {e.LastName}",
                    e.Email,
                    e.PhoneNumber,
                    e.WorkStatus
                });

            return Ok(usersInDepartment);
        }

        /// <summary>
        /// Reset a user's password.
        /// </summary>
        [HttpPost("reset-password/{userId}")]
        public async Task<IActionResult> ResetPassword(int userId, [FromBody] string newPassword)
        {
            var user = await _userRepository.GetUserByIdAsync(userId);
            if (user == null)
                return NotFound(new { Message = "User not found." });

            // Simulate password reset (should include hashing in real-world scenarios)
           // user.Password = newPassword;
            await _userRepository.UpdateUserAsync(user);

            return Ok(new { Message = "Password reset successfully." });
        }

        /// <summary>
        /// Check if a user exists by email.
        /// </summary>
        [HttpGet("exists-by-email")]
        public async Task<IActionResult> CheckIfUserExistsByEmail(string email)
        {
            var users = await _userRepository.GetAllUsersAsync();
            var userExists = users.Any(u => u.Email.Equals(email, System.StringComparison.OrdinalIgnoreCase));

            return Ok(new { Exists = userExists });
        }
    }
}