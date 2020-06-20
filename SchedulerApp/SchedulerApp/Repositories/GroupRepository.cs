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
    public class GroupRepository: IGroupRepository
    {
        private readonly ISqlDataService _sqlDataService;

        public GroupRepository(ISqlDataService sqlDataService)
        {
            _sqlDataService = sqlDataService;
        }

        public async Task<Group> GetGroupInfoAsync(string groupId)
        {
            Debug.WriteLine("### GetGroupInfoAsync ###");

            var p1 = new SqlParameter("@IdGroup", groupId);

            try
            {
                return await _sqlDataService.ExecuteStoreProcedureAsync<Group>("sp_GetGroupInfo", new[] { p1 }, Maps.ToGroup);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
