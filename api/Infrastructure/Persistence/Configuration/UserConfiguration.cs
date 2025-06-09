using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configuration;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users");

        builder.Property(e => e.Email)
            .IsRequired()
            .HasMaxLength(128);

        builder.Property(e => e.Password)
            .IsRequired()
            .HasMaxLength(128);
    }
}
