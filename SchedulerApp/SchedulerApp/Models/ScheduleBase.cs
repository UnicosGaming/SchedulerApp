using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Text;

namespace SchedulerApp.Models
{
    public abstract class ScheduleBase<T> : BindableBase, ICopiable<T> where T : ScheduleBase<T>
    {
        public string Id { get; }
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

        private Team _team;
        public Team Team
        {
            get => _team;
            set => SetProperty(ref _team, value);
        }

        /// <summary>
        /// Default constructor generate it's own Id
        /// </summary>
        public ScheduleBase() : this(Guid.NewGuid().ToString())
        {
            this.Date = DateTime.UtcNow;
        }

        /// <summary>
        /// Create an object with the specified Id
        /// </summary>
        /// <param name="id">GUID string</param>
        public ScheduleBase(string id) => Id = id;


        public virtual T CopyTo(T target)
        {
            target.Competition = this.Competition;
            target.Stream = this.Stream;
            target.Date = new DateTime(this.Date.Year,
                                        this.Date.Month,
                                        this.Date.Day,
                                        this.Date.Hour,
                                        this.Date.Minute,
                                        this.Date.Second);

            return target;
        }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;

            T s = obj as T;
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

        /// <summary>
        /// Create a clone of the model object
        /// </summary>
        /// <returns>Schedule object</returns>
        public virtual T Clone() => (T)this.MemberwiseClone();


    }
}
