using Dapper;
using MetaNet.Microservices.Domain.Repositories.Base;
using System.Data;
using System.Reflection;


namespace MetaNet.Microservices.Infrastructure.Repositories.Dapper.Base
{
    public abstract class GenericDapperRepository<T> : IGenericDapperRepository<T> where T : class
    {
        private readonly IDbConnection _dbConnection;

        public GenericDapperRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            var tableName = GetTableName(typeof(T));
            var query = $"SELECT * FROM {tableName}";
            return await _dbConnection.QueryAsync<T>(query);
        }

        public async Task<T> GetByIdAsync(Guid id)
        {
            var tableName = GetTableName(typeof(T));
            var query = $"SELECT * FROM {tableName} WHERE Id = @Id";
            return await _dbConnection.QueryFirstOrDefaultAsync<T>(query, new { Id = id });
        }

        public async Task<int> AddAsync(T entity)
        {
            var tableName = GetTableName(typeof(T));
            var properties = GetProperties(entity);
            var columns = string.Join(", ", properties.Select(p => p.Name));
            var values = string.Join(", ", properties.Select(p => "@" + p.Name));
            var query = $"INSERT INTO {tableName} ({columns}) VALUES ({values})";

            return await _dbConnection.ExecuteAsync(query, entity);
        }

        public async Task<int> UpdateAsync(T entity)
        {
            var tableName = GetTableName(typeof(T));
            var properties = GetProperties(entity);
            var setClause = string.Join(", ", properties.Select(p => $"{p.Name} = @{p.Name}"));
            var query = $"UPDATE {tableName} SET {setClause} WHERE Id = @Id";

            return await _dbConnection.ExecuteAsync(query, entity);
        }

        public async Task<int> DeleteAsync(int id)
        {
            var tableName = GetTableName(typeof(T));
            var query = $"DELETE FROM {tableName} WHERE Id = @Id";
            return await _dbConnection.ExecuteAsync(query, new { Id = id });
        }

        private static string GetTableName(Type type)
        {
            return type.Name + "s";
        }

        private static IEnumerable<PropertyInfo> GetProperties(T entity)
        {
            return typeof(T).GetProperties().Where(p => p.CanRead && p.CanWrite);
        }
    }
}
