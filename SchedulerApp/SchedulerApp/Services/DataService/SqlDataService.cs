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

        public async Task<Group> GetGroupInfoAsync(string groupId)
        {
            Debug.WriteLine("### GetGroupInfoAsync ###");

            var p1 = new SqlParameter("@IdGroup", groupId);

            try
            {
                return await ExecuteStoreProcedureAsync<Group>("sp_GetGroupInfo", new[] { p1 }, Maps.ToGroup);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Get the list of teams that belong to a group
        /// </summary>
        /// <param name="groupId">Group ID</param>
        /// <returns>List of Teams</returns>
        public async Task<List<Team>> GetGroupTeamsAsync(string groupId)
        {
            Debug.WriteLine("### GetGroupTeamsAsync ###");

            var p1 = new SqlParameter("@IdGroup", groupId);

            try
            {
                return await ExecuteStoreProcedureAsync<List<Team>>("sp_GetTeamsByGroup", new[] { p1 }, Maps.ToTeams);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Get the page associated to a team
        /// </summary>
        /// <param name="teamId">Team ID</param>
        /// <returns>The team Page</returns>
        public async Task<Page> GetTeamPageAsync(string teamId)
        {
            Debug.WriteLine("### GetTeamPageAsync ###");

            var p1 = new SqlParameter("@IdTeam", teamId);

            try
            {
                return await ExecuteStoreProcedureAsync<Page>("sp_GetPageByTeam", new[] { p1 }, Maps.ToPage);
            }
            catch (Exception)
            {
                throw;
            }

        }

        /// <summary>
        /// Execute an stored procedure @sp_name with the @parameters
        /// </summary>
        /// <param name="sp_name">Name of the stored procedure</param>
        /// <param name="parameters">Array of parameters</param>
        /// <returns>@SqlDataReader with the results</returns>
        public async Task<T> ExecuteStoreProcedureAsync<T>(string sp_name, SqlParameter[] parameters, Func<SqlDataReader, T> mapper)
        {
            Debug.WriteLine("### ExecuteStoreProcedureAsync ###");
            Debug.WriteLine(_connectionString);

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
    }
}
