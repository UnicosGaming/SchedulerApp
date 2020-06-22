using System;
using System.Collections.Generic;
using System.Text;

namespace SchedulerApp.Models
{
    public class Schedule : ScheduleBase<Schedule>
    {
        public Schedule() : base() { }
        public Schedule(string id) : base(id) { }
    }
}
