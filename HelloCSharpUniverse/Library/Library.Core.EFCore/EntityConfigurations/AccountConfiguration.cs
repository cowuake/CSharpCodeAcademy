﻿using Library.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Core.EFCore.EntityConfigurations
{
    public class AccountConfiguration : IEntityTypeConfiguration<Account>
    {
        public void Configure(EntityTypeBuilder<Account> builder)
        {
            builder
                .ToTable("account")
                .HasKey(u => u.Id);

            builder
                .Property(u => u.Username)
                .IsRequired()
                .HasColumnName("username")
                .HasColumnType("VARCHAR(100)")
                .HasMaxLength(100);

            builder.Property(u => u.Password)
                .IsRequired()
                .HasColumnName("password")
                .HasColumnType("VARCHAR(100)")
                .HasMaxLength(100);

            builder.Property(u => u.Role)
                .IsRequired()
                .HasColumnName("role")
                .HasColumnType("VARCHAR(20)")
                .HasMaxLength(20);
        }
    }
}
