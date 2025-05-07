namespace Employee_Management_Microservice.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Employee
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int EmployeeId { get; set; }

        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(50)]
        public string LastName { get; set; }

        [ForeignKey("Department")]
        public int DepartmentId { get; set; }
        public Department Department { get; set; }

        [ForeignKey("ReportingManager")]
        public int? ReportingManager { get; set; }
        public Employee Manager { get; set; }

        [ForeignKey("Role")]
        public int RoleId { get; set; }
        public Role Role { get; set; }

        [Required]
        [MaxLength(20)]
        public string CompanyId { get; set; }

        [Required]
        [EmailAddress]
        [MaxLength(100)]
        public string Email { get; set; }

        [Phone]
        [MaxLength(15)]
        public string PhoneNumber { get; set; }

        [Required]
        public DateTime HireDate { get; set; }

        [MaxLength(200)]
        public string ContractTerm { get; set; }

        [Required]
        public DateTime FirstDayOfWork { get; set; }

        public DateTime? LastDayOfWork { get; set; }

        [MaxLength(200)]
        public string WorkStatus { get; set; }

        [MaxLength(50)]
        public string ShiftType { get; set; }

        [MaxLength(50)]
        public string WorkAuthorization { get; set; }

        [Required]
        public DateTime ProbationEndDate { get; set; }

        [MaxLength(200)]
        public string Skills { get; set; }

        public bool IsRemote { get; set; }

        [Required]
        public DateTime ContractStartDate { get; set; }

        public DateTime? ContractEndDate { get; set; }

        // Additional Properties

        [Required]
        public DateTime DateOfBirth { get; set; } // Employee's Date of Birth

        [MaxLength(100)]
        public string EmergencyContactName { get; set; } // Emergency Contact Person Name

        [Phone]
        [MaxLength(15)]
        public string EmergencyContactNumber { get; set; } // Emergency Contact Phone Number

        [MaxLength(100)]
        public string Address { get; set; } // Employee's Residential Address

        public decimal Salary { get; set; } // Employee's Salary

        [MaxLength(50)]
        public string Nationality { get; set; } // Employee's Nationality

        public bool IsFullTime { get; set; } // Whether the employee is full-time or part-time

        public string ProfilePictureUrl { get; set; } // URL to the employee's profile picture
    }

    public class Department
    {
        [Key]
        public int DepartmentId { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
    }

    public class Role
    {
        [Key]
        public int RoleId { get; set; }

        [Required]
        [MaxLength(50)]
        public string RoleName { get; set; }
    }
}