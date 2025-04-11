using HRM.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace HRM.Persistance
{
    public class HRMDbContext(DbContextOptions<HRMDbContext> options) : DbContext(options)
    {
        public DbSet<Department> Departments { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Salary> Salaries { get; set; }
        public DbSet<ProjectEmployee> ProjectEmployees { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            modelBuilder.Entity<Employee>()
                .HasOne<Salary>(e => e.Salary)
                .WithOne(s => s.Employee)
                .HasForeignKey<Salary>(s => s.EmployeeId);

            modelBuilder.Entity<Employee>()
                .HasOne<Department>(e => e.Department)
                .WithMany(d => d.Employees)
                .HasForeignKey(e => e.DepartmentId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder
                .Entity<ProjectEmployee>()
                .HasOne<Employee>(pe => pe.Employee)
                .WithMany(e => e.ProjectEmployees)
                .HasForeignKey(pe => pe.EmployeeId);

            modelBuilder
                .Entity<ProjectEmployee>()
                .HasOne<Project>(pe => pe.Project)
                .WithMany(p => p.ProjectEmployees)
                .HasForeignKey(pe => pe.ProjectId);
        }
    }
}
