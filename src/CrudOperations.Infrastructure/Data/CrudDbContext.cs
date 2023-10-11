using CrudOperations.Domain.Entities;
using CrudOperations.Infrastructure.EntitiesConfiguration;
using Microsoft.EntityFrameworkCore;

namespace CrudOperations.Infrastructure.Data
{
    public class CrudDbContext : DbContext
    {
        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }

        public CrudDbContext(DbContextOptions<CrudDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new UserRoleConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}
