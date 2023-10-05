
using System.Collections.Generic;
using System.Threading.Tasks;
using sacraldotnet.DTO;

namespace sacraldotnet.Service
{
    public interface IReportDeliveryConfigurationService
    {
        Task<int> CreateAsync(ReportDeliveryConfigurationModel model);
        Task<ReportDeliveryConfigurationModel> GetByIdAsync(int id);
        Task<IEnumerable<ReportDeliveryConfigurationModel>> GetAllAsync();
        Task UpdateAsync(ReportDeliveryConfigurationModel model);
        Task DeleteAsync(int id);
    }
}
