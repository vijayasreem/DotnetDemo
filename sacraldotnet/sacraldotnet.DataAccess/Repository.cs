using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Threading.Tasks;
using Npgsql;

namespace sacraldotnet
{
    public interface IService<T>
    {
        Task<List<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id);
        Task<int> CreateAsync(T entity);
        Task<int> UpdateAsync(T entity);
        Task<int> DeleteAsync(int id);
    }

    public class Service<T> : IService<T> where T : IEntity
    {
        private readonly string _connectionString;

        public Service(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<List<T>> GetAllAsync()
        {
            using (var connection = GetConnection())
            {
                await connection.OpenAsync();

                using (var command = connection.CreateCommand())
                {
                    command.CommandText = $"SELECT * FROM {typeof(T).Name.ToLower()}";

                    var entities = new List<T>();

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            entities.Add(MapEntity(reader));
                        }
                    }

                    return entities;
                }
            }
        }

        public async Task<T> GetByIdAsync(int id)
        {
            using (var connection = GetConnection())
            {
                await connection.OpenAsync();

                using (var command = connection.CreateCommand())
                {
                    command.CommandText = $"SELECT * FROM {typeof(T).Name.ToLower()} WHERE id = @id";
                    command.Parameters.AddWithValue("@id", id);

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            return MapEntity(reader);
                        }

                        return default;
                    }
                }
            }
        }

        public async Task<int> CreateAsync(T entity)
        {
            using (var connection = GetConnection())
            {
                await connection.OpenAsync();

                using (var command = connection.CreateCommand())
                {
                    command.CommandText = $"INSERT INTO {typeof(T).Name.ToLower()} VALUES (@id, @name)";
                    command.Parameters.AddWithValue("@id", entity.Id);
                    command.Parameters.AddWithValue("@name", entity.Name);

                    return await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task<int> UpdateAsync(T entity)
        {
            using (var connection = GetConnection())
            {
                await connection.OpenAsync();

                using (var command = connection.CreateCommand())
                {
                    command.CommandText = $"UPDATE {typeof(T).Name.ToLower()} SET name = @name WHERE id = @id";
                    command.Parameters.AddWithValue("@id", entity.Id);
                    command.Parameters.AddWithValue("@name", entity.Name);

                    return await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task<int> DeleteAsync(int id)
        {
            using (var connection = GetConnection())
            {
                await connection.OpenAsync();

                using (var command = connection.CreateCommand())
                {
                    command.CommandText = $"DELETE FROM {typeof(T).Name.ToLower()} WHERE id = @id";
                    command.Parameters.AddWithValue("@id", id);

                    return await command.ExecuteNonQueryAsync();
                }
            }
        }

        private T MapEntity(DbDataReader reader)
        {
            var entity = Activator.CreateInstance<T>();

            entity.Id = reader.GetInt32(reader.GetOrdinal("id"));
            entity.Name = reader.GetString(reader.GetOrdinal("name"));

            return entity;
        }

        private IDbConnection GetConnection()
        {
            return new NpgsqlConnection(_connectionString);
        }
    }

    public interface IEntity
    {
        int Id { get; set; }
        string Name { get; set; }
    }
}