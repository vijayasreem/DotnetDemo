namespace SacreddotNet
{
    public enum FileType
    {
        PDF,
        CSV,
        Excel,
        Custom
    }

    public enum DestinationType
    {
        Email,
        CloudStorage,
        InternalServer
    }

    public class ReportDeliveryConfigurationModel
    {
        public int Id { get; set; }
        public DestinationType DestinationType { get; set; }
        public string DestinationAddress { get; set; }
        public string CustomFormat { get; set; }
        public string DayOfWeek { get; set; }
        public string DayOfMonth { get; set; }
        public TimeSpan DeliveryTime { get; set; }
        public string EmailAddresses { get; set; }
        public string Subject { get; set; }
        public string BodyText { get; set; }
        public string EmailFormat { get; set; }
        public string FTPUrl { get; set; }
        public string FTPPassword { get; set; }
        public string FilePath { get; set; }
        public string SharePointUrl { get; set; }
        public string DocumentLibraryName { get; set; }
        public string ClientName { get; set; }
        public string DeliveryDate { get; set; }
    }
}