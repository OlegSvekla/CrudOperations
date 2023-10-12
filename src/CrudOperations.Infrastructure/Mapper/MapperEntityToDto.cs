using CrudOperations.Domain.Dtos;
using CrudOperations.Domain.Entities;
using FluentNHibernate.Automapping;

namespace CrudOperation.Domain.Mapper
{
    public sealed class MapperEntityToDto : AutoMapper.Profile
    {
        public MapperEntityToDto()
        {
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<Role, RoleDto>().ReverseMap();
        }
    }
}