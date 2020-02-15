using System.Threading.Tasks;
using ZrakStore.Data.Entities;

namespace ZrakStore.Data.Repositories
{
    public interface IAsyncUserRepository : IAsyncRepository<User>
    {
        Task<User> GetByUsernameAsync(string username);
    }
}
