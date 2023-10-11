using CrudOperations.BL.Services.Implementation;
using CrudOperations.BL.Services.IService;
using CrudOperations.Domain.Entities;
using CrudOperations.Domain.Validation;
using CrudOperations.Infrastructure.Data.Repository.Implementation;
using CrudOperations.Infrastructure.Data.Repository.IRepository;
using FluentValidation;

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

            services.AddScoped<IFilterService<PagedUserAndRoleResult>, UserService>();
            services.AddScoped<IUserService<User>, UserService>();

            services.AddScoped<IValidator<UserQueryParameters>, UserQueryParametersValidation>();
        }
    }
}
