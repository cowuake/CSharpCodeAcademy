﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TicketingEFCore.Entities;

namespace TicketingEFCore.EFCore.Configs
{
    public class TicketConfig : IEntityTypeConfiguration<Ticket>
    {
        public void Configure(EntityTypeBuilder<Ticket> builder)
        {
            builder.HasKey(t => t.Id);

            builder.Property(t => t.Description)
                .IsRequired()
                .HasColumnName("description")
                .HasColumnType("VARCHAR")
                .HasMaxLength(500);

            builder.Property(t => t.CreatedDate)
                .IsRequired()
                .HasColumnName("opened")
                .HasColumnType("DATE");

            builder.Property(t => t.Customer)
                .IsRequired()
                .HasColumnName("customer")
                .HasColumnType("VARCHAR")
                .HasMaxLength(100);

            builder.Property(t => t.State)
                .IsRequired()
                .HasColumnName("state")
                .HasColumnType("VARCHAR")
                .HasMaxLength(10)
                .HasDefaultValue("new");

            builder.Property(t => t.CategoryId)
                .IsRequired()
                .HasColumnName("category_id")
                .HasColumnType("INT");

            builder.HasOne<Category>(t => t.Category)
                .WithMany(c => c.Tickets)
                .HasForeignKey(x => x.CategoryId)
                .IsRequired();
        }
    }
}