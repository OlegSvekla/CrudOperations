using CrudOperations.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrudOperations.BL.Services.IService
{
    public interface IFilterService<PagedUserAndRoleResult>
    {
        Task<PagedUserAndRoleResult> GetAllUsersAndRoles(
            string userTerm,
            string userSort,
            string roleTerm,
            string roleSort,
            int page,
            int limit);
    }
}
