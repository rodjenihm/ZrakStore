using System.Collections.Generic;
using System.Threading.Tasks;
using ZrakStore.Data.Entities;

namespace ZrakStore.Data.Repositories
{
    public interface IAsyncRepository<T> where T : BaseEntity
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIdAsync(string id);
        Task AddAsync(T t);
        Task UpdateAsync(T t);
        Task RemoveAsync(T t);
        Task<int> CountAsync();
    }
}
