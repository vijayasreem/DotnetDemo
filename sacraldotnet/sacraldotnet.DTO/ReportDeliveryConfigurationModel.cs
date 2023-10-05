namespace SacralDotNet
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
        public int? DayOfWeek { get; set; }
        public int? DayOfMonth { get; set; }
        public TimeSpan DeliveryTime { get; set; }
        public string[] EmailAddresses { get; set; }
        public string EmailSubject { get; set; }
        public string EmailBody { get; set; }
        public string EmailFormat { get; set; }
        public string FTPUrl { get; set; }
        public string FTPCreds { get; set; }
        public string FilePath { get; set; }
        public string SharePointUrl { get; set; }
        public string DocumentLibraryName { get; set; }
        public string ClientName { get; set; }
        public DateTime DeliveryDate { get; set; }
    }
}