using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Text;
using ECommerce.Core.EF;
using ECommerce.Core.EF.Entities;
using ECommerce.Core.EF.Configs;
using System.IO;

namespace ECommerce.Core.EF
{
    public class DataContext : DbContext
    {
        public DataContext() : base()
            => Database.EnsureCreated();

        public DataContext(DbContextOptions<DataContext> options) { }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderLine> OrderLines { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            if (!builder.IsConfigured)
            {
                IConfigurationRoot config = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json")
                    .Build();

                string connectionStringSQL = config.GetConnectionString("ecommerce");

                builder.UseSqlServer(connectionStringSQL);
            }
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.HasDefaultSchema("dbo");

            builder.Entity<Product>()
                .ToTable("product");

            builder.Entity<Category>()
                .ToTable("category");

            builder.Entity<Order>()
                .ToTable("order");

            builder.Entity<OrderLine>()
                .ToTable("orderline");

            builder.ApplyConfiguration(new ProductConfig());
            builder.ApplyConfiguration(new CategoryConfig());
            builder.ApplyConfiguration(new OrderConfig());
            builder.ApplyConfiguration(new OrderLineConfig());
        }
    }
}