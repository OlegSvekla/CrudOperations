using CrudOperations.BL.Services.IService;
using CrudOperations.Domain.Entities;
using CrudOperations.Infrastructure.Data.Rpository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrudOperations.BL.Services.Implementation
{
    public class UserService : IUserService<User>
    {
        private readonly IEfRepository<User> _userRepository;

        public UserService(IEfRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<IList<User>> GetAllUsers()
        {
            var users = await _userRepository.GetAllAsync();
            return users.ToList();
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
    }
}
