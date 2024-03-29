﻿using CustomerOrderManagement.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace CustomerOrderManagement.Core.EF.DataContext
{
    public class CustomerConfig : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder
                .HasKey(c => c.Id);

            builder
                .Property(c => c.Code)
                .HasColumnName("Code")
                .HasColumnType("VARCHAR(25)") // Length not needed in newer EF releases
                .HasMaxLength(25)
                .IsRequired();

            builder
                .Property(c => c.FirstName)
                .HasColumnName("FirstName")
                .HasColumnType("VARCHAR(25)") // Length not needed in newer EF releases
                .HasMaxLength(25)
                .IsRequired();

            builder
                .Property(c => c.LastName)
                .HasColumnName("LastName")
                .HasColumnType("VARCHAR(25)") // Length not needed in newer EF releases
                .HasMaxLength(25)
                .IsRequired();
        }
    }
}