
using sacraldotnet.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace sacraldotnet.Service
{
    public interface IInvoiceRepository
    {
        Task<List<InvoiceModel>> GetAllAsync();
        Task<InvoiceModel> GetByIdAsync(int id);
        Task<int> CreateAsync(InvoiceModel invoice);
        Task UpdateAsync(InvoiceModel invoice);
        Task DeleteAsync(int id);
        Task FetchScheduleInformation();
    }
}
