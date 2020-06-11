using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Text;

namespace SchedulerApp.Models
{
    public class Schedule : ModelBase<Schedule>
    {
        public string Id { get; set; }

        private DateTime _date;
        public DateTime Date
        {
            get => _date;
            set => SetProperty(ref _date, value);
        }

        private string _competition;
        public string Competition
        {
            get => _competition;
            set => SetProperty(ref _competition, value);
        }

        private string _stream;
        public string Stream
        {
            get => _stream;
            set => SetProperty(ref _stream, value);
        }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;

            Schedule s = obj as Schedule;
            if (s == null) return false;

            return (Id == s.Id) &&
                (Competition == s.Competition) &&
                (Stream == s.Stream) &&
                (Date.Day == s.Date.Day) &&
                (Date.Month == s.Date.Month) &&
                (Date.Year == s.Date.Year) &&
                (Date.Hour == s.Date.Hour) &&
                (Date.Minute == s.Date.Minute);
        }

    }
}
