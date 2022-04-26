using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Restaurant.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Restaurant.Core.EF.EntityConfigurations
{
    public class AccountConfiguration : IEntityTypeConfiguration<Account>
    {
        public void Configure(EntityTypeBuilder<Account> builder)
        {
            builder
                .ToTable("account")
                .HasKey(u => u.Id);

            builder
                .Property(a => a.Id)
                .HasColumnName("id");

            builder
                .Property(a => a.Email)
                .IsRequired()
                .HasColumnName("email")
                .HasColumnType("VARCHAR")
                .HasMaxLength(254);

            builder.Property(a => a.Password)
                .IsRequired()
                .HasColumnName("password")
                .HasColumnType("VARCHAR")
                .HasMaxLength(100);

            builder.Property(a => a.Role)
                .IsRequired()
                .HasColumnName("role")
                .HasColumnType("VARCHAR(20)")
                .HasMaxLength(20);
        }
    }
}