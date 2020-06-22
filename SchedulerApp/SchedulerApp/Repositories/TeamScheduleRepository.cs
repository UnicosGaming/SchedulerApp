using SchedulerApp.Models;
using SchedulerApp.Models.Mappers;
using SchedulerApp.Services.DataService;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SchedulerApp.Repositories
{
    public class TeamScheduleRepository : IWriteRepository<TeamSchedule>, IReadRepository<TeamSchedule>, IDeleteRepository<TeamSchedule>
    {
        private readonly ISqlDataService _sqlDataService;

        public TeamScheduleRepository(ISqlDataService sqlDataService)
        {
            _sqlDataService = sqlDataService;



        }

        public async Task Insert(TeamSchedule schedule)
        {
            var pId = new SqlParameter("@id", schedule.Id);
            var pIdTeam = new SqlParameter("@id_team", schedule.Team.Id);
            var pCodeTeam = new SqlParameter("@code_team", schedule.Team.Code);
            var pCompetition = new SqlParameter("@competition", schedule.Competition);
            var pStream = new SqlParameter("@stream", schedule.Stream);
            var pDate = new SqlParameter("@date", schedule.Date);
            var pOpponent = new SqlParameter("@opponent", schedule.Opponent);

            try
            {
                await _sqlDataService.ExecuteNonQueryStoredProcedure("sp_InsertTeamSchedule", new[] { pId, pIdTeam, pCodeTeam, pCompetition, pStream, pDate, pOpponent });
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task Update(TeamSchedule schedule)
        {
            var pId = new SqlParameter("@id", schedule.Id);
            var pCompetition = new SqlParameter("@competition", schedule.Competition);
            var pStream = new SqlParameter("@stream", schedule.Stream);
            var pDate = new SqlParameter("@date", schedule.Date);
            var pOpponent = new SqlParameter("@opponent", schedule.Opponent);

            try
            {
                await _sqlDataService.ExecuteNonQueryStoredProcedure("sp_UpdateTeamSchedule", new[] { pId, pCompetition, pStream, pDate, pOpponent });
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<TeamSchedule>> GetAll(string group_id, int skip = 0)
        {
            throw new NotImplementedException();
        }

        public async Task<TeamSchedule> Get(string id)
        {
            var pId = new SqlParameter("@id", id);

            try
            {
                return await _sqlDataService.ExecuteReadStoreProcedureAsync<TeamSchedule>("sp_GetTeamDetails", new[] { pId }, Maps.ToTeamSchedule);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async void Delete(string id)
        {
            var pId = new SqlParameter("@id", id);

            try
            {
                await _sqlDataService.ExecuteNonQueryStoredProcedure("sp_DeleteTeamSchedule", new[] { pId });
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
