using Dapper;
using DemoApplication.BusinessEntity;
using DemoApplication.Repository.Interface;
using Microsoft.Extensions.Options;
using System.Data;
using System.Data.SqlClient;

namespace DemoApplication.Repository
{
    public class BaseRepository : IBaseRepository
    {
        /// <summary>
        /// Connection object
        /// </summary>
        protected IDbConnection connection;

        /// <summary>
        /// Connection string Object
        /// </summary>
        protected string connectionString;

        /// <summary>
        /// IOption Connection config for DI of app setting
        /// </summary>
        protected IOptions<ConnectionSettings> connectionFactory;

        public IOptions<ConnectionSettings> ConnectionSettings
        {
            get { return connectionFactory; }
        }

        /// <summary>
        /// Default constructor init
        /// </summary>
        /// <param name="config"></param>
        public BaseRepository(IOptions<ConnectionSettings> config)
        {
            connectionFactory = config;
        }

        /// <summary>
        /// Default Connection Creation
        /// </summary>
        /// <returns></returns>
        public IDbConnection CreateConnection()
        {
            connectionString = connectionFactory.Value.DefaultConnection;
            if (!string.IsNullOrEmpty(connectionString))
            {
                if (connection != null && connection.State != ConnectionState.Closed)
                {
                    try
                    {
                        connection.Close();
                        connection.Dispose();
                    }
                    catch { }
                }

                return new SqlConnection(connectionString);
            }
            return null;
        }

        /// <summary>
        /// Generic Method for Single Result
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sqlQuery"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        protected T QueryFirstOrDefault<T>(string sqlQuery, object parameters = null, bool isStoreProcedure = true)
        {
            using (var connection = CreateConnection())
            {
                if (isStoreProcedure)
                    return connection.QueryFirstOrDefault<T>(sqlQuery, parameters, null, null, CommandType.StoredProcedure);
                else
                    return connection.QueryFirstOrDefault<T>(sqlQuery, parameters);
            }
        }

        /// <summary>
        /// Generic Method for List
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sqlQuery"></param>
        /// <param name="parameters"></param>
        /// <param name="isStoreProcedure"></param>
        /// <returns></returns>
        protected List<T> Query<T>(string sqlQuery, object parameters = null, bool isStoreProcedure = true)
        {
            try
            {
                using (var connection = CreateConnection())
                {
                    if (isStoreProcedure)
                        return connection.Query<T>(sqlQuery, parameters, null, false, null, CommandType.StoredProcedure).ToList();
                    else
                        return connection.Query<T>(sqlQuery, parameters).ToList();
                }
            }
            catch (SqlException ex)
            {
                throw;
            }
        }

        /// <summary>
        /// Generic method for sp execution
        /// </summary>
        /// <param name="sqlQuery"></param>
        /// <param name="parameters"></param>
        /// <param name="isStoreProcedure"></param>
        /// <returns></returns>
        protected int Execute(string sqlQuery, object parameters = null, bool isStoreProcedure = true)
        {
            using (var connection = CreateConnection())
            {
                if (isStoreProcedure)
                    return connection.Execute(sqlQuery, parameters, null, null, CommandType.StoredProcedure);
                else
                    return connection.Execute(sqlQuery, parameters);
            }
        }

        /// <summary>
        /// Generic method for execute scalar
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sqlQuery"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        protected T ExecuteScalar<T>(string sqlQuery, object parameters = null, bool isStoreProcedure = true)
        {
            using (var connection = CreateConnection())
            {
                if (isStoreProcedure)
                    return connection.ExecuteScalar<T>(sqlQuery, parameters, null, null, CommandType.StoredProcedure);
                else
                    return connection.ExecuteScalar<T>(sqlQuery, parameters);
            }
        }
    }
}