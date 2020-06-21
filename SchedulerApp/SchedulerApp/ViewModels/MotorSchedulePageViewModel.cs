using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using SchedulerApp.Models;
using SchedulerApp.Repositories;
using SchedulerApp.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchedulerApp.ViewModels
{
    public class MotorSchedulePageViewModel : ViewModelBase
    {
        private MotorSchedule _originalItem;


        private readonly IPageDialogService _pageDialogService;
        private readonly IMotorScheduleRepository _motorScheduleRepository;

        public DelegateCommand SaveCommand { get; private set; }

        private MotorSchedule _schedule;
        public MotorSchedule Schedule
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

        public MotorSchedulePageViewModel(INavigationService navigationService, IPageDialogService pageDialogService, IMotorScheduleRepository motorScheduleRepository):base(navigationService)
        {
            _pageDialogService = pageDialogService;
            _motorScheduleRepository = motorScheduleRepository;

            SaveCommand = new DelegateCommand(async () => await SaveAsync());
        }

        public override void Initialize(INavigationParameters parameters)
        {
            _originalItem = parameters["original"] as MotorSchedule;

            if (_originalItem == null)
            {
                var _currentTeam = parameters["team"] as Team;
                SetScheduleForAddition(_currentTeam);
            }
            else
                SetScheduleForEdition(parameters["model"] as MotorSchedule);
        }

        /// <summary>
        /// Check if there are changes when the user press on back arrow
        /// </summary>
        /// <param name="parameters"></param>
        public async override void OnNavigatedFrom(INavigationParameters parameters)
        {
            if (!_originalItem.Equals(Schedule))
            {
                var answer = await _pageDialogService.DisplayActionSheetAsync("Save changes?", "No", "Yes");

                if (answer.Equals("Yes"))
                {
                    await _motorScheduleRepository.Save(Schedule);
                }
            }
        }
        private void SetScheduleForAddition(Team team)
        {
            _originalItem = new MotorSchedule() { Team = team };
            Schedule = _originalItem;
        }

        private void SetScheduleForEdition(MotorSchedule schedule)
        {
            // Save the the time value before setting the Schedule property because after that the value will lose.
            _time = schedule.Date.TimeOfDay;

            Schedule = schedule;
            Time = _time;
        }

        private async Task SaveAsync()
        {
            //TODO: Implement validations
            if (string.IsNullOrEmpty(Schedule.Competition))
            {
                await _pageDialogService.DisplayAlertAsync("Competition is empty", "The Competition cannot be empty", "Ok");
            }
            else
            {
                try
                {
                    await _motorScheduleRepository.Save(Schedule);

                    // Overwrite the original item by the new one in order to bypass the comparison in OnNavigatedFrom
                    _originalItem = Schedule;

                    var parameter = new NavigationParameters() { { "model", Schedule } };

                    // Cannot GoBack() because the TeamSelection page, reset the navigation stack instead.
                    await NavigationService.NavigateAsync("/NavigationPage/MainPage", parameter);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"### [ERROR] SaveAsync: {ex.Message}");
                    await _pageDialogService.DisplayAlertAsync("ERROR", "Error on Save Motor Schedule", "OK");
                }

            }

        }
    }
}
