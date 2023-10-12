using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using CrudOperations.Domain.Entities;

namespace CrudOperations.Infrastructure.EntitiesConfiguration
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(b => b.Name).IsRequired();
            builder.Property(b => b.Age).IsRequired();
            builder.HasIndex(b => b.Email).IsUnique();
        }
    }
}
