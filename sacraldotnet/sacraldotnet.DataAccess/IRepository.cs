
using System.Collections.Generic;
using System.Threading.Tasks;
using sacraldotnet.DTO;

namespace sacraldotnet.Service
{
    public interface IRepository<TEntity> where TEntity : class
    {
        Task<TEntity> GetByIdAsync(int id);
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<int> InsertAsync(TEntity entity);
        Task<int> UpdateAsync(TEntity entity);
        Task<int> DeleteAsync(int id);
    }

    public interface IReportRepository : IRepository<Report>
    {
        // Declare additional report-related CRUD operations as needed
    }
}
