using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Threading.Tasks;
using Npgsql;

namespace sacraldotnet.Repository
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private readonly string _connectionString;

        public Repository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            using (var connection = GetOpenConnection())
            {
                var command = connection.CreateCommand();
                command.CommandText = $"SELECT * FROM {GetTableName()}";
                return await ReadEntitiesAsync(command);
            }
        }

        public async Task<TEntity> GetByIdAsync(int id)
        {
            using (var connection = GetOpenConnection())
            {
                var command = connection.CreateCommand();
                command.CommandText = $"SELECT * FROM {GetTableName()} WHERE Id = @Id";
                command.Parameters.Add(CreateParameter("Id", id));
                return await ReadEntityAsync(command);
            }
        }

        public async Task<TEntity> AddAsync(TEntity entity)
        {
            using (var connection = GetOpenConnection())
            {
                var command = connection.CreateCommand();
                command.CommandText = GetInsertCommandText();
                AddEntityParameters(command, entity);
                await command.ExecuteNonQueryAsync();
                return entity;
            }
        }

        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            using (var connection = GetOpenConnection())
            {
                var command = connection.CreateCommand();
                command.CommandText = GetUpdateCommandText();
                AddEntityParameters(command, entity);
                await command.ExecuteNonQueryAsync();
                return entity;
            }
        }

        public async Task DeleteAsync(int id)
        {
            using (var connection = GetOpenConnection())
            {
                var command = connection.CreateCommand();
                command.CommandText = $"DELETE FROM {GetTableName()} WHERE Id = @Id";
                command.Parameters.Add(CreateParameter("Id", id));
                await command.ExecuteNonQueryAsync();
            }
        }

        private async Task<IEnumerable<TEntity>> ReadEntitiesAsync(DbCommand command)
        {
            var entities = new List<TEntity>();
            using (var reader = await command.ExecuteReaderAsync())
            {
                while (await reader.ReadAsync())
                {
                    var entity = Activator.CreateInstance<TEntity>();
                    PopulateEntity(reader, entity);
                    entities.Add(entity);
                }
            }
            return entities;
        }

        private async Task<TEntity> ReadEntityAsync(DbCommand command)
        {
            using (var reader = await command.ExecuteReaderAsync())
            {
                if (await reader.ReadAsync())
                {
                    var entity = Activator.CreateInstance<TEntity>();
                    PopulateEntity(reader, entity);
                    return entity;
                }
                return null;
            }
        }

        private void PopulateEntity(DbDataReader reader, TEntity entity)
        {
            // TODO: Implement logic to populate entity properties from reader columns
        }

        private void AddEntityParameters(DbCommand command, TEntity entity)
        {
            // TODO: Implement logic to add parameters to the command based on entity properties
        }

        private DbParameter CreateParameter(string name, object value)
        {
            var parameter = new NpgsqlParameter(name, value);
            return parameter;
        }

        private string GetTableName()
        {
            // TODO: Implement logic to determine the table name based on TEntity type
            return typeof(TEntity).Name;
        }

        private string GetInsertCommandText()
        {
            // TODO: Implement logic to generate the INSERT command text based on TEntity type
            return "";
        }

        private string GetUpdateCommandText()
        {
            // TODO: Implement logic to generate the UPDATE command text based on TEntity type
            return "";
        }

        private NpgsqlConnection GetOpenConnection()
        {
            var connection = new NpgsqlConnection(_connectionString);
            connection.Open();
            return connection;
        }
    }
}