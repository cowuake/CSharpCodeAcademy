using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TicketingEFCore.Entities;

namespace TicketingEFCore.EFCore.Configs
{
    public class CategoryConfig : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Name)
                .IsRequired()
                .HasColumnName("name")
                .HasColumnType("VARCHAR")
                .HasMaxLength(50);

            builder.Property(c => c.Description)
                .HasColumnName("description")
                .HasColumnType("VARCHAR")
                .HasMaxLength(200);
        }
    }
}