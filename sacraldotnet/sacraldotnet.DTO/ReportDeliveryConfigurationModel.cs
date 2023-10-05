namespace sacraldotnet
{
    public enum DestinationType
    {
        Email,
        CloudStorage,
        InternalServer
    }

    public class ReportDeliveryConfigurationModel
    {
        public DestinationType DestinationType { get; set; }

        public string DestinationAddress { get; set; }

        public void ValidateDestination()
        {
            // TODO: Implement validation logic for DestinationAddress based on DestinationType
        }
    }

    public class ReportGeneratorModel
    {
        public FileType FileType { get; set; }

        public void GenerateReport()
        {
            // TODO: Implement logic for generating reports based on FileType
        }
    }

    public class ReportDeliveryConfigurationModel
    {
        public FileType FileType { get; set; }

        public string DestinationAddress { get; set; }

        public void ValidateDestination()
        {
            // TODO: Implement validation logic for DestinationAddress based on FileType
        }
    }

    public class ReportDeliveryConfigurationModel
    {
        public FrequencyType FrequencyType { get; set; }

        public DayOfWeek DayOfWeek { get; set; }

        public DayOfMonth DayOfMonth { get; set; }

        public TimeSpan DeliveryTime { get; set; }

        public void ValidateDeliveryConfiguration()
        {
            // TODO: Implement validation logic for FrequencyType, DayOfWeek, DayOfMonth, and DeliveryTime
        }
    }

    public class SharePointIntegrationModel
    {
        public string SharePointUrl { get; set; }

        public string DocumentLibraryName { get; set; }

        public void DeliverGLReport(string clientName, DateTime deliveryDate)
        {
            // TODO: Implement logic for delivering GL report to SharePoint
        }
    }
}