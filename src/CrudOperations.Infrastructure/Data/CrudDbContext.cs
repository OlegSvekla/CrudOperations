using CrudOperations.Domain.Entities;
using CrudOperations.Infrastructure.EntitiesConfiguration;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrudOperations.Infrastructure.Data
{
    public class CrudDbContext : DbContext
    {
        public DbSet<User> Authors { get; set; }

        public CrudDbContext(DbContextOptions<CrudDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}
