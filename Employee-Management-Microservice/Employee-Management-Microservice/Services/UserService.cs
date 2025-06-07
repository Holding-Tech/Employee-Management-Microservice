using Employee_Management_Microservice.DTO;
using Employee_Management_Microservice.Models;
using Employee_Management_Microservice.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Employee_Management_Microservice.Services
{

    public interface IEmployeeService
    {
        Task<IEnumerable<EmployeeReadDto>> GetAllEmployeesAsync();
        Task<EmployeeReadDto> GetEmployeeByIdAsync(int employeeId);
        Task<EmployeeReadDto> CreateEmployeeAsync(EmployeeCreateDto employeeDto);
        Task UpdateEmployeeAsync(int id, EmployeeCreateDto employeeDto);
        Task DeleteEmployeeAsync(int employeeId);
    }
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _repository;

        public EmployeeService(IEmployeeRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<EmployeeReadDto>> GetAllEmployeesAsync()
        {
            var employees = await _repository.GetAllEmployeesAsync();

            var employeeDtos = new List<EmployeeReadDto>();
            foreach (var emp in employees)
            {
                var department = await _repository.GetDepartmentByIdAsync(emp.DepartmentId);
                var role = await _repository.GetRoleByIdAsync(emp.RoleId);

                employeeDtos.Add(new EmployeeReadDto
                {
                    EmployeeId = emp.EmployeeId,
                    FirstName = emp.FirstName,
                    LastName = emp.LastName,
                    Email = emp.Email,
                    JobTitle = emp.JobTitle,
                    DepartmentName = department?.Name,
                    RoleName = role?.RoleName,
                    IsActive = emp.WorkStatus == "Active"
                });
            }

            return employeeDtos;
        }

        public async Task<EmployeeReadDto> GetEmployeeByIdAsync(int employeeId)
        {
            var emp = await _repository.GetEmployeeByIdAsync(employeeId);
            if (emp == null) return null;

            var department = await _repository.GetDepartmentByIdAsync(emp.DepartmentId);
            var role = await _repository.GetRoleByIdAsync(emp.RoleId);

            return new EmployeeReadDto
            {
                EmployeeId = emp.EmployeeId,
                FirstName = emp.FirstName,
                LastName = emp.LastName,
                Email = emp.Email,
                JobTitle = emp.JobTitle,
                DepartmentName = department?.Name,
                RoleName = role?.RoleName,
                IsActive = emp.WorkStatus == "Active"
            };
        }

        public async Task<EmployeeReadDto> CreateEmployeeAsync(EmployeeCreateDto dto)
        {
            var employee = new Employee
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                DepartmentId = dto.DepartmentId,
                ReportingManager = dto.ReportingManager,
                RoleId = dto.RoleId,
                CompanyId = dto.CompanyId,
                Email = dto.Email,
                PhoneNumber = dto.PhoneNumber,
                HireDate = dto.HireDate,
                ContractTerm = dto.ContractTerm,
                FirstDayOfWork = dto.FirstDayOfWork,
                LastDayOfWork = dto.LastDayOfWork,
                WorkStatus = dto.WorkStatus,
                ShiftType = dto.ShiftType,
                WorkAuthorization = dto.WorkAuthorization,
                ProbationEndDate = dto.ProbationEndDate,
                Skills = dto.Skills,
                IsRemote = dto.IsRemote,
                ContractStartDate = dto.ContractStartDate,
                ContractEndDate = dto.ContractEndDate,
                DateOfBirth = dto.DateOfBirth,
                EmergencyContactName = dto.EmergencyContactName,
                EmergencyContactNumber = dto.EmergencyContactNumber,
                Address = dto.Address,
                Salary = dto.Salary,
                Nationality = dto.Nationality,
                IsFullTime = dto.IsFullTime,
                ProfilePictureUrl = dto.ProfilePictureUrl,
                JobTitle = dto.JobTitle,
                IsOnLeave = dto.IsOnLeave,
                LastPromotionDate = dto.LastPromotionDate,
                PerformanceRating = dto.PerformanceRating,
                IsEligibleForRehire = dto.IsEligibleForRehire
            };

            var created = await _repository.CreateEmployeeAsync(employee);
            var department = await _repository.GetDepartmentByIdAsync(created.DepartmentId);
            var role = await _repository.GetRoleByIdAsync(created.RoleId);

            return new EmployeeReadDto
            {
                EmployeeId = created.EmployeeId,
                FirstName = created.FirstName,
                LastName = created.LastName,
                Email = created.Email,
                JobTitle = created.JobTitle,
                DepartmentName = department?.Name,
                RoleName = role?.RoleName,
                IsActive = created.WorkStatus == "Active"
            };
        }

        public async Task UpdateEmployeeAsync(int id, EmployeeCreateDto dto)
        {
            var employee = await _repository.GetEmployeeByIdAsync(id);
            if (employee == null) return;

            // Update fields
            employee.FirstName = dto.FirstName;
            employee.LastName = dto.LastName;
            employee.Email = dto.Email;
            // ... other fields as needed

            await _repository.UpdateEmployeeAsync(employee);
        }

        public async Task DeleteEmployeeAsync(int employeeId)
        {
            await _repository.DeleteEmployeeAsync(employeeId);
        }
    }
}
