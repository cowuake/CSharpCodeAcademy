using Library.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration; // Remember to add Microsoft.Extensions.Configuration.Json
using System.IO;

namespace Library.Core.EFCore
{
    public class LibraryContext : DbContext
    {
        public DbSet<Book> Books { get; set; }
        public DbSet<BookGenre> BookGenres { get; set; }

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
            builder
                .HasDefaultSchema("dbo");

            builder
                .Entity<Book>()
                .ToTable("book")
                .HasKey(b => b.ISBN);

            builder
                .Entity<Book>()
                .Property(b => b.Title) // REQUIRED!
                .HasColumnName("title")
                .HasColumnType("VARCHAR(MAX)") // Length not needed in newer EF releases
                .IsRequired();

            builder
                .Entity<Book>()
                .Property(b => b.Summary) // Not required
                .HasColumnName("summary")
                .HasColumnType("VARCHAR(MAX)"); // Length not needed in newer EF releases

            builder
                .Entity<Book>()
                .Property(b => b.Author) // REQUIRED!
                .HasColumnName("author")
                .HasColumnType("VARCHAR(100)") // Length not needed in newer EF releases
                .HasMaxLength(100)
                .IsRequired();

            builder
                .Entity<Book>()
                .Property(b => b.Language) // Not required
                .HasColumnName("language")
                .HasColumnType("VARCHAR(25)") // Length not needed in newer EF releases
                .HasMaxLength(25);

            builder
                .Entity<Book>()
                .Property(b => b.Year) // Not required
                .HasColumnName("year")
                .HasColumnType("INT");

            builder
                .Entity<Book>() 
                .Property(b => b.Publisher) // Not required
                .HasColumnName("publisher")
                .HasColumnType("VARCHAR(50)")
                .HasMaxLength(50);

            builder
                .Entity<Book>()
                .Property(b => b.Edition) // Not required
                .HasColumnName("edition")
                .HasColumnType("INT");

            builder
                .Entity<Book>()
                .Property(b => b.Pages) // Not required
                .HasColumnName("pages")
                .HasColumnType("INT");

            builder
                .Entity<Book>()
                .Property(b => b.Note) // Not required
                .HasColumnName("note")
                .HasColumnType("VARCHAR(250)") // Length not needed in newer EF releases
                .HasMaxLength(250);

            builder
               .Entity<BookGenre>()
               .ToTable("book_category")
               .HasKey(c => c.Id);

            builder
                .Entity<BookGenre>()
                .Property(c => c.Name) // REQUIRED!
                .HasColumnName("name")
                .HasColumnType("VARCHAR(50)") // Length not needed in newer EF releases
                .HasMaxLength(50)
                .IsRequired();

            builder
                .Entity<Book>()
                .Property(b => b.BookGenreId)
                .HasColumnName("book_genre_id");

            builder
                .Entity<BookGenre>()
                .HasMany(c => c.Books)
                .WithOne(b => b.BookGenre)
                .HasForeignKey(b => b.BookGenreId);
        }
    }
}