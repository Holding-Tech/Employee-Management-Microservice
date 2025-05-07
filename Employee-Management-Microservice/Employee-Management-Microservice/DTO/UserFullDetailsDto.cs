namespace Employee_Management_Microservice.DTO
{
    public class UserFullDetailsDto
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string RoleName { get; set; }
        public string RoleDescription { get; set; }
        public string DepartmentName { get; set; }
        public string DepartmentLocation { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsActive { get; set; }
        public decimal Salary { get; set; }
        public string JobTitle { get; set; }
        public bool IsFullTime { get; set; }
        public bool IsRemote { get; set; }
        public DateTime DateOfBirth { get; set; }
        public DateTime HireDate { get; set; }
        public string EmergencyContactName { get; set; }
        public string EmergencyContactNumber { get; set; }
        public string Address { get; set; }
    }
}