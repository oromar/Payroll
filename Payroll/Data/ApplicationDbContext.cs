using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Payroll.Models;

namespace Payroll.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Currency>()
                .HasIndex(p => new { p.Id, p.SearchFields })
                .IsUnique(true);

            modelBuilder.Entity<Occupation>()
                .HasIndex(p => new { p.Id, p.SearchFields })
                .IsUnique(true);

            modelBuilder.Entity<LicenseType>()
                .HasIndex(p => new { p.Id, p.SearchFields })
                .IsUnique(true);

            modelBuilder.Entity<Company>()
                .HasIndex(p => new { p.Id, p.SearchFields })
                .IsUnique(true);

            modelBuilder.Entity<Workplace>()
                .HasIndex(p => new { p.Id, p.SearchFields })
                .IsUnique(true);

            modelBuilder.Entity<JobRole>()
                .HasIndex(p => new { p.Id, p.SearchFields })
                .IsUnique(true);

            modelBuilder.Entity<Function>()
                .HasIndex(p => new { p.Id, p.SearchFields })
                .IsUnique(true);

            modelBuilder.Entity<OccurrenceType>()
               .HasIndex(p => new { p.Id, p.SearchFields })
               .IsUnique(true);

            modelBuilder.Entity<Employee>()
               .HasIndex(p => new { p.Id, p.SearchFields })
               .IsUnique(true);

            modelBuilder.Entity<Project>()
               .HasIndex(p => new { p.Id, p.SearchFields })
               .IsUnique(true);

            modelBuilder.Entity<Department>()
                .HasIndex(p => new { p.Id, p.SearchFields })
                .IsUnique(true);

            modelBuilder.Entity<ProjectEmployee>()
                .HasKey(p => new { p.ProjectId, p.EmployeeId });

            modelBuilder.Entity<ProjectEmployee>()
                .HasOne(p => p.Project)
                .WithMany(p => p.Employees)
                .HasForeignKey(p => p.ProjectId);

            modelBuilder.Entity<ProjectEmployee>()
                .HasOne(p => p.Employee)
                .WithMany(p => p.Projects)
                .HasForeignKey(p => p.EmployeeId);

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Currency> Currency { get; set; }
        public DbSet<Occupation> Occupation { get; set; }        
        public DbSet<LicenseType> LicenseType { get; set; }
        public DbSet<Company> Company { get; set; }
        public DbSet<Workplace> Workplace { get; set; }
        public DbSet<JobRole> JobRole { get; set; }
        public DbSet<Function> Function { get; set; }
        public DbSet<Payroll.Models.OccurrenceType> OccurrenceType { get; set; }
        public DbSet<Payroll.Models.Employee> Employee { get; set; }
        public DbSet<Payroll.Models.Department> Department { get; set; }
        public DbSet<Payroll.Models.EmployeeHistory> EmployeeHistory { get; set; }
        public DbSet<Payroll.Models.Project> Project { get; set; }
        public DbSet<Payroll.Models.ProjectEmployee> ProjectEmployee { get; set; }
    }
}
