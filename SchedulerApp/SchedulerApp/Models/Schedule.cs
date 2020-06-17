using Newtonsoft.Json;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Text;

namespace SchedulerApp.Models
{
    public class Schedule : ModelBase<Schedule>, ICopiable<Schedule>
    {
        // CTORs
        public Schedule() : base()
        {
            this.Date = DateTime.UtcNow;
        }
        public Schedule(string id) : base(id) { }

        //public override bool Equals(object obj)
        //{
        //    if (obj == null) return false;

        //    Schedule s = obj as Schedule;
        //    if (s == null) return false;

        //    return (Id == s.Id) &&
        //        (Competition == s.Competition) &&
        //        (Stream == s.Stream) &&
        //        (Date.Day == s.Date.Day) &&
        //        (Date.Month == s.Date.Month) &&
        //        (Date.Year == s.Date.Year) &&
        //        (Date.Hour == s.Date.Hour) &&
        //        (Date.Minute == s.Date.Minute);
        //}

        //public Schedule CopyTo(Schedule target)
        //{
        //    target.Competition = this.Competition;
        //    target.Stream = this.Stream;
        //    target.Date = new DateTime(this.Date.Year,
        //                                this.Date.Month,
        //                                this.Date.Day,
        //                                this.Date.Hour,
        //                                this.Date.Minute,
        //                                this.Date.Second);

        //    return target;
        //}

    }
}
