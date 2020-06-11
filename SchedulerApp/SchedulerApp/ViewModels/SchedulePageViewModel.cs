using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using SchedulerApp.Models;
using SchedulerApp.Services.DataService;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace SchedulerApp.ViewModels
{
    public class SchedulePageViewModel : ViewModelBase
    {
        private Schedule _originalItem;

        public DelegateCommand SaveCommand { get; private set; }

        private Schedule _schedule;
        public Schedule Schedule
        {
            get => _schedule;
            set => SetProperty(ref _schedule, value);
        }

        private TimeSpan _time;
        private readonly IPageDialogService _pageDialogService;
        private readonly IDataService _dataService;

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

        public SchedulePageViewModel(INavigationService navigationService, IPageDialogService pageDialogService, IDataService dataService) : base(navigationService)
        {
            _pageDialogService = pageDialogService;
            _dataService = dataService;

            SaveCommand = new DelegateCommand(async () => await SaveAsync());
        }

        public override void Initialize(INavigationParameters parameters)
        {
            _originalItem = parameters["original"] as Schedule;

            // Save the the time value before setting the Schedule property because after that the value will lose.
            _time = (parameters["model"] as Schedule).Date.TimeOfDay;

            Schedule = parameters["model"] as Schedule;
            Time = _time;
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
                    await _dataService.Save(Schedule);
                }
            }
        }

        private async Task SaveAsync()
        {
            // Save data
            Debug.WriteLine("Saving...");
            await _dataService.Save(Schedule);

            // Overwrite the original item by the new one in order to bypass the comparison in OnNavigatedFrom
            _originalItem = Schedule; 

            await NavigationService.GoBackAsync();
        }
    }
}
