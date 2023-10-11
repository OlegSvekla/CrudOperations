using CrudOperations.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CrudOperations.Infrastructure.Data
{
    public class CrudDbContextSeed
    {
        public static async Task SeedAsyncData(CrudDbContext context, ILogger logger, int retry = 0)
        {
            var retryForAvailbility = retry;

            try
            {
                logger.LogInformation("Data seeding started.");

                if (!await context.Roles.AnyAsync())
                {
                    await context.Roles.AddRangeAsync(GetPreConfiguredRoles());

                    await context.SaveChangesAsync();
                }
                if (!await context.Users.AnyAsync())
                {
                    await context.Users.AddRangeAsync(GetPreConfiguredUsers());

                    await context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                if (retryForAvailbility >= 10) throw;
                {
                    retryForAvailbility++;

                    logger.LogError(ex.Message);
                    await SeedAsyncData(context, logger, retryForAvailbility);
                }
                throw;
            }
        }

        private static IEnumerable<Role> GetPreConfiguredRoles()
        {
            var roles = new List<Role>
            {
                new Role { Name = "User" },
                new Role { Name = "Admin" },
                new Role { Name = "SuperAdmin" },
                new Role { Name = "Support" }
            };
            return roles;
        }

        private static IEnumerable<User> GetPreConfiguredUsers()
        {
            var users = new List<User>
                {
                    new User
                    {
                        Name = "User1",
                        Age = 1,
                        Email = "User1@example.com",
                        UserRoles = new List<UserRole>
                        {
                            new UserRole {  RoleId = 1 }
                        },
                    },
                    new User
                    {
                        Name = "User2",
                        Age = 2,
                        Email = "User2@example.com",
                        UserRoles = new List<UserRole>
                        {
                            new UserRole { RoleId = 2 }
                        }
                    },
                    new User
                    {
                        Name = "User3",
                        Age = 3,
                        Email = "User3@example.com",
                        UserRoles = new List<UserRole>
                        {
                            new UserRole { RoleId = 3}
                        }
                    },
                    new User
                    {
                        Name = "User4",
                        Age = 4,
                        Email = "User4@example.com",
                        UserRoles = new List<UserRole>
                        {
                            new UserRole { RoleId = 1},
                            new UserRole { RoleId = 2},
                            new UserRole { RoleId = 3}
                        }
                    },
                    new User
                    {
                        Name = "User5",
                        Age = 4,
                        Email = "User5@example.com",
                        UserRoles = new List<UserRole>
                        {
                            new UserRole { RoleId = 2},
                            new UserRole { RoleId = 4}
                        }
                    },
                };
            return users;
        }
    }
}
