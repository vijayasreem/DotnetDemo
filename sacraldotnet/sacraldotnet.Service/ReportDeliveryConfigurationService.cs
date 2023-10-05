using System.Collections.Generic;
using System.Threading.Tasks;
using sacraldotnet.DataAccess;
using sacraldotnet.DTO;

namespace sacraldotnet.Service
{
    public class ReportDeliveryConfigurationService : IReportDeliveryConfigurationService
    {
        private readonly IReportDeliveryConfigurationDataAccess _reportDeliveryConfigurationDataAccess;

        public ReportDeliveryConfigurationService(IReportDeliveryConfigurationDataAccess reportDeliveryConfigurationDataAccess)
        {
            _reportDeliveryConfigurationDataAccess = reportDeliveryConfigurationDataAccess;
        }

        public async Task<int> CreateAsync(ReportDeliveryConfigurationModel model)
        {
            // Implement create logic here
            return await _reportDeliveryConfigurationDataAccess.CreateAsync(model);
        }

        public async Task<ReportDeliveryConfigurationModel> GetByIdAsync(int id)
        {
            // Implement get by ID logic here
            return await _reportDeliveryConfigurationDataAccess.GetByIdAsync(id);
        }

        public async Task<IEnumerable<ReportDeliveryConfigurationModel>> GetAllAsync()
        {
            // Implement get all logic here
            return await _reportDeliveryConfigurationDataAccess.GetAllAsync();
        }

        public async Task UpdateAsync(ReportDeliveryConfigurationModel model)
        {
            // Implement update logic here
            await _reportDeliveryConfigurationDataAccess.UpdateAsync(model);
        }

        public async Task DeleteAsync(int id)
        {
            // Implement delete logic here
            await _reportDeliveryConfigurationDataAccess.DeleteAsync(id);
        }
    }
}