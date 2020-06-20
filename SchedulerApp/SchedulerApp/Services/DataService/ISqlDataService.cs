using SchedulerApp.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace SchedulerApp.Services.DataService
{
    public interface ISqlDataService
    {
        //Task<Group> GetGroupInfoAsync(string groupId);
        //Task<List<Team>> GetGroupTeamsAsync(string groupId);
        //Task<Page> GetTeamPageAsync(string teamId);

        Task<T> ExecuteStoreProcedureAsync<T>(string sp_name, SqlParameter[] parameters, Func<SqlDataReader, T> mapper);
    }
}
