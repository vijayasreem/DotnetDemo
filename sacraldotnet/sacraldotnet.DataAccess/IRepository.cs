
using System.Collections.Generic;
using System.Threading.Tasks;
using sacraldotnet.DTO;

namespace sacraldotnet.Service
{
    public interface IService<T>
    {
        Task<List<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id);
        Task<int> CreateAsync(T entity);
        Task<int> UpdateAsync(T entity);
        Task<int> DeleteAsync(int id);
    }
}
