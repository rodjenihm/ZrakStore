using System.Collections.Generic;
using System.Threading.Tasks;
using ZrakStore.Data.Entities;
using ZrakStore.Data.Repositories;

namespace ZrakStore.Services
{
    public class RoleService : IRoleService
    {
        private readonly IAsyncRoleRepository roleRepository;

        public RoleService(IAsyncRoleRepository roleRepository)
        {
            this.roleRepository = roleRepository;
        }

        public async Task AddUserToRoleAsync(User user, RoleType role)
        {
            await roleRepository.AddToRoleAsync(user.Id, role.ToString());
        }

        public Task<IEnumerable<string>> GetUserRolesAsync(User user)
        {
            return roleRepository.GetRolesByUsernameAsync(user.Username);
        }
    }
}
