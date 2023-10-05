namespace sacraldotnet
{
    public class ReportDeliveryConfigurationModel
    {
        public int Id { get; set; }
        public DestinationType DestinationType { get; set; }
        public string DestinationAddress { get; set; }
    }

    public enum DestinationType
    {
        Email,
        CloudStorage,
        InternalServer
    }
    
    public class ReportGeneratorModel
    {
        public int Id { get; set; }
        public FileType FileType { get; set; }
        public string CustomFormat { get; set; }
        public ReportDeliveryConfigurationModel ReportDeliveryConfiguration { get; set; }
        public FrequencyType FrequencyType { get; set; }
        public int[] DaysOfWeek { get; set; }
        public int[] DaysOfMonth { get; set; }
        public TimeSpan DeliveryTime { get; set; }
    }

    public enum FileType
    {
        PDF,
        CSV,
        Excel,
        Custom
    }

    public enum FrequencyType
    {
        DayOfWeek,
        DayOfMonth
    }
}