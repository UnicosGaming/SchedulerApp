using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using SchedulerApp.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace SchedulerApp.ViewModels
{
    public class SchedulePageViewModel : ViewModelBase
    {
        private Schedule _schedule;
        public Schedule Schedule
        {
            get => _schedule;
            set => SetProperty(ref _schedule, value);
        }

        private TimeSpan _time;
        public TimeSpan Time
        {
            get
            {
                if (_schedule is null) return default;

                return _schedule.Date.TimeOfDay;
            }
            set
            {
                _schedule.Date = new DateTime(_schedule.Date.Year, _schedule.Date.Month, _schedule.Date.Day, value.Hours, value.Minutes, 0);
                _time = value;
                RaisePropertyChanged(nameof(Time));
            }
        }

        public SchedulePageViewModel(INavigationService navigationService) : base(navigationService)
        {

        }

        public override void Initialize(INavigationParameters parameters)
        {
            // Save the the time value before setting the Schedule property because after that the value will lose.
            _time = (parameters["model"] as Schedule).Date.TimeOfDay;

            Schedule = parameters["model"] as Schedule;
            Time = _time;
        }
    }
}
