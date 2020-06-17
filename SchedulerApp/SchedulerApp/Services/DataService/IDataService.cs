using SchedulerApp.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SchedulerApp.Services.DataService
{
    public interface IDataService
    {
        Task<IEnumerable<TeamSchedule>> Get();
        Task<TeamSchedule> Get(string id);
        Task<TeamSchedule> Save(TeamSchedule item);
        void Delete(string id);
    }
}
