using System.Collections.Generic;
using System.Threading.Tasks;
using ZrakStore.Data.Entities;
using ZrakStore.Data.Repositories;

namespace ZrakStore.Services
{
    public class WebService : IUserService, IRoleService
    {
        private readonly IAsyncUserRepository userRepository;
        private readonly IAsyncRoleRepository roleRepository;

        public WebService(IAsyncUserRepository userRepository, IAsyncRoleRepository roleRepository)
        {
            this.userRepository = userRepository;
            this.roleRepository = roleRepository;
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

        public async Task AddUserToRoleAsync(User user, RoleType role)
        {
            await roleRepository.AddToRoleAsync(user.Id, role.ToString());
        }
    }
}
