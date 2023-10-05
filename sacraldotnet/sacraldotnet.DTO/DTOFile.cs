namespace SacralDotNet
{
    public enum DestinationType
    {
        Email,
        CloudStorage,
        InternalServer
    }

    public enum FileType
    {
        PDF,
        CSV,
        Excel,
        Custom
    }

    public class ReportGenerator
    {
        public void GenerateReport(FileType fileType)
        {
            // Simulate the report generation process for predefined formats (PDF, CSV, Excel).
            // Simulate handling the custom format input and generate a report accordingly for the Custom format.
        }
    }

    public class ReportDeliveryConfigurationModel
    {
        public int Id { get; set; }
        public DestinationType DestinationType { get; set; }
        public string DestinationAddress { get; set; }
        public int FrequencyType { get; set; }
        public int DayOfWeek { get; set; }
        public int DayOfMonth { get; set; }
        public TimeSpan DeliveryTime { get; set; }
        public string SubjectLine { get; set; }
        public string BodyText { get; set; }
        public string TemplateFormatting { get; set; }
        public string FTP_URL { get; set; }
        public string Password_Credential { get; set; }
        public string FilePath { get; set; }
        public string SharePointURL { get; set; }
        public string DocumentLibraryName { get; set; }
        public string ClientName { get; set; }
        public DateTime DeliveryDate { get; set; }

        public void ValidateDestination()
        {
            // Validate the format of Email addresses, ensure the address is not empty for CloudStorage, and ensure the address is not empty for InternalServer.
        }

        public void ValidateDeliveryConfiguration()
        {
            // Ensure that the selected FrequencyType, DayOfWeek, DayOfMonth, and DeliveryTime are valid.
        }
    }
}