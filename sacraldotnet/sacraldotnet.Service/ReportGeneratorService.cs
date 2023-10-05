;
                case DestinationType.CloudStorage:
                    // Validate Cloud storage address
                    if (string.IsNullOrWhiteSpace(config.DestinationAddress))
                    {
                        throw new ArgumentException("Cloud storage address cannot be empty");
                    }
                    break;
                case DestinationType.FTP:
                    // Validate FTP address
                    if (string.IsNullOrWhiteSpace(config.DestinationAddress))
                    {
                        throw new ArgumentException("FTP address cannot be empty");
                    }
                    break;
                case DestinationType.Sharepoint:
                    // Validate Sharepoint address
                    if (string.IsNullOrWhiteSpace(config.DestinationAddress))
                    {
                        throw new ArgumentException("Sharepoint address cannot be empty");
                    }
                    break;
            }
        }

        private async Task ValidateDeliveryConfigurationAsync(ReportDeliveryConfigurationModel config)
        {
            // Validate delivery type
            if (config.DeliveryType == DeliveryType.None)
            {
                throw new ArgumentException("Delivery type must be specified");
            }

            // Validate delivery frequency
            if (config.DeliveryFrequency == 0)
            {
                throw new ArgumentException("Delivery frequency cannot be zero");
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SacreddotNet.DTO;
using SacreddotNet.DataAccess;

namespace SacreddotNet.Service
{
    public class ReportGeneratorService : IReportGeneratorRepository
    {
        private readonly IReportGeneratorRepository _reportGeneratorRepository;

        public ReportGeneratorService(IReportGeneratorRepository reportGeneratorRepository)
        {
            _reportGeneratorRepository = reportGeneratorRepository;
        }

        public async Task<IEnumerable<ReportDeliveryConfigurationModel>> GetReportDeliveryConfigurationsAsync()
        {
            return await _reportGeneratorRepository.GetReportDeliveryConfigurationsAsync();
        }

        public async Task<ReportDeliveryConfigurationModel> GetReportDeliveryConfigurationAsync(int id)
        {
            return await _reportGeneratorRepository.GetReportDeliveryConfigurationAsync(id);
        }

        public async Task<int> CreateReportDeliveryConfigurationAsync(ReportDeliveryConfigurationModel config)
        {
            await ValidateDestinationAsync(config);
            await ValidateDeliveryConfigurationAsync(config);

            return await _reportGeneratorRepository.CreateReportDeliveryConfigurationAsync(config);
        }

        public async Task UpdateReportDeliveryConfigurationAsync(ReportDeliveryConfigurationModel config)
        {
            await ValidateDestinationAsync(config);
            await ValidateDeliveryConfigurationAsync(config);

            await _reportGeneratorRepository.UpdateReportDeliveryConfigurationAsync(config);
        }

        public async Task DeleteReportDeliveryConfigurationAsync(int id)
        {
            await _reportGeneratorRepository.DeleteReportDeliveryConfigurationAsync(id);
        }

        public async Task GenerateReportAsync(FileType fileType, ReportDeliveryConfigurationModel config)
        {
            switch (fileType)
            {
                case FileType.PDF:
                    // Simulate PDF report generation
                    Console.WriteLine("Generating PDF report...");
                    break;
                case FileType.CSV:
                    // Simulate CSV report generation
                    Console.WriteLine("Generating CSV report...");
                    break;
                case FileType.Excel:
                    // Simulate Excel report generation
                    Console.WriteLine("Generating Excel report...");
                    break;
                case FileType.Custom:
                    // Simulate Custom report generation
                    Console.WriteLine("Generating Custom report...");
                    break;
            }

            // Simulate report delivery
            await DeliverReportAsync(config);
        }

        private async Task DeliverReportAsync(ReportDeliveryConfigurationModel config)
        {
            switch (config.DestinationType)
            {
                case DestinationType.Email:
                    // Validate email addresses
                    if (config.EmailAddresses.Any(string.IsNullOrWhiteSpace))
                    {
                        throw new ArgumentException("Email addresses cannot be empty");
                    }

                    // Validate subject
                    if (string.IsNullOrWhiteSpace(config.EmailSubject))
                    {
                        throw new ArgumentException("Email subject cannot be empty");
                    }

                    // Validate body
                    if (string.IsNullOrWhiteSpace(config.EmailBody))
                    {
                        throw