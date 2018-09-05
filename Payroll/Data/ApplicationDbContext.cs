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

            base.OnModelCreating(modelBuilder);
        }



        public DbSet<Payroll.Models.Currency> Currency { get; set; }
        public DbSet<Payroll.Models.Occupation> Occupation { get; set; }        
        public DbSet<Payroll.Models.LicenseType> TipoLicenca { get; set; }
    }
}
