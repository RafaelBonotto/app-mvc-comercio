using System.Collections.Generic;
using System.Threading.Tasks;

namespace Comercio.Interfaces.Base
{
    public interface IRepositoryBase<T>
    {
        Task<List<T>> GetAllAsync();
        Task<List<T>> GetByKeyAsync(string key);        
        Task<T> GetByIdAsync(int id);
        Task<T> AddAsync(T entity);
        Task<T> DeleteAsync(int id);
        Task<T> UpdateAsync(T entity);
    }
}
