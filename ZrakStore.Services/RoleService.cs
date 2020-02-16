using System;
using System.Collections.Generic;
using System.Text;
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
            var roleLevel = (int)role;
            await roleRepository.AddToRoleAsync(user.Id, roleLevel.ToString());
        }
    }
}
