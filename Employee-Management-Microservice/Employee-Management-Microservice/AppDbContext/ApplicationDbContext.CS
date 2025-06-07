
using Employee_Management_Microservice.Models;
using Microsoft.EntityFrameworkCore;

namespace Employee_Management_Microservice
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        // DbSet properties for each entity in the application
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Role> Roles { get; set; }
    

       
    }
}