using Employees.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Employees.Core.EF
{
    public class EmployeeContext : DbContext
    {
        public DbSet<Employee> Employees { get; set; }

        #region ========================= Ctors =========================
        public EmployeeContext() : base() { }

        public EmployeeContext(DbContextOptions<EmployeeContext> options) : base(options) { }
        #endregion ========================= Ctors =========================

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            if (!builder.IsConfigured)
            {
                IConfigurationRoot config = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json")
                    .Build();

                string connectionStringSQL = config.GetConnectionString("employees");

                builder.UseSqlServer(connectionStringSQL);
            }
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.HasDefaultSchema("dbo");

            builder.Entity<Employee>()
                .ToTable("employee");

            builder.Entity<Employee>().HasKey(e => e.Id);

            builder.Entity<Employee>()
                .Property(e => e.Code)
                .HasColumnName("code")
                .HasColumnType("VARCHAR")
                .HasMaxLength(10)
                .IsRequired();

            builder.Entity<Employee>()
                .Property(e => e.FirstName)
                .HasColumnName("first_name")
                .HasColumnType("VARCHAR")
                .HasMaxLength(25)
                .IsRequired();

            builder.Entity<Employee>()
                .Property(e => e.LastName)
                .HasColumnName("last_name")
                .HasColumnType("VARCHAR")
                .HasMaxLength(25)
                .IsRequired();
        }
    }
}