using CrudOperations.BL.Services.IService;
using CrudOperations.Domain.Entities;
using CrudOperations.Infrastructure.Data.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CrudOperations.BL.Services.Implementation
{
    public class UserService : IUserService<PagedUserAndRoleResult>
    {
        private readonly IEfRepository<User> _userRepository;
        private readonly IEfRepository<Role> _roleRepository;

        public UserService(IEfRepository<User> userRepository, IEfRepository<Role> roleRepository)
        {
            _userRepository = userRepository;
            _roleRepository = roleRepository;
        }

        public async Task<PagedUserAndRoleResult> GetAllUsersAndRoles(
            string userTerm,
            string userSort,
            string roleTerm,
            string roleSort,
            int page,
            int limit)
        {

            var usersQuery = await _userRepository.GetFilteredAsync(
                user => (userTerm == null ||
                    user.Name.Contains(userTerm) ||
                    user.Age.ToString().Contains(userTerm) ||
                    user.Email.Contains(userTerm)
                )
            );

            var rolesQuery = await _roleRepository.GetFilteredAsync(
                role => (roleTerm == null || role.Name.Contains(roleTerm))
            );

            if (!string.IsNullOrWhiteSpace(userSort))
            {
                usersQuery = ApplySortingUser(usersQuery, userSort);
            }

            if (!string.IsNullOrWhiteSpace(roleSort))
            {
                rolesQuery = ApplySortingRole(rolesQuery, roleSort);
            }

            var totalUsers = await usersQuery.CountAsync();
            var totalRoles = await rolesQuery.CountAsync();

            usersQuery = usersQuery.Skip((page - 1) * limit).Take(limit);
            rolesQuery = rolesQuery.Skip((page - 1) * limit).Take(limit);

            var users = await usersQuery.ToListAsync();
            var roles = await rolesQuery.ToListAsync();

            var pagedResult = new PagedUserAndRoleResult
            {
                Users = users,
                Roles = roles,
                TotalCount = totalUsers + totalRoles,
                TotalPages = (int)Math.Ceiling((double)(totalUsers + totalRoles) / limit)
            };

            return pagedResult;
        }

        private IQueryable<User> ApplySortingUser(IQueryable<User> query, string sort)
        {
            var sortFields = sort.Split(',');
            foreach (var field in sortFields)
            {
                var trimmedField = field.Trim();
                if (trimmedField.StartsWith("-"))
                {
                    query = query.OrderByDescending(ApplySortOrderUser(trimmedField.Substring(1)));
                }
                else
                {
                    query = query.OrderBy(ApplySortOrderUser(trimmedField));
                }
            }
            return query;
        }

        private IQueryable<Role> ApplySortingRole(IQueryable<Role> query, string sort)
        {
            var sortFields = sort.Split(',');
            foreach (var field in sortFields)
            {
                var trimmedField = field.Trim();
                if (trimmedField.StartsWith("-"))
                {
                    query = query.OrderByDescending(ApplySortOrderRole(trimmedField.Substring(1)));
                }
                else
                {
                    query = query.OrderBy(ApplySortOrderRole(trimmedField));
                }
            }
            return query;
        }

        private Expression<Func<User, object>> ApplySortOrderUser(string field)
        {
            switch (field)
            {
                case "Name":
                    return user => user.Name;
                case "Age":
                    return user => user.Age;
                case "Email":
                    return user => user.Email;
                default:
                    return user => user.Id;
            }
        }
        private Expression<Func<Role, object>> ApplySortOrderRole(string field)
        {
            switch (field)
            {
                case "Name":
                    return user => user.Name;
                default:
                    return user => user.Id;
            }
        }

        public Task<bool> AddUser(User user)
        {
            throw new NotImplementedException();
        }

        public Task<User> DeleteUser(int id)
        {
            throw new NotImplementedException();
        }

        public Task<User> GetUseryId(int id)
        {
            throw new NotImplementedException();
        }

        public Task<User> UpdateUser(int id, User book)
        {
            throw new NotImplementedException();
        }

        PagedUserAndRoleResult IUserService<PagedUserAndRoleResult>.GetUseryId(int id)
        {
            throw new NotImplementedException();
        }

        public PagedUserAndRoleResult AddUser(PagedUserResult user)
        {
            throw new NotImplementedException();
        }

        public PagedUserAndRoleResult UpdateUser(int id, PagedUserResult user)
        {
            throw new NotImplementedException();
        }

        PagedUserAndRoleResult IUserService<PagedUserAndRoleResult>.DeleteUser(int id)
        {
            throw new NotImplementedException();
        }

        public PagedUserAndRoleResult AddUser(PagedUserAndRoleResult user)
        {
            throw new NotImplementedException();
        }

        public PagedUserAndRoleResult UpdateUser(int id, PagedUserAndRoleResult user)
        {
            throw new NotImplementedException();
        }

        public Task<PagedUserAndRoleResult> GetAllUsers(string term, string sort, int page, int limit)
        {
            throw new NotImplementedException();
        }

        public Task<PagedUserAndRoleResult> GetAllUsers(string userTerm, string userSort, string roleTerm, string roleSort, int page, int limit)
        {
            throw new NotImplementedException();
        }
    }
}
