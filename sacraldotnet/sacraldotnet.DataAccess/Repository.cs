using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Npgsql;

namespace sacraldotnet
{
    public interface IRepository<TEntity> where TEntity : class
    {
        Task<TEntity> GetByIdAsync(int id);
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<int> InsertAsync(TEntity entity);
        Task<int> UpdateAsync(TEntity entity);
        Task<int> DeleteAsync(int id);
    }

    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private readonly string _connectionString;

        public Repository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<TEntity> GetByIdAsync(int id)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var command = connection.CreateCommand();
                command.CommandText = "SELECT * FROM " + typeof(TEntity).Name + " WHERE Id = @Id";
                command.Parameters.AddWithValue("@Id", id);

                using (var reader = await command.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                    {
                        return MapEntity(reader);
                    }
                }
            }

            return null;
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            var entities = new List<TEntity>();

            using (var connection = new NpgsqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var command = connection.CreateCommand();
                command.CommandText = "SELECT * FROM " + typeof(TEntity).Name;

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        entities.Add(MapEntity(reader));
                    }
                }
            }

            return entities;
        }

        public async Task<int> InsertAsync(TEntity entity)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var command = connection.CreateCommand();
                command.CommandText = "INSERT INTO " + typeof(TEntity).Name + " VALUES (@Id, @Name)";
                command.Parameters.AddWithValue("@Id", GetEntityId(entity));
                command.Parameters.AddWithValue("@Name", GetEntityName(entity));

                return await command.ExecuteNonQueryAsync();
            }
        }

        public async Task<int> UpdateAsync(TEntity entity)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var command = connection.CreateCommand();
                command.CommandText = "UPDATE " + typeof(TEntity).Name + " SET Name = @Name WHERE Id = @Id";
                command.Parameters.AddWithValue("@Id", GetEntityId(entity));
                command.Parameters.AddWithValue("@Name", GetEntityName(entity));

                return await command.ExecuteNonQueryAsync();
            }
        }

        public async Task<int> DeleteAsync(int id)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var command = connection.CreateCommand();
                command.CommandText = "DELETE FROM " + typeof(TEntity).Name + " WHERE Id = @Id";
                command.Parameters.AddWithValue("@Id", id);

                return await command.ExecuteNonQueryAsync();
            }
        }

        private TEntity MapEntity(IDataReader reader)
        {
            // Map the data reader to the entity object
            return null;
        }

        private int GetEntityId(TEntity entity)
        {
            // Get the entity ID
            return 0;
        }

        private string GetEntityName(TEntity entity)
        {
            // Get the entity name
            return null;
        }
    }

    public class ReportRepository : IRepository<Report>
    {
        private readonly Repository<Report> _repository;

        public ReportRepository(string connectionString)
        {
            _repository = new Repository<Report>(connectionString);
        }

        public async Task<Report> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<Report>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<int> InsertAsync(Report entity)
        {
            return await _repository.InsertAsync(entity);
        }

        public async Task<int> UpdateAsync(Report entity)
        {
            return await _repository.UpdateAsync(entity);
        }

        public async Task<int> DeleteAsync(int id)
        {
            return await _repository.DeleteAsync(id);
        }
    }

    public class Report
    {
        public int Id { get; set; }
        public string Name { get; set; }
        // Add additional report properties as needed
    }
}