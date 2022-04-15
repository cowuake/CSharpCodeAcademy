using CustomerOrderManagement.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace CustomerOrderManagement.Core.EF.DataContext
{
    public class CustomerOrderManagementContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }

        public CustomerOrderManagementContext() : base()
        {
            Database.EnsureCreated();
        }

        // Needed in order to eventually pass the options externally from a Web API!
        public CustomerOrderManagementContext(DbContextOptions<CustomerOrderManagementContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            if (!builder.IsConfigured)
            {
                string connectionStringSQL;

                //IConfigurationRoot config = new ConfigurationBuilder()
                //    .SetBasePath(Directory.GetCurrentDirectory())
                //    .AddJsonFile("appsettings.json")
                //    .Build();

                //connectionStringSQL = config.GetConnectionString("customer_order_management");

                //builder.UseSqlServer(connectionStringSQL);

                connectionStringSQL =
                    "Data Source=.\\SQLEXPRESS;Initial Catalog=customer_order_management;Integrated Security=True;MultipleActiveResultSets=true;";

                builder.UseSqlServer(connectionStringSQL);
            }
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.HasDefaultSchema("dbo");

            builder.Entity<Customer>()
                .ToTable("customer");

            builder.Entity<Order>()
                .ToTable("order");

            builder.ApplyConfiguration(new CustomerConfig());
            builder.ApplyConfiguration(new OrderConfig());
        }
    }
}