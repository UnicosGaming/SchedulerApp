using SchedulerApp.Models;
using SchedulerApp.Models.Mappers;
using SchedulerApp.Services.DataService;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace SchedulerApp.Repositories
{
    public class ScheduleRepository : IReadRepository<Schedule>
    {
        private readonly ISqlDataService _sqlDataService;

        public ScheduleRepository(ISqlDataService sqlDataService)
        {
            _sqlDataService = sqlDataService;
        }

        public async Task<List<Schedule>> GetAll(string group_id, int skip = 0)
        {
            var pSkip = new SqlParameter("@rowsToSkip", System.Data.SqlDbType.TinyInt) { Value = skip };
            var pIdGroup = new SqlParameter("@id_group", group_id);

            try
            {
                return await _sqlDataService.ExecuteReadStoreProcedureAsync<List<Schedule>>("sp_GetNextSchedules", new[] { pSkip, pIdGroup }, Maps.ToSchedules);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Schedule> Get(string id)
        {
            throw new NotImplementedException();
        }
    }
}
