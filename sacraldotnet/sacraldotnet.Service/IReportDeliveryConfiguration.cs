public interface IReportDeliveryConfiguration
{
    DestinationType DestinationType { get; set; }
    string DestinationAddress { get; set; }

    bool ValidateDestination();
    bool ValidateDeliveryConfiguration();
}

public interface IReportGenerator
{
    Task GenerateReport(FileType fileType);
}

public interface ISchedulerService
{
    Task ScheduleReportGeneration();
}