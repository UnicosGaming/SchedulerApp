using System;
using System.Collections.Generic;
using System.Text;

namespace SchedulerApp.Models
{
    public class MotorSchedule : ScheduleBase<MotorSchedule>
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

    }
}
