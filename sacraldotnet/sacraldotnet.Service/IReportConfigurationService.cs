public interface IReportConfigurationService
{
    Task ConfigureReport(string fileType, string destination, string frequency);
}