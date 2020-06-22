using SchedulerApp.Configuration;
using SchedulerApp.Models;
using SchedulerApp.Models.Mappers;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SchedulerApp.Services.DataService
{
    public class SqlDataService : ISqlDataService
    {
        private string _connectionString => Secrets.ConnectionString;

        /// <summary>
        /// Execute an stored procedure @sp_name with the @parameters
        /// </summary>
        /// <param name="sp_name">Name of the stored procedure</param>
        /// <param name="parameters">Array of parameters</param>
        /// <returns>@SqlDataReader with the results</returns>
        public async Task<T> ExecuteReadStoreProcedureAsync<T>(string sp_name, SqlParameter[] parameters, Func<SqlDataReader, T> mapper)
        {
            Debug.WriteLine("### ExecuteStoreProcedureAsync ###");

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    SqlCommand command = new SqlCommand(sp_name, connection)
                    {
                        CommandType = System.Data.CommandType.StoredProcedure
                    };

                    command.Parameters.AddRange(parameters);

                    connection.Open();
                    var reader = await command.ExecuteReaderAsync();

                    return mapper(reader);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Execute the stored procedure @sp_name with the @parameters
        /// </summary>
        /// <param name="sp_name">Name of the stored procedure</param>
        /// <param name="parameters">Array of parameters</param>
        /// <returns>Number of rows affected</returns>
        public async Task<int> ExecuteNonQueryStoredProcedure(string sp_name, SqlParameter[] parameters)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    SqlCommand command = new SqlCommand(sp_name, connection)
                    {
                        CommandType = System.Data.CommandType.StoredProcedure
                    };

                    command.Parameters.AddRange(parameters);

                    connection.Open();

                    var result = await command.ExecuteNonQueryAsync();

                    return result;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
