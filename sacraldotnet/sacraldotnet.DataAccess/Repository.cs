using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Npgsql;

namespace sacraldotnet
{
    public class ReportConfigurationRepository : IReportConfigurationRepository
    {
        private readonly string connectionString;

        public ReportConfigurationRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public async Task<List<ReportConfigurationModel>> GetAllAsync()
        {
            using (var connection = new NpgsqlConnection(connectionString))
            {
                await connection.OpenAsync();

                using (var command = new NpgsqlCommand("SELECT * FROM ReportConfiguration", connection))
                {
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        var reportConfigurations = new List<ReportConfigurationModel>();

                        while (await reader.ReadAsync())
                        {
                            var reportConfiguration = new ReportConfigurationModel
                            {
                                Id = reader.GetInt32(0),
                                FileType = reader.GetString(1),
                                Destination = reader.GetString(2),
                                Frequency = reader.GetString(3)
                            };

                            reportConfigurations.Add(reportConfiguration);
                        }

                        return reportConfigurations;
                    }
                }
            }
        }

        public async Task<ReportConfigurationModel> GetByIdAsync(int id)
        {
            using (var connection = new NpgsqlConnection(connectionString))
            {
                await connection.OpenAsync();

                using (var command = new NpgsqlCommand("SELECT * FROM ReportConfiguration WHERE Id = @id", connection))
                {
                    command.Parameters.AddWithValue("@id", id);

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            var reportConfiguration = new ReportConfigurationModel
                            {
                                Id = reader.GetInt32(0),
                                FileType = reader.GetString(1),
                                Destination = reader.GetString(2),
                                Frequency = reader.GetString(3)
                            };

                            return reportConfiguration;
                        }
                    }
                }
            }

            return null;
        }

        public async Task<int> CreateAsync(ReportConfigurationModel reportConfiguration)
        {
            using (var connection = new NpgsqlConnection(connectionString))
            {
                await connection.OpenAsync();

                using (var command = new NpgsqlCommand("INSERT INTO ReportConfiguration (FileType, Destination, Frequency) VALUES (@fileType, @destination, @frequency) RETURNING Id", connection))
                {
                    command.Parameters.AddWithValue("@fileType", reportConfiguration.FileType);
                    command.Parameters.AddWithValue("@destination", reportConfiguration.Destination);
                    command.Parameters.AddWithValue("@frequency", reportConfiguration.Frequency);

                    return (int)await command.ExecuteScalarAsync();
                }
            }
        }

        public async Task<bool> UpdateAsync(ReportConfigurationModel reportConfiguration)
        {
            using (var connection = new NpgsqlConnection(connectionString))
            {
                await connection.OpenAsync();

                using (var command = new NpgsqlCommand("UPDATE ReportConfiguration SET FileType = @fileType, Destination = @destination, Frequency = @frequency WHERE Id = @id", connection))
                {
                    command.Parameters.AddWithValue("@fileType", reportConfiguration.FileType);
                    command.Parameters.AddWithValue("@destination", reportConfiguration.Destination);
                    command.Parameters.AddWithValue("@frequency", reportConfiguration.Frequency);
                    command.Parameters.AddWithValue("@id", reportConfiguration.Id);

                    return await command.ExecuteNonQueryAsync() > 0;
                }
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            using (var connection = new NpgsqlConnection(connectionString))
            {
                await connection.OpenAsync();

                using (var command = new NpgsqlCommand("DELETE FROM ReportConfiguration WHERE Id = @id", connection))
                {
                    command.Parameters.AddWithValue("@id", id);

                    return await command.ExecuteNonQueryAsync() > 0;
                }
            }
        }
    }

    // Add similar repository classes for other models (VendorModel, ReportFileModel, EmailAttachmentModel, etc.) here.
}