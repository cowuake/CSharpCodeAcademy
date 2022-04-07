using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TicketingEFCore.Entities;

namespace TicketingEFCore.EFCore.Configs
{
    public class CategoryConfig : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasKey(t => t.Id).HasName("id");

            builder.Property(t => t.Name)
                .IsRequired()
                .HasColumnName("name")
                .HasColumnType("VARCHAR")
                .HasMaxLength(50);

            builder.Property(t => t.Description)
                .HasColumnName("description")
                .HasColumnType("VARCHAR")
                .HasMaxLength(200);

            builder.HasMany<Ticket>(x => x.Tickets)
                .WithOne(x => x.Category)
                .HasForeignKey(x => x.Id);
        }
    }
}