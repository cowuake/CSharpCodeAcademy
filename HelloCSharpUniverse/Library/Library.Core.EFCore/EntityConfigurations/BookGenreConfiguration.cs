using Library.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Core.EFCore.EntityConfigurations
{
    public class BookGenreConfiguration : IEntityTypeConfiguration<BookGenre>
    {
        public void Configure(EntityTypeBuilder<BookGenre> builder)
        {
            builder
                .ToTable("book_category")
                .HasKey(c => c.Id);

            builder
                .Property(c => c.Name) // REQUIRED!
                .HasColumnName("name")
                .HasColumnType("VARCHAR(50)") // Length not needed in newer EF releases
                .HasMaxLength(50)
                .IsRequired();
        }
    }
}