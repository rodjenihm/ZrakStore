using System.Collections.Generic;
using System.Threading.Tasks;
using ZrakStore.Data.Entities;
using ZrakStore.Data.Repositories;

namespace ZrakStore.Services
{
    public class UserService : IUserService
    {
        private readonly IAsyncUserRepository userRepository;

        public UserService(IAsyncUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public async Task AddUserAsync(User user)
        {
            await userRepository.AddAsync(user);
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await userRepository.GetAllAsync();
        }

        public async Task<User> GetUserByUsernameAsync(string username)
        {
            return await userRepository.GetByUsernameAsync(username);
        }

        public async Task<User> GetUserByIdAsync(string id)
        {
            return await userRepository.GetByIdAsync(id);
        }
    }
}
