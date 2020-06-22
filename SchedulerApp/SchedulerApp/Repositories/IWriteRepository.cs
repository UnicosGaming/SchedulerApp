using SchedulerApp.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SchedulerApp.Repositories
{
    public interface IWriteRepository<T> where T: Schedule
    {
        Task Insert(T schedule);
        Task Update(T schedule);
    }
}
