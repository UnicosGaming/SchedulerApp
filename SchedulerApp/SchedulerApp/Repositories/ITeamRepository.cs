using SchedulerApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SchedulerApp.Repositories
{
    public interface ITeamRepository
    {
        Task<List<Team>> GetTeamsFromGroupAsync(string groupId);
    }
}
