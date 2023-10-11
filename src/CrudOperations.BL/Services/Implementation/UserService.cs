using CrudOperations.BL.Services.IService;
using CrudOperations.Domain.Entities;
using CrudOperations.Infrastructure.Data.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

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

        //public async Task<PagedUserResult> GetAllUsers(string term, string sort, int page, int limit)
        //{
        //    var usersData = await _userRepository.GetAllAsync();
        //    var usersQuery = usersData.AsQueryable();

        //    // Применение фильтрации
        //    if (!string.IsNullOrWhiteSpace(term))
        //    {
        //        term = term.Trim().ToLower();
        //        usersQuery = usersQuery.Where(u =>
        //            u.Name.ToLower().Contains(term) ||
        //            u.Age.ToString().Contains(term) ||
        //            u.Email.ToLower().Contains(term)
        //        );
        //    }

        //    // Применение сортировки
        //    if (!string.IsNullOrWhiteSpace(sort))
        //    {
        //        usersQuery = ApplySorting(usersQuery, sort);
        //    }

        //    // Получение общего количества записей
        //    var totalCount = await usersQuery.CountAsync();

        //    // Применение пагинации
        //    usersQuery = usersQuery.Skip((page - 1) * limit).Take(limit);

        //    var pagedUsers = await usersQuery.ToListAsync();

        //    var result = new PagedUserResult
        //    {
        //        Users = pagedUsers,
        //        TotalCount = totalCount,
        //        TotalPages = (int)Math.Ceiling((double)totalCount / limit)
        //    };

        //    return result;
        //}
        public async Task<PagedUserAndRoleResult> GetAllUsersAndRoles(string userTerm, string userSort, string roleTerm, string roleSort, int page, int limit)
        {
            var usersData = await _userRepository.GetAllAsync();
            var usersQuery = usersData.AsQueryable();

            var rolesData = await _roleRepository.GetAllAsync();
            var rolesQuery = rolesData.AsQueryable();

            // Применение фильтрации для пользователей
            if (!string.IsNullOrWhiteSpace(userTerm))
            {
                userTerm = userTerm.Trim().ToLower();
                usersQuery = usersQuery.Where(u =>
                    u.Name.ToLower().Contains(userTerm) ||
                    u.Age.ToString().Contains(userTerm) ||
                    u.Email.ToLower().Contains(userTerm)
                );
            }

            // Применение фильтрации для ролей
            if (!string.IsNullOrWhiteSpace(roleTerm))
            {
                roleTerm = roleTerm.Trim().ToLower();
                rolesQuery = rolesQuery.Where(r =>
                    r.Name.ToLower().Contains(roleTerm)
                );
            }

            // Применение сортировки для пользователей
            if (!string.IsNullOrWhiteSpace(userSort))
            {
                usersQuery = ApplySortingUser(usersQuery, userSort);
            }

            // Применение сортировки для ролей
            if (!string.IsNullOrWhiteSpace(roleSort))
            {
                rolesQuery = ApplySortingRole(rolesQuery, roleSort);
            }

            // Выполнение операций пагинации для пользователей и ролей
            usersQuery = usersQuery.Skip((page - 1) * limit).Take(limit);
            rolesQuery = rolesQuery.Skip((page - 1) * limit).Take(limit);

            // Получение общего количества записей
            var userTotalCount = await usersQuery.CountAsync();
            var roleTotalCount = await rolesQuery.CountAsync();

            // Объединение результатов и возвращение
            var pagedResult = new PagedUserAndRoleResult
            {
                Users = await usersQuery.ToListAsync(),
                Roles = await rolesQuery.ToListAsync(),
                TotalCount = userTotalCount + roleTotalCount,
                TotalPages = (int)Math.Ceiling((double)(userTotalCount + roleTotalCount) / limit)
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



        //public async Task<IList<User>> GetAllUsers(
        //    int page = 1,
        //    int pageSize = 10,
        //    string sortBy = "Id",
        //    string sortOrder = "asc",
        //    string nameFilter = null,
        //    int? ageFilter = null,
        //    string emailFilter = null)
        //{
        //    var usersTask = await _userRepository.GetAllAsync(); // Получение задачи с пользователем

        //    var usersQuery = usersTask.AsQueryable(); // Преобразование в IQueryable

        //    // Применение фильтрации по атрибутам модели User
        //    if (!string.IsNullOrEmpty(nameFilter))
        //    {
        //        usersQuery = usersQuery.Where(u => u.Name.Contains(nameFilter));
        //    }
        //    if (ageFilter.HasValue)
        //    {
        //        usersQuery = usersQuery.Where(u => u.Age == ageFilter.Value);
        //    }
        //    if (!string.IsNullOrEmpty(emailFilter))
        //    {
        //        usersQuery = usersQuery.Where(u => u.Email.Contains(emailFilter));
        //    }

        //    // Применение сортировки
        //    if (!string.IsNullOrEmpty(sortBy))
        //    {
        //        switch (sortBy)
        //        {
        //            case "Id":
        //                usersQuery = sortOrder == "asc"
        //                    ? usersQuery.OrderBy(u => u.Id)
        //                    : usersQuery.OrderByDescending(u => u.Id);
        //                break;
        //            case "Name":
        //                usersQuery = sortOrder == "asc"
        //                    ? usersQuery.OrderBy(u => u.Name)
        //                    : usersQuery.OrderByDescending(u => u.Name);
        //                break;
        //                // Добавьте другие атрибуты для сортировки по аналогии
        //        }
        //    }

        //    // Применение пагинации
        //    usersQuery = usersQuery.Skip((page - 1) * pageSize).Take(pageSize);

        //    var users = await usersQuery.ToListAsync();

        //    return users;
        //}

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
