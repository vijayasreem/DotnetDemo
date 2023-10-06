public interface IVendorService
{
    Task<List<Vendor>> FetchVendorsBySector(string sector);
}