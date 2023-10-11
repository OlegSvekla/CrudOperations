using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CrudOperations.Domain.Entities;
using System.Reflection.Emit;

namespace CrudOperations.Infrastructure.EntitiesConfiguration
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(b => b.Name).IsRequired();
            builder.Property(b => b.Age).IsRequired();
            builder.Property(b => b.Email).IsRequired();
        }
    }
}
