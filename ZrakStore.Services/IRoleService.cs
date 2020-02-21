using System.Collections.Generic;
using System.Threading.Tasks;
using ZrakStore.Data.Entities;

namespace ZrakStore.Services
{
    public interface IRoleService
    {
        Task AddUserToRoleAsync(User user, RoleType role);
        Task<IEnumerable<string>> GetUserRolesAsync(User user);
    }
}
