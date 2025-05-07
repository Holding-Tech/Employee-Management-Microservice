using Employee_Management_Microservice.DTO.Employee_Management_Microservice.Models;
using Employee_Management_Microservice.Models;
using Employee_Management_Microservice.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Employee_Management_Microservice.Services
{
    public interface IUserService
    {
        Task<IEnumerable<UserReadDto>> GetAllUsersAsync();
        Task<UserReadDto> GetUserByIdAsync(int userId);
        Task<UserReadDto> CreateUserAsync(UserCreateDto userCreateDto);
        Task UpdateUserAsync(UserUpdateDto userUpdateDto);
        Task DeleteUserAsync(int userId);
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

        public async Task<UserReadDto> CreateUserAsync(UserCreateDto userCreateDto)
        {
            var user = new User
            {
                FirstName = userCreateDto.FirstName,
                LastName = userCreateDto.LastName,
                Email = userCreateDto.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(userCreateDto.Password), // Hash the password
                RoleName = userCreateDto.RoleName
            };

            await _userRepository.CreateUserAsync(user);

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
    }
}