using CrudOperations.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CrudOperations.Api.Extensions
{
    public static class DbConfiguration
    {
        public static void Configuration(
            IConfiguration configuration,
            IServiceCollection services)
        {
            services.AddDbContext<CrudDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("CrudDbConnection")));
        }
    }
}
