using SchedulerApp.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SchedulerApp.Repositories
{
    public interface IReadRepository<T> where T : Schedule
    {
        Task<List<T>> Get();
        Task<T> Get(string id);
    }
}
