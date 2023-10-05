public interface IReportDeliveryConfiguration
{
    DestinationType DestinationType { get; set; }
    string DestinationAddress { get; set; }
    bool ValidateDestination();
    bool ValidateDeliveryConfiguration();
}

public interface IReportGenerator
{
    Task GenerateReport(FileType fileType, IReportDeliveryConfiguration deliveryConfiguration);
}

public interface ISchedulingConfigurationTool
{
    Task ConfigureScheduling();
}