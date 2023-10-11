using CrudOperations.BL.Services.Implementation;
using CrudOperations.BL.Services.IService;
using CrudOperations.Domain.Entities;
using CrudOperations.Infrastructure.Data;
using CrudOperations.Infrastructure.Data.Repository.Implementation;
using CrudOperations.Infrastructure.Data.Repository.IRepository;

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
            services.AddScoped<IUserService<PagedUserAndRoleResult>, UserService>();
        }
    }
}
