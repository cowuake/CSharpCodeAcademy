using ECommerce.Core.EF.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Core.EF.Configs
{
    public class OrderConfig : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Date)
                .IsRequired()
                .HasColumnName("date")
                .HasColumnType("DATE");

            builder.Property(x => x.Total)
                .IsRequired()
                .HasColumnName("total")
                .HasColumnType("MONEY");
        }
    }
}
