using SchedulerApp.Models;
using SchedulerApp.Services.DataService;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;

namespace SchedulerApp.Repositories
{
    public class TeamScheduleRepository : IScheduleRepository<TeamSchedule>
    {
        private readonly ISqlDataService _sqlDataService;

        public TeamScheduleRepository(ISqlDataService sqlDataService)
        {
            _sqlDataService = sqlDataService;
        }

        public async Task Save(TeamSchedule schedule)
        {
            var pId = new SqlParameter("@id", schedule.Id);
            var pIdTeam = new SqlParameter("@id_team", schedule.Team.Id);
            var pCompetition = new SqlParameter("@competition", schedule.Competition);
            var pStream = new SqlParameter("@stream", schedule.Stream);
            var pDate = new SqlParameter("@date", schedule.Date);
            var pOpponent = new SqlParameter("@opponent", schedule.Opponent);

            try
            {
                await _sqlDataService.ExecuteNonQueryStoredProcedure("sp_InsertTeamSchedule", new[] { pId, pIdTeam, pCompetition, pStream, pDate, pOpponent });
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
