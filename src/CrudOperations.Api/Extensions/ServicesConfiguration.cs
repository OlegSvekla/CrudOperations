using CrudOperation.Domain.Mapper;
using CrudOperations.BL.Services;
using CrudOperations.BL.Services.IService;
using CrudOperations.Domain.Dtos;
using CrudOperations.Domain.Entities;
using CrudOperations.Domain.SortFilterPaginationModels;
using CrudOperations.Domain.Validation;
using CrudOperations.Infrastructure.Data.Repository;
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
            services.AddScoped<IValidator<UserDto>, UserDtoValidation>();
            services.AddScoped<IValidator<RoleDto>, RoleDtoValidation>();

            services.AddAutoMapper(typeof(MapperEntityToDto));
        }
    }
}
