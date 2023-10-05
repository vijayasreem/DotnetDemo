);
                command.Parameters.AddWithValue("@sharePointUrl", model.SharePointUrl);
                command.Parameters.AddWithValue("@documentLibraryName", model.DocumentLibraryName);
                command.Parameters.AddWithValue("@clientName", model.ClientName);
                command.Parameters.AddWithValue("@deliveryDate", model.DeliveryDate);

                return await command.ExecuteNonQueryAsync();
            }
        }
    }
}

using SacralDotNet.DTO;

namespace SacralDotNet.Service
{
    public interface IReportGeneratorRepository
    {
        Task<ReportDeliveryConfigurationModel> GetReportDeliveryConfigurationByIdAsync(int id);
        Task<int> CreateReportDeliveryConfigurationAsync(ReportDeliveryConfigurationModel model);
    }
}