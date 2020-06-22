using SchedulerApp.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SchedulerApp.Repositories
{
    public interface IDeleteRepository<T> where T : Schedule
    {
        void Delete(string id);
    }
}
