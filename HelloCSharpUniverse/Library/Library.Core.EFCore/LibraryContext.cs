using Library.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration; // Remember to add Microsoft.Extensions.Configuration.Json
using System.IO;

namespace Library.Core.EFCore
{
    public class LibraryContext : DbContext
    {
        public DbSet<Book> Books { get; set; }

        public LibraryContext() : base()
        {
            Database.EnsureCreated();
        }

        // Needed in order to eventually pass the options externally from a Web API!
        public LibraryContext(DbContextOptions<LibraryContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            if (!builder.IsConfigured)
            {
                string connectionStringSQL;

                IConfigurationRoot config = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json")
                    .Build();

                connectionStringSQL = config.GetConnectionString("library");

                builder.UseSqlServer(connectionStringSQL);

                //connectionStringSQL =
                //    "Data Source=.\\SQLEXPRESS;Initial Catalog=library;Integrated Security=True;MultipleActiveResultSets=true;";

                //builder.UseSqlServer(connectionStringSQL);
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
                .HasColumnType("VARCHAR(100)") // Length not needed in newer EF releases
                .HasMaxLength(100)
                .IsRequired();

            builder.Entity<Book>()
                .Property(b => b.Summary)
                .HasColumnName("description")
                .HasColumnType("VARCHAR(250)") // Length not needed in newer EF releases
                .HasMaxLength(250)
                .IsRequired();

            builder.Entity<Book>()
                .Property(b => b.Author)
                .HasColumnName("author")
                .HasColumnType("VARCHAR(25)") // Length not needed in newer EF releases
                .HasMaxLength(25)
                .IsRequired();
        }
    }
}