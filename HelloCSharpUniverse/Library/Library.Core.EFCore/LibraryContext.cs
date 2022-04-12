using Library.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration; // Remember to add Microsoft.Extensions.Configuration.Json
using System.IO;

namespace Library.Core.EFCore
{
    public class LibraryContext : DbContext
    {
        public DbSet<Book> Books { get; set; }

        public LibraryContext() : base() { }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            if (!builder.IsConfigured)
            {
                IConfigurationRoot config = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json")
                    .Build();

                string connectionStringSQL = config.GetConnectionString("library");

                builder.UseSqlServer(connectionStringSQL);
            }
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.HasDefaultSchema("dbo");

            builder.Entity<Book>()
                .ToTable("book")
                .HasKey(b => b.ISBN);

            builder.Entity<Book>()
                .Property(b => b.Title)
                .HasColumnName("title")
                .HasColumnType("VARCHAR")
                .HasMaxLength(100)
                .IsRequired();

            builder.Entity<Book>()
                .Property(b => b.Summary)
                .HasColumnName("description")
                .HasColumnType("VARCHAR")
                .HasMaxLength(250)
                .IsRequired();

            builder.Entity<Book>()
                .Property(b => b.Author)
                .HasColumnName("author")
                .HasColumnType("VARCHAR")
                .HasMaxLength(25)
                .IsRequired();
        }
    }
}