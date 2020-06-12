using SchedulerApp.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SchedulerApp.Services.DataService
{
    public interface IDataService
    {
        Task<IEnumerable<Schedule>> Get();
        Task<Schedule> Get(string id);
        Task<Schedule> Save(Schedule item);
        void Delete(string id);
    }
}
