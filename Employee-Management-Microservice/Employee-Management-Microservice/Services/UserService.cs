using Employee_Management_Microservice.DTO;
using Employee_Management_Microservice.DTO.Employee_Management_Microservice.Models;
using Employee_Management_Microservice.DTO.Employee_Management_Microservice.Models.Employee_Management_Microservice.DTO;
using Employee_Management_Microservice.Models;
using Employee_Management_Microservice.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Employee_Management_Microservice.Services
{
    public interface IUserService
    {
        Task<IEnumerable<UserReadDto>> GetAllUsersAsync();
        Task<UserReadDto> GetUserByIdAsync(int userId);
        Task<UserFullDetailsDto> CreateUserAsync(UserCreateDto userCreateDto);
        Task UpdateUserAsync(UserUpdateDto userUpdateDto);
        Task DeleteUserAsync(int userId);
        Task<List<EmployeeReportDto>> GetActiveEmployeesReportAsync();
    }

    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<IEnumerable<UserReadDto>> GetAllUsersAsync()
        {
            var users = await _userRepository.GetAllUsersAsync();
            var userDtos = new List<UserReadDto>();

            foreach (var user in users)
            {
                userDtos.Add(new UserReadDto
                {
                    UserId = user.UserId,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    RoleName = user.RoleName,
                    CreatedAt = user.CreatedAt,
                    IsActive = user.IsActive
                });
            }

            return userDtos;
        }

        public async Task<UserReadDto> GetUserByIdAsync(int userId)
        {
            var user = await _userRepository.GetUserByIdAsync(userId);
            if (user == null) return null;

            return new UserReadDto
            {
                UserId = user.UserId,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                RoleName = user.RoleName,
                CreatedAt = user.CreatedAt,
                IsActive = user.IsActive
            };
        }

        public async Task<UserFullDetailsDto> CreateUserAsync(UserCreateDto userCreateDto)
        {
            // Create and initialize a User object
            var user = new User
            {
                FirstName = userCreateDto.FirstName,
                LastName = userCreateDto.LastName,
                Email = userCreateDto.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(userCreateDto.Password), // Hash the password securely
                RoleName = userCreateDto.RoleName,
                CreatedAt = DateTime.UtcNow,
                IsActive = true // Default the user to active
            };

            // Save the user to the repository
            await _userRepository.CreateUserAsync(user);

            // Fetch related role and department details
            var role = await _userRepository.GetRoleByNameAsync(user.RoleName);
            var department = await _userRepository.GetDepartmentByIdAsync(userCreateDto.DepId);

            // Return the UserFullDetailsDto with all relevant details
            return new UserFullDetailsDto
            {
                UserId = user.UserId,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                RoleName = user.RoleName,
                RoleDescription = role?.RoleDescription,
                DepartmentName = department?.Name,
                DepartmentLocation = department?.Location,
                CreatedAt = user.CreatedAt,
                IsActive = user.IsActive,
                Salary = userCreateDto.Salary,
                JobTitle = userCreateDto.JobTitle,
                IsFullTime = userCreateDto.IsFullTime,
                IsRemote = userCreateDto.IsRemote,
                DateOfBirth = userCreateDto.DateOfBirth,
                HireDate = userCreateDto.HireDate,
                EmergencyContactName = userCreateDto.EmergencyContactName,
                EmergencyContactNumber = userCreateDto.EmergencyContactNumber,
                Address = userCreateDto.Address
            };
        }

        public async Task UpdateUserAsync(UserUpdateDto userUpdateDto)
        {
            var user = await _userRepository.GetUserByIdAsync(userUpdateDto.UserId);
            if (user == null) return;

            user.FirstName = userUpdateDto.FirstName ?? user.FirstName;
            user.LastName = userUpdateDto.LastName ?? user.LastName;
            user.Email = userUpdateDto.Email ?? user.Email;
            user.RoleName = userUpdateDto.RoleName ?? user.RoleName;
            if (userUpdateDto.IsActive.HasValue)
            {
                user.IsActive = userUpdateDto.IsActive.Value;
            }

            await _userRepository.UpdateUserAsync(user);
        }

        public async Task DeleteUserAsync(int userId)
        {
            await _userRepository.DeleteUserAsync(userId);
        }

        public async Task<List<EmployeeReportDto>> GetActiveEmployeesReportAsync()
        {
            var activeEmployees = await _userRepository.GetEmployeesByStatusAsync("Active");
            return activeEmployees.Select(e => new EmployeeReportDto
            {
                EmployeeId = e.EmployeeId,
                FirstName = e.FirstName,
                LastName = e.LastName,
                DepartmentName = e.Department.Name,
                Email = e.Email,
                PhoneNumber = e.PhoneNumber,
                WorkStatus = e.WorkStatus,
                HireDate = e.HireDate
            }).ToList();
        }
    }
}