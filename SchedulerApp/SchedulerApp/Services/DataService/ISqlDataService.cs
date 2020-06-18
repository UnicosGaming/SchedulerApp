using SchedulerApp.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SchedulerApp.Services.DataService
{
    public interface ISqlDataService
    {
        Task<Group> GetGroupInfoAsync(string groupId);
        Task<List<Team>> GetGroupTeamsAsync(string groupId);
        Task<Page> GetTeamPageAsync(string teamId);
    }
}
