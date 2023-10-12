using AutoMapper;
using CrudOperations.BL.Services.IService;
using CrudOperations.Domain.Dtos;
using CrudOperations.Domain.Entities;
using CrudOperations.Domain.SortFilterPaginationModels;
using CrudOperations.Infrastructure.Data.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CrudOperations.BL.Services
{
    public class UserService : IFilterService<PagedUserAndRoleResult>, IUserService<User>
    {
        private readonly IEfRepository<User> _userRepository;
        private readonly IEfRepository<Role> _roleRepository;
        private readonly IEfRepository<UserRole> _userRoleRepository;
        private readonly IMapper _mapper;

        public UserService(
            IEfRepository<User> userRepository,
            IEfRepository<Role> roleRepository,
            IMapper mapper,
            IEfRepository<UserRole> userRoleRepository)
        {
            _userRepository = userRepository;
            _roleRepository = roleRepository;
            _mapper = mapper;
            _userRoleRepository = userRoleRepository;
        }

        public async Task<PagedUserAndRoleResult> SortUsers(
            string userTerm,
            string userSort,
            string roleTerm,
            string roleSort,
            int page,
            int limit)
        {
            var usersQuery = await _userRepository.GetFilteredAsync(
                user => userTerm == null ||
                    user.Name.Contains(userTerm) ||
                    user.Age.ToString().Contains(userTerm) ||
                    user.Email.Contains(userTerm)
                
            );

            var rolesQuery = await _roleRepository.GetFilteredAsync(
                role => roleTerm == null || role.Name.Contains(roleTerm)
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

        public async Task<User> GetUserById(int id)
        {
            var user = await _userRepository.GetOneByAsync(expression: _ => _.Id.Equals(id));
            if (user is null)
            {
                return null;
            }

            return user;
        }

        public async Task<bool> AddUser(UserDto userDto)
        {
            var existingUser = await _userRepository.GetOneByAsync(expression: u => u.Email == userDto.Email);
            if (existingUser != null)
            {
                return false;
            }

            var user = _mapper.Map<User>(userDto);

            var result = await _userRepository.AddAsync(user);

            return true;
        }

        public async Task<User> UpdateUser(int id, UserDto userDto)
        {
            var existingUser = await _userRepository.GetOneByAsync(expression: userDto => userDto.Id == id);
            if (existingUser is null)
            {
                return null;
            }
            else
            {
                existingUser.Name = userDto.Name;
                existingUser.Age = userDto.Age;
                existingUser.Email = userDto.Email;

                await _userRepository.UpdateAsync(existingUser);
                return existingUser;
            }
        }

        public async Task<bool> DeleteUser(int userId)
        {
            var userToDelete = await _userRepository.GetOneByAsync(expression: _ => _.Id.Equals(userId));
            if (userToDelete is null)
            {
                return false;
            }

            await _userRepository.DeleteAsync(userToDelete!);

            return true;
        }

        public async Task<bool> AddRoleToUser(int userId, RoleDto roleName)
        {
            var existingUser = await _userRepository.GetOneByAsync(expression: user => user.Id == userId);
            if (existingUser is null)
            {
                return false;
            }
            if (existingUser.UserRoles != null && existingUser.UserRoles.Any(ur => ur.Role.Name == roleName.Name))
            {
                return false;
            }

            var existingRole = await _roleRepository.GetOneByAsync(expression: role => role.Name == roleName.Name);
            if (existingRole is null)
            {
                var role = _mapper.Map<Role>(roleName);

                await _roleRepository.AddAsync(role);

                existingRole = role;
            }
            if (existingUser.UserRoles is null)
            {
                existingUser.UserRoles = new List<UserRole>();
            }

            existingUser.UserRoles.Add(new UserRole { UserId = existingUser.Id, RoleId = existingRole.Id });

            await _userRepository.UpdateAsync(existingUser);

            return true;
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
    }
}