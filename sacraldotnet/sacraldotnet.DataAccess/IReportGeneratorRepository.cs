NullOrWhiteSpace(config.DestinationAddress))
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
            switch (config.CustomFormat)
            {
                case CustomFormat.Weekly:
                    // Validate weekly configuration
                    if (config.DayOfWeek == null)
                    {
                        throw new ArgumentException("Day of week cannot be empty");
                    }
                    break;
                case CustomFormat.Monthly:
                    // Validate monthly configuration
                    if (config.DayOfMonth == null)
                    {
                        throw new ArgumentException("Day of month cannot be empty");
                    }
                    break;
                case CustomFormat.SpecificDate:
                    // Validate specific date configuration
                    if (config.DeliveryDate == null)
                    {
                        throw new ArgumentException("Delivery date cannot be empty");
                    }
                    break;
            }
        }
    }
}

using System.Threading.Tasks;
using SacreddotNet.DTO;

namespace SacreddotNet.Service
{
    public interface IReportGeneratorRepository
    {
        Task<ReportDeliveryConfigurationModel> GetReportDeliveryConfigurationAsync(int id);
        Task<int> CreateReportDeliveryConfigurationAsync(ReportDeliveryConfigurationModel config);
        Task UpdateReportDeliveryConfigurationAsync(ReportDeliveryConfigurationModel config);
        Task DeleteReportDeliveryConfigurationAsync(int id);
        Task<IEnumerable<ReportDeliveryConfigurationModel>> GetReportDeliveryConfigurationsAsync();
        Task GenerateReportAsync(FileType fileType, ReportDeliveryConfigurationModel config);
    }
}