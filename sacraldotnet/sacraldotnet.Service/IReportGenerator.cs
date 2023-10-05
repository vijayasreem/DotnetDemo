public interface IReportGenerator
{
    Task GenerateReport(FileType fileType, ReportDeliveryConfiguration deliveryConfiguration);
}

public interface IReportScheduler
{
    Task ScheduleReports();
}

public interface IReportDeliveryConfigurationValidation
{
    bool ValidateDestination();
}