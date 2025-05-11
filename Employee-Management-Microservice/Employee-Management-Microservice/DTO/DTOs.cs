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
         
            public int UserId { get; set; }

            public string FirstName { get; set; }

          
            public string LastName { get; set; }

          
            public string Email { get; set; }

          
            public string PasswordHash { get; set; }

            
            public string RoleName { get; set; } // e.g., Admin, HR, Developer, etc.

            public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

            public bool IsActive { get; set; } = true;
        }

        // DTOs
        public class EmployeeReportDto
        {
            public int EmployeeId { get; set; } // Unique ID of the employee
            public string FirstName { get; set; } // First name of the employee
            public string LastName { get; set; } // Last name of the employee
            public string DepartmentName { get; set; } // Department to which the employee belongs
            public string Email { get; set; } // Email address of the employee
            public string PhoneNumber { get; set; } // Phone number of the employee
            public string WorkStatus { get; set; } // Work status (e.g., Active, Retired)
            public DateTime HireDate { get; set; } // Date the employee was hired
            public string RoleName { get; set; } // Role of the employee in the company
            public bool IsRemote { get; set; } // Indicates if the employee is remote
        }
        // User Creation DTO
        namespace Employee_Management_Microservice.DTO
        {
            using System;
            using System.ComponentModel.DataAnnotations;

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

                [Required]
                public int DepId { get; set; } // Department ID

                [Required]
                public decimal Salary { get; set; } // User's Salary

                [Required]
                [MaxLength(100)]
                public string JobTitle { get; set; } // Job title (e.g., Software Engineer, HR Manager)

                [Required]
                public bool IsFullTime { get; set; } // Whether the user is full-time or part-time

                [Required]
                public bool IsRemote { get; set; } // Indicates if the user is working remotely

                [Required]
                public DateTime DateOfBirth { get; set; } // User's Date of Birth

                [Required]
                public DateTime HireDate { get; set; } // Date the user was hired

                [Required]
                [MaxLength(100)]
                public string EmergencyContactName { get; set; } // Emergency Contact Person Name

                [Required]
                [MaxLength(15)]
                public string EmergencyContactNumber { get; set; } // Emergency Contact Phone Number

                [Required]
                [MaxLength(200)]
                public string Address { get; set; } // User's Residential Address
            }
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
