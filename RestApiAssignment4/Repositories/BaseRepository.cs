using System.Collections.Generic;
using System;
using System.Data.SqlClient;
using Dapper;

namespace RestApiAssignment4.Repositories
{
    public class BaseRepository<T> where T : class
    {
        private readonly string _connectionString;

        public BaseRepository(string connectionString)
        {
            _connectionString = connectionString;
        }
        public int GetRecentId(string query)
        {
            using var connection = new SqlConnection(_connectionString);
            return connection.Execute(query);
        }

        public IEnumerable<T> GetAll(string query)
        {
            using var connection = new SqlConnection(_connectionString);
            return connection.Query<T>(query);
        }

        public T Get(string query, object parameters)
        {
            using var connection = new SqlConnection(_connectionString);
            return connection.QuerySingleOrDefault<T>(query, parameters);
        }

        public int UpdateOrDelete(string query, object parameters)
        {
            using var connection = new SqlConnection(_connectionString);
            return connection.Execute(query, parameters);
        }
        
        public U Create<U>(string query, object parameters)
        {
            using var connection = new SqlConnection(_connectionString);
            return connection.QuerySingleOrDefault<U>(query, parameters);
        }
    }
}
