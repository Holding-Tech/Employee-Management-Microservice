namespace Employee_Management_Microservice.DTO
{
    public class EmployeeCreateDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int DepartmentId { get; set; }
        public int? ReportingManager { get; set; }
        public int RoleId { get; set; }
        public string CompanyId { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime HireDate { get; set; }
        public string ContractTerm { get; set; }
        public DateTime FirstDayOfWork { get; set; }
        public DateTime? LastDayOfWork { get; set; }
        public string WorkStatus { get; set; }
        public string ShiftType { get; set; }
        public string WorkAuthorization { get; set; }
        public DateTime ProbationEndDate { get; set; }
        public string Skills { get; set; }
        public bool IsRemote { get; set; }
        public DateTime ContractStartDate { get; set; }
        public DateTime? ContractEndDate { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string EmergencyContactName { get; set; }
        public string EmergencyContactNumber { get; set; }
        public string Address { get; set; }
        public decimal Salary { get; set; }
        public string Nationality { get; set; }
        public bool IsFullTime { get; set; }
        public string ProfilePictureUrl { get; set; }
        public string JobTitle { get; set; }
        public bool IsOnLeave { get; set; }
        public DateTime? LastPromotionDate { get; set; }
        public string PerformanceRating { get; set; }
        public bool IsEligibleForRehire { get; set; }
    }
    public class EmployeeReadDto
    {
        public int EmployeeId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string JobTitle { get; set; }
        public string DepartmentName { get; set; }
        public string RoleName { get; set; }
        public bool IsActive { get; set; } // Based on WorkStatus
    }
}
