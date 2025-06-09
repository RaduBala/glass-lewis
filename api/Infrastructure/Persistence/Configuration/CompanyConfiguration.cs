using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configuration;

public class CompanyConfiguration : IEntityTypeConfiguration<Company>
{
    public void Configure(EntityTypeBuilder<Company> builder)
    {
        builder.ToTable("Companies");

        builder.HasKey(e => e.Id);

        builder.Property(c => c.Id).IsRequired();

        builder
            .Property(c => c.Name)
            .HasMaxLength(128)
            .IsRequired();

        builder.Property(c => c.Ticker)
            .HasMaxLength(64)
            .IsRequired();

        builder.Property(c => c.Exchange)
            .HasMaxLength(128)
            .IsRequired();

        builder.Property(c => c.Isin)
            .HasMaxLength(12)
            .IsFixedLength()
            .IsRequired();

        builder.HasIndex(c => c.Isin)
               .IsUnique();

        builder
            .Property(c => c.Website)
            .HasMaxLength(1024);
    }
}
