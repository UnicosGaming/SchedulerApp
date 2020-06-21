using SchedulerApp.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SchedulerApp.Repositories
{
    public interface IScheduleRepository<T> where T: Schedule
    {
        Task Save(T schedule);
    }
}
