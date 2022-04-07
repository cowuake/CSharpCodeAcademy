using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Text;
using TicketingEFCore.Entities;
using TicketingEFCore.EFCore.Configs;

namespace TicketingEFCore.EFCore
{
    public class DataContext : DbContext
    {
        public DataContext() : base() { }
            //> Database.EnsureCreated();

        public DataContext(DbContextOptions<DataContext> options) { }

        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            builder.LogTo(s => System.Diagnostics.Debug.WriteLine(s));
            builder.UseSqlServer(@"Server=.\SQLEXPRESS;Initial Catalog=ticketing;Integrated Security=true;MultipleActiveResultSets=true");
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.HasDefaultSchema("dbo");

            builder.Entity<Ticket>()
                .ToTable("ticket");

            builder.Entity<Category>()
                .ToTable("category");

            builder.ApplyConfiguration(new TicketConfig());
            builder.ApplyConfiguration(new CategoryConfig());
        }
    }
}
