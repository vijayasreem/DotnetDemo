var monthFolder = deliveryDate.ToString("MMM");
            var dayFolder = deliveryDate.ToString("dd");

            var path = $"{_sharePointUrl}/{_documentLibraryName}/{yearFolder}/{monthFolder}/{dayFolder}/{clientName}.pdf";

            // Simulate delivering GL report to SharePoint
            await Task.Delay(1000);
            Console.WriteLine($"Successfully delivered GL report to {path}");
        }
    }

    // Method to configure report delivery
    public void ConfigureReportDelivery(ReportDeliveryConfiguration config) 
    {
        // Validate DestinationAddress
        config.ValidateDestination();

        // Validate FrequencyType, DayOfWeek, DayOfMonth, and DeliveryTime
        config.ValidateDeliveryConfiguration();

        // Simulate saving report delivery configuration
        Console.WriteLine($"Successfully configured report delivery with {config.DestinationType}");
    }
}

public class ReportGeneratorService : IReportGeneratorService
{
    // Enumeration for FileType
    public enum FileType 
    {
        PDF,
        CSV,
        Excel,
        Custom
    }

    // Enumeration for DestinationType
    public enum DestinationType 
    {
        Email,
        CloudStorage,
        InternalServer
    }

    // Class for ReportDeliveryConfiguration
    public class ReportDeliveryConfiguration 
    {
        public DestinationType DestinationType { get; set; }
        public string DestinationAddress { get; set; }
        public string DayOfWeek { get; set; }
        public string DayOfMonth { get; set; }
        public TimeSpan DeliveryTime { get; set; }

        // Method to validate DestinationAddress based on the selected DestinationType
        public void ValidateDestination() 
        {
            switch (DestinationType) 
            {
                case DestinationType.Email:
                    if (!IsValidEmail(DestinationAddress)) 
                    {
                        throw new Exception("Invalid Email Address");
                    }
                    break;
                case DestinationType.CloudStorage:
                    if (string.IsNullOrEmpty(DestinationAddress)) 
                    {
                        throw new Exception("Empty Cloud Storage Address");
                    }
                    break;
                case DestinationType.InternalServer:
                    if (string.IsNullOrEmpty(DestinationAddress)) 
                    {
                        throw new Exception("Empty Internal Server Address");
                    }
                    break;
            }
        }

        // Method to validate selected FrequencyType, DayOfWeek, DayOfMonth, and DeliveryTime
        public void ValidateDeliveryConfiguration() 
        {
            // Validate file type
            if (DayOfWeek == null && DayOfMonth == null) 
            {
                throw new Exception("Frequency Type not selected");
            }

            // Validate DayOfWeek
            if (DayOfWeek != null) 
            {
                int dayOfWeek;
                if (!int.TryParse(DayOfWeek, out dayOfWeek) || dayOfWeek < 1 || dayOfWeek > 7) 
                {
                    throw new Exception("Invalid Day of Week");
                }
            }

            // Validate DayOfMonth
            if (DayOfMonth != null) 
            {
                int dayOfMonth;
                if (!int.TryParse(DayOfMonth, out dayOfMonth) || dayOfMonth < 1 || dayOfMonth > 31) 
                {
                    throw new Exception("Invalid Day of Month");
                }
            }

            // Validate DeliveryTime
            if (DeliveryTime.TotalHours < 0 || DeliveryTime.TotalHours > 24) 
            {
                throw new Exception("Invalid Delivery Time");
            }
        }

        // Method to check if email address is valid
        private bool IsValidEmail(string email) 
        {
            try 
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch 
            {
                return false;
            }
        }
    }

    // Method to generate reports based on selected file type
    public async Task GenerateReport(FileType fileType) 
    {
        switch (fileType) 
        {
            case FileType.PDF:
                // Simulate generating PDF report
                await Task.Delay(1000);
                break;
            case FileType.CSV:
               