public interface IReportGeneratorService
{
    Task GenerateReportAsync(FileType fileType, ReportDeliveryConfiguration config);
}