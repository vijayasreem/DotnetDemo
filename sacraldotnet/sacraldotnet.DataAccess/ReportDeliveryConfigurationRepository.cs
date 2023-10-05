
using Dapper;
using Npgsql;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace sacraldotnet.Repository
{
    public class ReportDeliveryConfigurationRepository : IReportDeliveryConfigurationService
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
                var sql = "INSERT INTO ReportDeliveryConfigurations (DestinationType, DestinationAddress, FrequencyType, DayOfWeek, DayOfMonth, DeliveryTime) " +
                          "VALUES (@DestinationType, @DestinationAddress, @FrequencyType, @DayOfWeek, @DayOfMonth, @DeliveryTime) RETURNING Id";

                var parameters = new
                {
                    DestinationType = (int)model.DestinationType,
                    DestinationAddress = model.DestinationAddress,
                    FrequencyType = (int)model.FrequencyType,
                    DayOfWeek = model.DayOfWeek,
                    DayOfMonth = model.DayOfMonth,
                    DeliveryTime = model.DeliveryTime
                };

                return await connection.ExecuteScalarAsync<int>(sql, parameters);
            }
        }

        public async Task<ReportDeliveryConfigurationModel> GetByIdAsync(int id)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                var sql = "SELECT * FROM ReportDeliveryConfigurations WHERE Id = @Id";
                var parameters = new { Id = id };

                return await connection.QuerySingleOrDefaultAsync<ReportDeliveryConfigurationModel>(sql, parameters);
            }
        }

        public async Task<IEnumerable<ReportDeliveryConfigurationModel>> GetAllAsync()
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                var sql = "SELECT * FROM ReportDeliveryConfigurations";

                return await connection.QueryAsync<ReportDeliveryConfigurationModel>(sql);
            }
        }

        public async Task UpdateAsync(ReportDeliveryConfigurationModel model)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                var sql = "UPDATE ReportDeliveryConfigurations SET DestinationType = @DestinationType, " +
                          "DestinationAddress = @DestinationAddress, FrequencyType = @FrequencyType, " +
                          "DayOfWeek = @DayOfWeek, DayOfMonth = @DayOfMonth, DeliveryTime = @DeliveryTime " +
                          "WHERE Id = @Id";

                await connection.ExecuteAsync(sql, model);
            }
        }

        public async Task DeleteAsync(int id)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                var sql = "DELETE FROM ReportDeliveryConfigurations WHERE Id = @Id";
                var parameters = new { Id = id };

                await connection.ExecuteAsync(sql, parameters);
            }
        }
    }
}
