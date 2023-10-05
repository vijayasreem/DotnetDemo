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
            // Validate delivery frequency
            if (config.DeliveryFrequency == DeliveryFrequency.Unknown)
            {
                throw new ArgumentException("Delivery frequency is required");
            }

            // Validate delivery interval
            if (config.DeliveryInterval == 0)
            {
                throw new ArgumentException("Delivery interval is required");
            }
        }
    }

    public interface IReportGeneratorRepository
    {
        Task<IEnumerable<ReportDeliveryConfigurationModel>> GetReportDeliveryConfigurationsAsync();
        Task<ReportDeliveryConfigurationModel> GetReportDeliveryConfigurationAsync(int id);
        Task<int> CreateReportDeliveryConfigurationAsync(ReportDeliveryConfigurationModel config);
        Task UpdateReportDeliveryConfigurationAsync(ReportDeliveryConfigurationModel config);
        Task DeleteReportDeliveryConfigurationAsync(int id);
        Task GenerateReportAsync(FileType fileType, ReportDeliveryConfigurationModel config);
    }

Interface :

public interface IReportGeneratorRepository
    {
        Task<IEnumerable<ReportDeliveryConfigurationModel>> GetReportDeliveryConfigurationsAsync();
        Task<ReportDeliveryConfigurationModel> GetReportDeliveryConfigurationAsync(int id);
        Task<int> CreateReportDeliveryConfigurationAsync(ReportDeliveryConfigurationModel config);
        Task UpdateReportDeliveryConfigurationAsync(ReportDeliveryConfigurationModel config);
        Task DeleteReportDeliveryConfigurationAsync(int id);
        Task GenerateReportAsync(FileType fileType, ReportDeliveryConfigurationModel config);
        Task ValidateDestinationAsync(ReportDeliveryConfigurationModel config);
        Task ValidateDeliveryConfigurationAsync(ReportDeliveryConfigurationModel config);
        Task DeliverReportAsync(ReportDeliveryConfigurationModel config);
    }