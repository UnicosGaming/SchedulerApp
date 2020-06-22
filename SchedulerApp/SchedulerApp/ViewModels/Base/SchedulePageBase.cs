using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using SchedulerApp.Models;
using SchedulerApp.Repositories;
using System;
using System.Runtime.InteropServices.ComTypes;
using System.Threading.Tasks;

namespace SchedulerApp.ViewModels.Base
{
    public abstract class SchedulePageBase<T> : ViewModelBase where T : Schedule, new()
    {
        private readonly IPageDialogService _pageDialogService;
        protected readonly IWriteRepository<T> WriteRepository;
        protected readonly IReadRepository<T> ReadRepository;

        private T _originalItem;
        private bool _isEditing;

        public DelegateCommand SaveCommand { get; private set; }

        private T _schedule;
        public T Schedule
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

        public SchedulePageBase(INavigationService navigationService, 
            IPageDialogService pageDialogService, 
            IWriteRepository<T> writeRepository,
            IReadRepository<T> readRepository) : base(navigationService)
        {
            _pageDialogService = pageDialogService;
            WriteRepository = writeRepository;
            ReadRepository = readRepository;

            SaveCommand = new DelegateCommand(async () => await SaveAsync());
        }

        public override void Initialize(INavigationParameters parameters)
        {
            if (parameters.ContainsKey("schedule"))
            {
                var schedule = parameters["schedule"] as Schedule;
                SetScheduleForEditionAsync(schedule.Id);
            }
            else
            {
                var _team = parameters["team"] as Team;
                SetScheduleForAddition(_team);
            }
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
                    await SaveAsync();
                }
            }
        }

        protected virtual void SetScheduleForAddition(Team team)
        {
            _originalItem = new T() { Team = team };
            Schedule = _originalItem.Clone() as T;
        }

        protected async virtual void SetScheduleForEditionAsync(string id_schedule)
        {
            _isEditing = true;
            _originalItem = await ReadRepository.Get(id_schedule);
            Schedule = _originalItem.Clone() as T;
            Time = _originalItem.Date.TimeOfDay;
        }

        protected virtual async Task SaveAsync()
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
                    if (_isEditing)
                        await WriteRepository.Update(Schedule);
                    else
                        await WriteRepository.Insert(Schedule);

                    // Overwrite the original item by the new one in order to bypass the comparison in OnNavigatedFrom
                    _originalItem = Schedule;

                    var parameter = new NavigationParameters() { { "schedule", Schedule } };

                    // Cannot GoBack() because the TeamSelection page, reset the navigation stack instead.
                    await NavigationService.NavigateAsync("/NavigationPage/MainPage", parameter);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"### [ERROR] SaveAsync: {ex.Message}");
                    await _pageDialogService.DisplayAlertAsync("ERROR", "Error on Save Team Schedule", "OK");
                }

            }
        }
    }
}