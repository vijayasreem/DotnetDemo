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
            }
        }

        private async Task ValidateDeliveryConfigurationAsync(ReportDeliveryConfigurationModel config)
        {
            // Validate other delivery configuration
            if (config.CustomFormat == CustomFormat.Weekly || config.CustomFormat == CustomFormat.Monthly)
            {
                if (config.DeliveryTime == DateTime.MinValue)
                {
                    throw new ArgumentException("Delivery time cannot be empty");
                }
            }
        }
    }
}

namespace SacreddotNet.Repositories
{
    public class ReportGeneratorRepository : IReportGeneratorRepository
    {
        private string _connectionString;

        public ReportGeneratorRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<ReportDeliveryConfigurationModel> GetReportDeliveryConfigurationAsync(int id)
        {
            using (var con = new NpgsqlConnection(_connectionString))
            {
                var query = "SELECT * FROM ReportDeliveryConfiguration WHERE Id = @Id";
                var result = await con.QueryFirstOrDefaultAsync<ReportDeliveryConfigurationModel>(query, new { Id = id });
                return result;
            }
        }

        public async Task<int> CreateReportDeliveryConfigurationAsync(ReportDeliveryConfigurationModel config)
        {
            using (var con = new NpgsqlConnection(_connectionString))
            {
                var query = @"INSERT INTO ReportDeliveryConfiguration (DestinationType, DestinationAddress, CustomFormat, DayOfWeek, DayOfMonth, DeliveryTime, EmailAddresses, Subject, BodyText, EmailFormat, FTPUrl, FTPPassword, FilePath, SharePointUrl, DocumentLibraryName, ClientName, DeliveryDate) 
                              VALUES (@DestinationType, @DestinationAddress, @CustomFormat, @DayOfWeek, @DayOfMonth, @DeliveryTime, @EmailAddresses, @Subject, @BodyText, @EmailFormat, @FTPUrl, @FTPPassword, @FilePath, @SharePointUrl, @DocumentLibraryName, @ClientName, @DeliveryDate) 
                              RETURNING Id";
                var result = await con.ExecuteScalarAsync<int>(query, config);
                return result;
            }
        }

        public async Task UpdateReportDeliveryConfigurationAsync(ReportDeliveryConfigurationModel config)
        {
            using (var con = new NpgsqlConnection(_connectionString))
            {
                var query = @"UPDATE ReportDeliveryConfiguration 
                              SET DestinationType = @DestinationType, DestinationAddress = @DestinationAddress, CustomFormat = @CustomFormat, DayOfWeek = @DayOfWeek, DayOfMonth = @DayOfMonth, DeliveryTime = @DeliveryTime, EmailAddresses = @EmailAddresses, Subject = @Subject, BodyText = @BodyText, EmailFormat = @EmailFormat, FTPUrl = @FTPUrl, FTPPassword = @FTPPassword, FilePath = @FilePath, SharePointUrl = @SharePointUrl, DocumentLibraryName = @DocumentLibraryName, ClientName = @ClientName, DeliveryDate = @DeliveryDate 
                              WHERE Id = @Id";
                await con.ExecuteAsync(query, config);
            }
        }

        public async Task DeleteReportDeliveryConfigurationAsync(int id)
        {
            using (var con = new NpgsqlConnection(_connectionString))
            {
                var query = "DELETE FROM ReportDeliveryConfiguration WHERE Id = @Id";
                await con.ExecuteAsync(query, new { Id = id });
            }
        }

        public async Task<IEnumerable<ReportDeliveryConfigurationModel>> GetReportDeliveryConfigurationsAsync()
        {
            using (var con = new NpgsqlConnection(_connectionString))
            {
                var query = "SELECT * FROM ReportDeliveryConfiguration";
                var result = await con.QueryAsync<ReportDeliveryConfigurationModel>(query);
                return result;
            }
        }

        public async Task GenerateReportAsync(FileType fileType, ReportDeliveryConfigurationModel config)
        {
            switch (fileType)
            {
                case FileType.PDF:
                    //