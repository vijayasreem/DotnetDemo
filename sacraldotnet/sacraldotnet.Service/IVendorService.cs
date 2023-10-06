public interface IVendorService
{
    Task FetchVendorsBySector(string sector);
    Task FetchScheduleInformation();
    Task StartTimer();
}