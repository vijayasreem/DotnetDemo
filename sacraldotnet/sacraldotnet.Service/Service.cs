using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ReportSchedulingTool
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

    public class ReportDeliveryConfiguration
    {
        public DestinationType DestinationType { get; set; }
        public string DestinationAddress { get; set; }

        public bool ValidateDestination()
        {
            // Validate the DestinationAddress based on the selected DestinationType
            // Add your validation logic here
            return true; // Return true if validation is successful, otherwise false
        }
    }

    public interface IReportGenerator
    {
        Task GenerateReport(FileType fileType, ReportDeliveryConfiguration deliveryConfiguration);
    }

    public class ReportGenerator : IReportGenerator
    {
        public async Task GenerateReport(FileType fileType, ReportDeliveryConfiguration deliveryConfiguration)
        {
            switch (fileType)
            {
                case FileType.PDF:
                    // Logic for generating PDF report
                    break;
                case FileType.CSV:
                    // Logic for generating CSV report
                    break;
                case FileType.Excel:
                    // Logic for generating Excel report
                    break;
                case FileType.Custom:
                    // Prompt user to specify the custom format for the report
                    string customFormat = await GetCustomFormatFromUser();
                    // Logic for generating custom format report
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(fileType), fileType, null);
            }
        }

        private Task<string> GetCustomFormatFromUser()
        {
            // Implement logic to prompt user for custom format input
            return Task.FromResult(""); // Replace with actual user input
        }
    }

    public class ReportScheduler
    {
        private IReportGenerator reportGenerator;

        public ReportScheduler(IReportGenerator reportGenerator)
        {
            this.reportGenerator = reportGenerator;
        }

        public async Task ScheduleReports()
        {
            // Logic for scheduling reports
            FileType fileType = await GetFileTypeFromUser();
            ReportDeliveryConfiguration deliveryConfiguration = await GetDeliveryConfigurationFromUser();

            await reportGenerator.GenerateReport(fileType, deliveryConfiguration);
        }

        private Task<FileType> GetFileTypeFromUser()
        {
            // Implement logic to prompt user for file type selection
            return Task.FromResult(FileType.PDF); // Replace with actual user input
        }

        private Task<ReportDeliveryConfiguration> GetDeliveryConfigurationFromUser()
        {
            // Implement logic to prompt user for delivery configuration
            return Task.FromResult(new ReportDeliveryConfiguration()); // Replace with actual user input
        }
    }

    public class Program
    {
        public static async Task Main(string[] args)
        {
            IReportGenerator reportGenerator = new ReportGenerator();
            ReportScheduler reportScheduler = new ReportScheduler(reportGenerator);
            await reportScheduler.ScheduleReports();
        }
    }
}