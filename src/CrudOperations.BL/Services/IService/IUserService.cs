using CrudOperations.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrudOperations.BL.Services.IService
{
    //public interface IUserService<T> where T : class
    //{
    //    Task<T> GetAllUsers(string term, string sort, int page, int limit);
    //    //Task<IList<T>> GetAllUsers(
    //    //        int page = 1,
    //    //        int pageSize = 10,
    //    //        string sortBy = "Id",
    //    //        string sortOrder = "asc",
    //    //        string nameFilter = null,
    //    //        int? ageFilter = null,
    //    //        string emailFilter = null);
    //    Task<T> GetUseryId(int id);
    //    Task<bool> AddUser(T book);
    //    Task<T> UpdateUser(int id, T book);
    //    Task<T> DeleteUser(int id);
    //}
    public interface IUserService<PagedUserResult>
    {
        PagedUserAndRoleResult GetUseryId(int id);
        PagedUserAndRoleResult AddUser(PagedUserResult user);
        PagedUserAndRoleResult UpdateUser(int id, PagedUserResult user);
        PagedUserAndRoleResult DeleteUser(int id);
        Task<PagedUserAndRoleResult> GetAllUsersAndRoles(string userTerm, string userSort, string roleTerm, string roleSort, int page, int limit);
    }
}
