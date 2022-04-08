using ECommerce.Core.EF.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Core.EF.Configs
{
    public class ProductConfig : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name)
                .IsRequired()
                .HasColumnName("name")
                .HasColumnType("VARCHAR")
                .HasMaxLength(100);

            builder.Property(x => x.Description)
                .IsRequired()
                .HasColumnName("description")
                .HasColumnType("VARCHAR")
                .HasMaxLength(500);

            builder.Property(x => x.Price)
                .IsRequired()
                .HasColumnName("price")
                .HasColumnType("MONEY");

            builder.Property(x => x.Visible)
                .IsRequired()
                .HasColumnName("visible")
                .HasColumnType("BIT");

            builder.Property(x => x.CategoryId)
                .IsRequired()
                .HasColumnName("category_id")
                .HasColumnType("INT");

            builder.HasOne<Category>(x => x.Category)
                .WithMany(c => c.Products)
                .HasForeignKey(x => x.CategoryId)
                .IsRequired();
        }
    }
}