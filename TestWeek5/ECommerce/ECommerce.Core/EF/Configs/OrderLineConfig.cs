using ECommerce.Core.EF.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Core.EF.Configs
{
    public class OrderLineConfig : IEntityTypeConfiguration<OrderLine>
    {
        public void Configure(EntityTypeBuilder<OrderLine> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Quantity)
                .IsRequired()
                .HasColumnName("quantity")
                .HasColumnType("INT");

            builder.HasOne<Product>(x => x.Product)
                .WithMany(p => p.OrderLines)
                .HasForeignKey(x => x.ProductId);

            builder.HasOne<Order>(x => x.Order)
                .WithMany(o => o.OrderLines)
                .HasForeignKey(x => x.OrderId);
        }
    }
}
