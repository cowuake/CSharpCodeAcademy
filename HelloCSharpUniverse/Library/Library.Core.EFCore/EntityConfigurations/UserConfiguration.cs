using Library.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Core.EFCore.EntityConfigurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder
                .ToTable("user")
                .HasKey(u => u.Id);

            builder
                .Property(u => u.Username)
                .IsRequired()
                .HasColumnType("VARCHAR(100)")
                .HasMaxLength(100);

            builder.Property(u => u.Password)
                .IsRequired()
                .HasColumnType("VARCHAR(100)")
                .HasMaxLength(100);

            builder.Property(u => u.Role)
                .IsRequired()
                .HasColumnType("VARCHAR(20)")
                .HasMaxLength(20);
        }
    }
}
