using SchedulerApp.Models;
using System.Threading.Tasks;

namespace SchedulerApp.Repositories
{
    public interface IPageRepository
    {
        Task<Page> GetPageByTeamAsync(string teamId);
    }
}
