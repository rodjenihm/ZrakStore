using System.Collections.Generic;
using System.Threading.Tasks;
using ZrakStore.Data.Entities;

namespace ZrakStore.Services
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task<User> GetUserByUsernameAsync(string username);
        Task<User> GetUserByIdAsync(string id);
        Task AddUserAsync(User user);
    }
}
