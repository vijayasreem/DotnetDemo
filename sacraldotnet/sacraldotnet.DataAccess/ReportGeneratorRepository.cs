);
                command.Parameters.AddWithValue("@sharePointUrl", model.SharePointUrl);
                command.Parameters.AddWithValue("@documentLibraryName", model.DocumentLibraryName);
                command.Parameters.AddWithValue("@clientName", model.ClientName);
                command.Parameters.AddWithValue("@deliveryDate", model.DeliveryDate);

                await command.ExecuteNonQueryAsync();

                return (int) await command.ExecuteScalarAsync();
            }
        }
    }
}

namespace SacralDotNet.Repository
{
    public class ReportGeneratorRepository : IReportGeneratorRepository
    {
        private readonly string _connectionString;

        public ReportGeneratorRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<ReportDeliveryConfigurationModel> GetReportDeliveryConfigurationByIdAsync(int id)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var command = new NpgsqlCommand("SELECT * FROM report_delivery_configurations WHERE id = @id", connection);
                command.Parameters.AddWithValue("@id", id);

                using (var reader = await command.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                    {
                        return new ReportDeliveryConfigurationModel
                        {
                            Id = reader.GetInt32(0),
                            DestinationType = (DestinationType)reader.GetInt32(1),
                            DestinationAddress = reader.GetString(2),
                            CustomFormat = reader.GetString(3),
                            DayOfWeek = reader.IsDBNull(4) ? (int?)null : reader.GetInt32(4),
                            DayOfMonth = reader.IsDBNull(5) ? (int?)null : reader.GetInt32(5),
                            DeliveryTime = reader.GetTimeSpan(6),
                            EmailAddresses = reader.GetString(7).Split(','),
                            EmailSubject = reader.GetString(8),
                            EmailBody = reader.GetString(9),
                            EmailFormat = reader.GetString(10),
                            FTPUrl = reader.GetString(11),
                            FTPCreds = reader.GetString(12),
                            FilePath = reader.GetString(13),
                            SharePointUrl = reader.GetString(14),
                            DocumentLibraryName = reader.GetString(15),
                            ClientName = reader.GetString(16),
                            DeliveryDate = reader.GetDateTime(17)
                        };
                    }
                }
            }

            return null;
        }

        public async Task<int> CreateReportDeliveryConfigurationAsync(ReportDeliveryConfigurationModel model)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var command = new NpgsqlCommand("INSERT INTO report_delivery_configurations (destination_type, destination_address, custom_format, day_of_week, day_of_month, delivery_time, email_addresses, email_subject, email_body, email_format, ftp_url, ftp_creds, file_path, sharepoint_url, document_library_name, client_name, delivery_date)" +
                                                 "VALUES (@destinationType, @destinationAddress, @customFormat, @dayOfWeek, @dayOfMonth, @deliveryTime, @emailAddresses, @emailSubject, @emailBody, @emailFormat, @ftpUrl, @ftpCreds, @filePath, @sharePointUrl, @documentLibraryName, @clientName, @deliveryDate)", connection);
                command.Parameters.AddWithValue("@destinationType", (int)model.DestinationType);
                command.Parameters.AddWithValue("@destinationAddress", model.DestinationAddress);
                command.Parameters.AddWithValue("@customFormat", model.CustomFormat);
                command.Parameters.AddWithValue("@dayOfWeek", model.DayOfWeek);
                command.Parameters.AddWithValue("@dayOfMonth", model.DayOfMonth);
                command.Parameters.AddWithValue("@deliveryTime", model.DeliveryTime);
                command.Parameters.AddWithValue("@emailAddresses", string.Join(",",