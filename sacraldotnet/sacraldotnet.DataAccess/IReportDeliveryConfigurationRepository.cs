(string)result["document_library_name"],
                        ClientName = (string)result["client_name"],
                        DeliveryDate = (DateTime)result["delivery_date"]
                    };

                    return model;
                }

                return null;
            }
        }
    }
}

namespace SacralDotNet.Service
{
    using SacralDotNet.DTO;

    public interface IReportDeliveryConfigurationRepository
    {
        Task<int> CreateAsync(ReportDeliveryConfigurationModel model);
        Task<ReportDeliveryConfigurationModel> GetAsync(int id);
    }
}