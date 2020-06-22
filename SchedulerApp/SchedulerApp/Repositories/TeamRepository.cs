using SchedulerApp.Models;
using SchedulerApp.Models.Mappers;
using SchedulerApp.Services.DataService;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;

namespace SchedulerApp.Repositories
{
    public class TeamRepository : ITeamRepository
    {
        private readonly ISqlDataService _sqlDataService;

        public TeamRepository(ISqlDataService sqlDataService)
        {
            _sqlDataService = sqlDataService;
        }

        /// <summary>
        /// Get the list of teams that belong to a group
        /// </summary>
        /// <param name="groupId">Group ID</param>
        /// <returns>List of Teams</returns>
        public async Task<List<Team>> GetTeamsFromGroupAsync(string groupId)
        {
            Debug.WriteLine("### GetGroupTeamsAsync ###");

            var p1 = new SqlParameter("@IdGroup", groupId);

            try
            {
                return await _sqlDataService.ExecuteReadStoreProcedureAsync<List<Team>>("sp_GetTeamsByGroup", new[] { p1 }, Maps.ToTeams);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
