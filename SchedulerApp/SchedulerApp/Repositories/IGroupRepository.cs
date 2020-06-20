using SchedulerApp.Models;
using System.Threading.Tasks;

namespace SchedulerApp.Repositories
{
    public interface IGroupRepository
    {
        Task<Group> GetGroupInfoAsync(string groupId);
    }
}
