using System;
using System.Collections.Generic;
using System.Text;

namespace SchedulerApp.Models
{
    public class Schedule
    {
        public string Id { get; set; }
        public DateTime Date { get; set; }
        public string Competition { get; set; }
        public string Stream { get; set; }

    }
}
