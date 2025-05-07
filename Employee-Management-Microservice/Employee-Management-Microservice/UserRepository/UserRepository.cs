using Employee_Management_Microservice.DTO.Employee_Management_Microservice.Models;
using Employee_Management_Microservice.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Employee_Management_Microservice.Repositories
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task<User> GetUserByIdAsync(int userId);
        Task<User> CreateUserAsync(User user);
        Task UpdateUserAsync(User user);
        Task DeleteUserAsync(int userId);
        Task<List<Employee>> GetEmployeesByStatusAsync(string status);

        // New Methods
        Task<Role> GetRoleByNameAsync(string roleName);
        Task<Department> GetDepartmentByIdAsync(int departmentId);
    }

    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext.ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext.ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<User> GetUserByIdAsync(int userId)
        {
            return await _context.Users.FindAsync(userId);
        }

        public async Task<User> CreateUserAsync(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task UpdateUserAsync(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteUserAsync(int userId)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user != null)
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
            }
        }

        /// <summary>
        /// Get all employees with a specific work status.
        /// </summary>
        /// <param name="status">The work status to filter employees by (e.g., "Active").</param>
        /// <returns>A list of employees with the specified work status.</returns>
        public async Task<List<Employee>> GetEmployeesByStatusAsync(string status)
        {
            return await _context.Employees
                .Include(e => e.Department) // Include related Department entity
                .Include(e => e.Role)      // Include related Role entity
                .Where(e => e.WorkStatus == status) // Filter by work status
                .ToListAsync();
        }

        /// <summary>
        /// Retrieve a Role by its name.
        /// </summary>
        /// <param name="roleName">The name of the role.</param>
        /// <returns>The Role object if found; otherwise, null.</returns>
        public async Task<Role> GetRoleByNameAsync(string roleName)
        {
            return await _context.Roles
                .FirstOrDefaultAsync(r => r.RoleName == roleName);
        }

        /// <summary>
        /// Retrieve a Department by its ID.
        /// </summary>
        /// <param name="departmentId">The ID of the department.</param>
        /// <returns>The Department object if found; otherwise, null.</returns>
        public async Task<Department> GetDepartmentByIdAsync(int departmentId)
        {
            return await _context.Departments
                .FirstOrDefaultAsync(d => d.DepartmentId == departmentId);
        }
    }
}