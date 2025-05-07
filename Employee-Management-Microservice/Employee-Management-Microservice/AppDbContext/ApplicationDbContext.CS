using Employee_Management_Microservice.DTO.Employee_Management_Microservice.Models;
using Employee_Management_Microservice.Models;
using Microsoft.EntityFrameworkCore;

namespace Employee_Management_Microservice.ApplicationDbContext
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        // DbSet properties for each entity in the application
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Employee and Department relationship
            modelBuilder.Entity<Employee>()
                .HasOne(e => e.Department)
                .WithMany()
                .HasForeignKey(e => e.DepartmentId)
                .OnDelete(DeleteBehavior.Restrict); // Prevent cascading deletes

            // Employee and Role relationship
            modelBuilder.Entity<Employee>()
                .HasOne(e => e.Role)
                .WithMany()
                .HasForeignKey(e => e.RoleId)
                .OnDelete(DeleteBehavior.Restrict);

            // Employee and Manager relationship (self-referencing)
            modelBuilder.Entity<Employee>()
                .HasOne(e => e.Manager)
                .WithMany()
                .HasForeignKey(e => e.ReportingManager)
                .OnDelete(DeleteBehavior.Restrict);

            // Seed data for Roles and Departments
            modelBuilder.Entity<Role>().HasData(
                new Role { RoleId = 1, RoleName = "Admin" },
                new Role { RoleId = 2, RoleName = "Developer" },
                new Role { RoleId = 3, RoleName = "HR" }
            );

            modelBuilder.Entity<Department>().HasData(
                new Department { DepartmentId = 1, Name = "Engineering" },
                new Department { DepartmentId = 2, Name = "Human Resources" },
                new Department { DepartmentId = 3, Name = "Finance" }
            );

            base.OnModelCreating(modelBuilder);
        }
    }
}