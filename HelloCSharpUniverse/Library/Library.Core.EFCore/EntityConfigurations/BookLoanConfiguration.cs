using Library.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Core.EFCore.EntityConfigurations
{
    public class BookLoanConfiguration : IEntityTypeConfiguration<BookLoan>
    {
        public void Configure(EntityTypeBuilder<BookLoan> builder)
        {
            builder
                .ToTable("book_loan")
                .HasKey(l => new { l.AccountId, l.BookIsbn });

            builder
                .Property(l => l.StartTime)
                .IsRequired()
                .HasColumnName("start_time")
                .HasColumnType("DATETIME");

            builder
                .Property(l => l.EndTime) // NOTE: Not required, it must be possible to put NULL
                .HasColumnName("end_time")
                .HasColumnType("DATETIME");

            builder
                .HasOne<Account>(l => l.Account)
                .WithMany(a => a.BookLoans)
                .HasForeignKey(l => l.AccountId);

            builder
                .HasOne<Book>(l => l.Book)
                .WithMany(b => b.BookLoans)
                .HasForeignKey(l => l.BookIsbn);

            builder
                .Property(l => l.AccountId)
                .HasColumnType("INT")
                .HasColumnName("account_id");

            builder
                .Property(l => l.BookIsbn)
                .HasColumnName("book_isbn")
                .HasColumnType("VARCHAR(17)")
                .HasMaxLength(17);
        }
    }
}