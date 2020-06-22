using System;
using System.Collections.Generic;
using System.Text;

namespace SchedulerApp.Models
{
    public class MotorSchedule : Schedule
    {
        private string _car;
        public string Car
        {
            get => _car;
            set => SetProperty(ref _car, value);
        }

        private string _track;
        public string Track
        {
            get => _track;
            set => SetProperty(ref _track, value);
        }

        public MotorSchedule() : base() { }
        public MotorSchedule(string id) : base(id) { }

    }
}
