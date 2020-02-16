using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ZrakStore.Data.Entities;

namespace ZrakStore.Data.Repositories
{
    public interface IAsyncRoleRepository : IAsyncRepository<Role>
    {
        Task AddToRoleAsync(string userId, string roleId);
    }
}
