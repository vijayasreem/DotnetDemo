(string)result["document_library_name"],
                        ClientName = (string)result["client_name"],
                        DeliveryDate = (DateTime?)result["delivery_date"]
                    };

                    return model;
                }

                return null;
            }
        }
    }
}

namespace SacralDotNet.Repository
{
    using SacralDotNet;
    using System.Threading.Tasks;
    using Npgsql;

    public class ReportDeliveryConfigurationRepository : IReportDeliveryConfigurationRepository
    {
        private readonly string _connectionString;

        public ReportDeliveryConfigurationRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<int> CreateAsync(ReportDeliveryConfigurationModel model)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                var command = connection.CreateCommand();
                command.CommandText = @"INSERT INTO report_delivery_configuration (destination_type, destination_address, frequency_type, day_of_week, day_of_month, delivery_time, subject_line, body_text, template_formatting, ftp_url, password_credential, file_path, sharepoint_url, document_library_name, client_name, delivery_date) 
                                        VALUES (@destinationType, @destinationAddress, @frequencyType, @dayOfWeek, @dayOfMonth, @deliveryTime, @subjectLine, @bodyText, @templateFormatting, @ftpUrl, @passwordCredential, @filePath, @sharePointUrl, @documentLibraryName, @clientName, @deliveryDate) RETURNING id;";

                command.Parameters.AddWithValue("destinationType", model.DestinationType);
                command.Parameters.AddWithValue("destinationAddress", model.DestinationAddress);
                command.Parameters.AddWithValue("frequencyType", model.FrequencyType);
                command.Parameters.AddWithValue("dayOfWeek", model.DayOfWeek);
                command.Parameters.AddWithValue("dayOfMonth", model.DayOfMonth);
                command.Parameters.AddWithValue("deliveryTime", model.DeliveryTime);
                command.Parameters.AddWithValue("subjectLine", model.SubjectLine);
                command.Parameters.AddWithValue("bodyText", model.BodyText);
                command.Parameters.AddWithValue("templateFormatting", model.TemplateFormatting);
                command.Parameters.AddWithValue("ftpUrl", model.FTP_URL);
                command.Parameters.AddWithValue("passwordCredential", model.Password_Credential);
                command.Parameters.AddWithValue("filePath", model.FilePath);
                command.Parameters.AddWithValue("sharePointUrl", model.SharePointURL);
                command.Parameters.AddWithValue("documentLibraryName", model.DocumentLibraryName);
                command.Parameters.AddWithValue("clientName", model.ClientName);
                command.Parameters.AddWithValue("deliveryDate", model.DeliveryDate);

                return (int)await command.ExecuteScalarAsync();
            }
        }

        public async Task<ReportDeliveryConfigurationModel> GetAsync(int id)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                var command = connection.CreateCommand();
                command.CommandText = @"SELECT * FROM report_delivery_configuration WHERE id = @id;";
                command.Parameters.AddWithValue("id", id);

                var result = await command.ExecuteReaderAsync();

                if (result.Read())
                {
                    var model = new ReportDeliveryConfigurationModel
                    {
                        Id = (int)result["id"],
                        DestinationType = (DestinationType)result["destination_type"],
                        DestinationAddress = (string)result["destination_address"],
                        FrequencyType = (int)result["frequency_type"],
                        DayOfWeek = (int)result["day_of_week"],
                        DayOfMonth = (int)result["day_of_month"],
                        DeliveryTime = (TimeSpan)result["delivery_time"],
                        SubjectLine = (string)result["subject_line"],
                        BodyText = (string)result["body_text"],
                        TemplateFormatting = (string)result["template