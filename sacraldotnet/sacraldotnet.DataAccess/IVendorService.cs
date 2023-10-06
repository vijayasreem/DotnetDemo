
using System.Collections.Generic;
using System.Threading.Tasks;
using sacraldotnet.DTO;

namespace sacraldotnet.Service
{
    public interface IVendorService
    {
        Task<List<VendorModel>> GetAllVendorsAsync();
        Task<byte[]> GeneratePdfAsync(List<VendorModel> vendors);
        Task FetchScheduleInformationAsync();
    }
}
