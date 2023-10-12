using CrudOperations.Domain.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrudOperations.BL.Services.IService
{
    public interface IUserService<T> where T : class    
    {
        Task<T> GetUserById(int id);
        Task<bool> AddUser(UserDto userDto);
        Task<T> UpdateUser(int id, UserDto user);
        Task<bool> DeleteUser(int bookId);
        Task<bool> AddRoleToUser(int userId, RoleDto newRoleDto);
    }
}
