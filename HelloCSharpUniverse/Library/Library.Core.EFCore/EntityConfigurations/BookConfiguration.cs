using Library.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Core.EFCore.EntityConfigurations
{
    public class BookConfiguration : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder
                .ToTable("book")
                .HasKey(b => b.ISBN)
                .HasName("isbn");

            builder
                .Property(b => b.Title) // REQUIRED!
                .HasColumnName("title")
                .HasColumnType("VARCHAR(MAX)") // Length not needed in newer EF releases
                .IsRequired();

            builder
                .Property(b => b.Summary) // Not required
                .HasColumnName("summary")
                .HasColumnType("VARCHAR(MAX)"); // Length not needed in newer EF releases

            builder
                .Property(b => b.Author) // REQUIRED!
                .HasColumnName("author")
                .HasColumnType("VARCHAR(100)") // Length not needed in newer EF releases
                .HasMaxLength(100)
                .IsRequired();

            builder
                .Property(b => b.Language) // Not required
                .HasColumnName("language")
                .HasColumnType("VARCHAR(25)") // Length not needed in newer EF releases
                .HasMaxLength(25);

            builder
                .Property(b => b.Year) // Not required
                .HasColumnName("year")
                .HasColumnType("INT");

            builder
                .Property(b => b.Publisher) // Not required
                .HasColumnName("publisher")
                .HasColumnType("VARCHAR(50)")
                .HasMaxLength(50);

            builder
                .Property(b => b.Edition) // Not required
                .HasColumnName("edition")
                .HasColumnType("INT");

            builder
                .Property(b => b.Pages) // Not required
                .HasColumnName("pages")
                .HasColumnType("INT");

            builder
                .Property(b => b.Note) // Not required
                .HasColumnName("note")
                .HasColumnType("VARCHAR(250)") // Length not needed in newer EF releases
                .HasMaxLength(250);

            builder
                .Property(b => b.BookGenreId)
                .HasColumnName("book_genre_id");

            builder
                .HasOne(b => b.BookGenre)
                .WithMany(g => g.Books)
                .HasForeignKey(b => b.BookGenreId);
        }
    }
}