using CrudOperations.Domain.Entities;
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
        Task<bool> AddUser(T user);

        Task<T> UpdateUser(int id, T user);
        Task<T> DeleteUser(int id);


        Task<T> AddRole(Role role, int id);


    }
}
