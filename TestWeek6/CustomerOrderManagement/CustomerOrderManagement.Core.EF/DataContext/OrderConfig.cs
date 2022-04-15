using CustomerOrderManagement.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace CustomerOrderManagement.Core.EF.DataContext
{
    public class OrderConfig : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder
                .HasKey(o => o.Id);

            builder
                .Property(o => o.Date)
                .HasColumnName("Author")
                .HasColumnType("DATE")
                .IsRequired();

            builder
                .Property(o => o.Code)
                .HasColumnName("Code")
                .HasColumnType("VARCHAR(25)") // Length not needed in newer EF releases
                .HasMaxLength(25)
                .IsRequired();

            builder
                .Property(o => o.ProductCode)
                .HasColumnName("ProductCode")
                .HasColumnType("VARCHAR(25)") // Length not needed in newer EF releases
                .HasMaxLength(25)
                .IsRequired();

            builder
                .Property(o => o.Total)
                .HasColumnName("Total")
                .HasColumnType("MONEY")
                .IsRequired();

            builder
                .HasOne<Customer>(o => o.Customer)
                .WithMany(c => c.Orders)
                .HasForeignKey(o => o.CustomerId)
                .IsRequired();
        }
    }
}