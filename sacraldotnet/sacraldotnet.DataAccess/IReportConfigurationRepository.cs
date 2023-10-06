
using System.Collections.Generic;
using System.Threading.Tasks;
using sacraldotnet.DTO;

namespace sacraldotnet.Service
{
    public interface IReportConfigurationRepository
    {
        Task<List<ReportConfigurationModel>> GetAllAsync();
        Task<ReportConfigurationModel> GetByIdAsync(int id);
        Task<int> CreateAsync(ReportConfigurationModel reportConfiguration);
        Task<bool> UpdateAsync(ReportConfigurationModel reportConfiguration);
        Task<bool> DeleteAsync(int id);
    }
}
