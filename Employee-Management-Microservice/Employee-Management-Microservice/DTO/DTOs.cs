namespace Employee_Management_Microservice.DTO
{
    using System;
    using System.ComponentModel.DataAnnotations;

    namespace Employee_Management_Microservice.Models
    {
        // User Model
        public class User
        {
            [Key]
            [Required]
            public int UserId { get; set; }

            [Required]
            [MaxLength(50)]
            public string FirstName { get; set; }

            [Required]
            [MaxLength(50)]
            public string LastName { get; set; }

            [Required]
            [EmailAddress]
            [MaxLength(100)]
            public string Email { get; set; }

            [Required]
            [MaxLength(100)]
            public string PasswordHash { get; set; }

            [Required]
            [MaxLength(50)]
            public string RoleName { get; set; } // e.g., Admin, HR, Developer, etc.

            public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

            public bool IsActive { get; set; } = true;
        }

        // DTOs

        // User Creation DTO
        public class UserCreateDto
        {
            [Required]
            [MaxLength(50)]
            public string FirstName { get; set; }

            [Required]
            [MaxLength(50)]
            public string LastName { get; set; }

            [Required]
            [EmailAddress]
            [MaxLength(100)]
            public string Email { get; set; }

            [Required]
            [MaxLength(100)]
            public string Password { get; set; }

            [Required]
            [MaxLength(50)]
            public string RoleName { get; set; }
        }

        // User Update DTO
        public class UserUpdateDto
        {
            [Required]
            public int UserId { get; set; }

            [MaxLength(50)]
            public string FirstName { get; set; }

            [MaxLength(50)]
            public string LastName { get; set; }

            [EmailAddress]
            [MaxLength(100)]
            public string Email { get; set; }

            [MaxLength(50)]
            public string RoleName { get; set; }

            public bool? IsActive { get; set; }
        }

        // User Read DTO
        public class UserReadDto
        {
            public int UserId { get; set; }

            public string FirstName { get; set; }

            public string LastName { get; set; }

            public string Email { get; set; }

            public string RoleName { get; set; }

            public DateTime CreatedAt { get; set; }

            public bool IsActive { get; set; }
        }
    }
}
