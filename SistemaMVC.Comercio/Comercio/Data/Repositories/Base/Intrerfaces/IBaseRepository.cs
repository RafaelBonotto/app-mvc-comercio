using System.Collections.Generic;
using System.Threading.Tasks;

namespace Comercio.Data.Repositories.Base.Intrerfaces
{
    public interface IBaseRepository<T>
    {
        Task<List<T>> GetAllAsync();
        Task<List<T>> GetByKeyAsync(string key);
        Task<T> GetByIdAsync(int id);
        Task<bool> AddAsync(T entity);
        Task<bool> UpdateAsync(T entity);
        Task<bool> DeleteAsync(int id);
    }
}
