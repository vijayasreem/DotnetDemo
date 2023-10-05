namespace sacraldotnet
{
    public class ReportDeliveryConfigurationModel
    {
        public int Id { get; set; }
        public DestinationTypeModel DestinationType { get; set; }
        public string DestinationAddress { get; set; }
        public FrequencyTypeModel FrequencyType { get; set; }
        public int DayOfWeek { get; set; }
        public int DayOfMonth { get; set; }
        public TimeSpan DeliveryTime { get; set; }
    }

    public enum DestinationTypeModel
    {
        Email,
        CloudStorage,
        InternalServer
    }

    public enum FrequencyTypeModel
    {
        Daily,
        Weekly,
        Monthly,
        Custom
    }

    public class ReportGeneratorModel
    {
        public FileTypeModel FileType { get; set; }
        public ReportDeliveryConfigurationModel ReportDeliveryConfiguration { get; set; }

        public void GenerateReport()
        {
            switch (FileType)
            {
                case FileTypeModel.PDF:
                    // Logic for generating PDF report
                    break;
                case FileTypeModel.CSV:
                    // Logic for generating CSV report
                    break;
                case FileTypeModel.Excel:
                    // Logic for generating Excel report
                    break;
                case FileTypeModel.Custom:
                    // Logic for generating custom report
                    break;
                default:
                    // Invalid file type
                    break;
            }
        }
    }

    public enum FileTypeModel
    {
        PDF,
        CSV,
        Excel,
        Custom
    }
}