using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using SchedulerApp.Models;
using SchedulerApp.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SchedulerApp.ViewModels
{
    public class MotorSchedulePageViewModel : ViewModelBase
    {
        private readonly IPageDialogService _pageDialogService;

        private MotorSchedule _schedule;
        public MotorSchedule Schedule
        {
            get => _schedule;
            set => SetProperty(ref _schedule, value);
        }

        public MotorSchedulePageViewModel(INavigationService navigationService, IPageDialogService pageDialogService):base(navigationService)
        {
            _pageDialogService = pageDialogService;
        }
    }
}
