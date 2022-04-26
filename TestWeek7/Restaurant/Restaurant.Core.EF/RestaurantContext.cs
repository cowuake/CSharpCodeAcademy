using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Restaurant.Core;
using Restaurant.Core.EF.EntityConfigurations;
using Restaurant.Core.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Restaurant.Core.EF
{
    public class RestaurantContext : DbContext
    {
        public DbSet<Dish> Dishes { get; set; }
        public DbSet<Account> Accounts { get; set; }

        public RestaurantContext() : base()
        {
            Database.EnsureCreated();
        }

        public RestaurantContext(DbContextOptions<RestaurantContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            if (!builder.IsConfigured)
            {
                string connectionStringSQL;

                IConfigurationRoot config = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile(Constants.APPLICATION_SETTINGS_FILE)
                    .Build();

                connectionStringSQL = config.GetConnectionString(Constants.CONNECTION_STRING_NAME);

                builder.UseSqlServer(connectionStringSQL);
            }
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder
                .HasDefaultSchema("dbo");

            builder.ApplyConfiguration(new DishConfiguration());
            builder.ApplyConfiguration(new AccountConfiguration());
        }
    }
}
