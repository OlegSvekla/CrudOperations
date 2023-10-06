using CrudOperations.BL.Services.Implementation;
using CrudOperations.BL.Services.IService;
using CrudOperations.Domain.Entities;
using CrudOperations.Infrastructure.Data.Rpository.Implementation;
using CrudOperations.Infrastructure.Data.Rpository.IRepository;

namespace CrudOperations.Api.Extensions
{
    public static class ServicesConfiguration
    {
        public static void Configuration(
            IServiceCollection services)
        {
            services.AddControllers();
            services.AddEndpointsApiExplorer();

            services.AddScoped(typeof(IEfRepository<>), typeof(EfRepository<>));
            services.AddScoped<IUserService<User>, UserService>();
        }
    }
}
