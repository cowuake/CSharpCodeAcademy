using Library.Core.EFCore.EntityConfigurations;
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
        public DbSet<Account> Accounts { get; set; }

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

            builder.ApplyConfiguration(new BookConfiguration());
            builder.ApplyConfiguration(new BookGenreConfiguration());
            builder.ApplyConfiguration(new AccountConfiguration());
        }
    }
}