using AuthenticationAPI.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AuthenticationAPI.Infrastructure.Configurations.Builders
{
    public class UserBuilder : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name)
                .HasColumnType("varchar(100)");

            builder.Property(x => x.Email)
                .HasColumnType("varchar(256)");

            builder.Property(x => x.Password)
                .HasColumnType("varchar(200)");

            builder.Property(x => x.CreatedAt).ValueGeneratedOnAdd();
            builder.Property(x => x.ChangedAt).ValueGeneratedOnUpdate();
        }
    }
}
