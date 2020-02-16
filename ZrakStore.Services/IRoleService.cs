using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ZrakStore.Data.Entities;

namespace ZrakStore.Services
{
    public interface IRoleService
    {
        Task AddUserToRoleAsync(User user, RoleType role);
    }
}
