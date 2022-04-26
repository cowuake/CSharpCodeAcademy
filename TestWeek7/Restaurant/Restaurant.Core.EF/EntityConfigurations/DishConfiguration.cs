using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Restaurant.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Restaurant.Core.EF.EntityConfigurations
{
    public class DishConfiguration : IEntityTypeConfiguration<Dish>
    {
        public void Configure(EntityTypeBuilder<Dish> builder)
        {
            builder
                .ToTable("dish")
                .HasKey(d => d.ID);

            builder
                .Property(d => d.ID)
                .HasColumnName("id");

            builder
                .Property(d => d.Name)
                .HasColumnName("name")
                .HasColumnType("VARCHAR")
                .HasMaxLength(50)
                .IsRequired();

            builder
                .Property(d => d.Type)
                .HasColumnName("type")
                .HasColumnType("VARCHAR")
                .HasMaxLength(20)
                .IsRequired();

            builder
                .Property(d => d.Price)
                .HasColumnName("price")
                .HasColumnType("MONEY")
                .IsRequired();
        }
    }
}