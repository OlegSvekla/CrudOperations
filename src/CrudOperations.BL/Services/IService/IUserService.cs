using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrudOperations.BL.Services.IService
{
    public interface IUserService<T> where T : class
    {
        Task<IList<T>> GetAllUsers();
        Task<T> GetUseryId(int id);
        Task<bool> AddUser(T book);
        Task<T> UpdateUser(int id, T book);
        Task<T> DeleteUser(int id);
    }
}
