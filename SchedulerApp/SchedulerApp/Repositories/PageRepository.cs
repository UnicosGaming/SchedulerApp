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
    public class PageRepository : IPageRepository
    {
        private readonly ISqlDataService _sqlDataService;

        public PageRepository(ISqlDataService sqlDataService)
        {
            _sqlDataService = sqlDataService;
        }

        /// <summary>
        /// Get the page associated to a team
        /// </summary>
        /// <param name="teamId">Team ID</param>
        /// <returns>The team Page</returns>
        public async Task<Page> GetPageByTeamAsync(string teamId)
        {
            Debug.WriteLine("### GetTeamPageAsync ###");

            var p1 = new SqlParameter("@IdTeam", teamId);

            try
            {
                return await _sqlDataService.ExecuteReadStoreProcedureAsync<Page>("sp_GetPageByTeam", new[] { p1 }, Maps.ToPage);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
