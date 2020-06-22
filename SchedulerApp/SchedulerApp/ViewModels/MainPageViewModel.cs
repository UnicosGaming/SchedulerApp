using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using SchedulerApp.Configuration;
using SchedulerApp.Models;
using SchedulerApp.Repositories;
using SchedulerApp.Services.DataService;
using SchedulerApp.Services.IdentityService;
using SchedulerApp.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Xml.Schema;

namespace SchedulerApp.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        private readonly IPageDialogService _pageDialogService;
        private readonly IDataService _dataService;
        private readonly IIdentityService _identityService;
        private readonly IReadRepository<Schedule> _scheduleRepository;
        private readonly IDeleteRepository<TeamSchedule> _teamDeleteRepository;
        private readonly IDeleteRepository<MotorSchedule> _motorDeleteRepository;
        private Schedule _originalItem;


        public DelegateCommand<Schedule> ItemTappedCommand { get; private set; }
        public DelegateCommand AddCommand { get; private set; }
        public DelegateCommand<Schedule> DeleteCommand { get; private set; }
        public DelegateCommand LogoutCommand { get; private set; }

        private string _headerText;
        public string HeaderText
        {
            get => _headerText;
            set => SetProperty(ref _headerText, value);
        }

        private bool _isReadOnly;
        public bool IsReadOnly
        {
            get => _isReadOnly;
            set => SetProperty(ref _isReadOnly, value);
        }

        ObservableCollection<Schedule> _items;
        public ObservableCollection<Schedule> Items
        {
            get => _items;
            set => SetProperty(ref _items, value);
        }

        public MainPageViewModel(INavigationService navigationService,
            IPageDialogService pageDialogService,
            IDataService dataService,
            IIdentityService identityService,
            IReadRepository<Schedule> scheduleRepository,
            IDeleteRepository<TeamSchedule> teamDeleteRepository,
            IDeleteRepository<MotorSchedule> motorDeleteRepository) : base(navigationService)
        {
            _pageDialogService = pageDialogService;
            _dataService = dataService;
            _identityService = identityService;
            _scheduleRepository = scheduleRepository;
            _teamDeleteRepository = teamDeleteRepository;
            _motorDeleteRepository = motorDeleteRepository;

            ItemTappedCommand = new DelegateCommand<Schedule>((x) => EditSchedule(x));
            AddCommand = new DelegateCommand(() => AddSchedule());
            DeleteCommand = new DelegateCommand<Schedule>((x) => DeleteSchedule(x));
            LogoutCommand = new DelegateCommand(() => Logout());
        }

        /// <summary>
        /// Initialize the list with the schedules
        /// </summary>
        /// <param name="parameters"></param>
        public async override void Initialize(INavigationParameters parameters)
        {
            try
            {
                IsTaskRunning = true;

                // User Info
                CurrentUser = Session.CurrentUser;
                HeaderText = $"{CurrentUser.Name} [{CurrentUser.Group.Name}]";
                IsReadOnly = CurrentUser.Group.IsReadOnly;

                await LoadSchedulesAsync();

                var schedule = parameters["schedule"] as Schedule;

                if (schedule != null)
                {
                    if (!Items.Any(x => x.Id.Equals(schedule.Id)))
                    {
                        Items.Add(schedule);
                    }
                }
            }
            catch (Exception ex)
            {
                await _pageDialogService.DisplayAlertAsync("ERROR", "Error on load schedules", "OK");
                Debug.WriteLine($"### [MainPageViewModel - Initialize] Error: {ex.Message}");
            }
            finally
            {
                IsTaskRunning = false;
            }
        }

        private async Task LoadSchedulesAsync()
        {
            try
            {
                var schedules = await _scheduleRepository.GetAll(CurrentUser.Group.Id);
                Items = new ObservableCollection<Schedule>(schedules.OrderBy(x => x.Date));
            }
            catch (Exception)
            {
                throw;
            }
        }

        private async void AddSchedule()
        {
            if (CurrentUser.Group.Teams.Count > 1)
            {
                await NavigationService.NavigateAsync("TeamSelectionPage");
            }
            else
            {
                try
                {
                    // The group only manages one team
                    var team = CurrentUser.Group.Teams.First();
                    var pageType = PageLocator.GetPage(team.Page.Name);

                    var parameter = new NavigationParameters() { { "team", team } };

                    await NavigationService.NavigateAsync($"{pageType}", parameter);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"### [ERROR] AddSchedule: {ex.Message}");
                    await _pageDialogService.DisplayAlertAsync("Error on AddSchedule", "Cannot create a new schedule", "OK");
                }
            }
        }

        private async void EditSchedule(Schedule schedule)
        {
            if ((!IsReadOnly) && (schedule != null))
            {
                var pageType = PageLocator.GetPage(schedule.Team.Page.Name);

                var parameter = new NavigationParameters() { { "schedule", schedule } };

                await NavigationService.NavigateAsync($"{pageType}", parameter);
            }
        }
        private async void DeleteSchedule(Schedule schedule)
        {

            var answer = await _pageDialogService.DisplayActionSheetAsync($"Delete {schedule.Competition}", "No", "Yes");

            if (answer.Equals("Yes"))
            {
                // TODO: Refactor this $h17
                if (schedule.Team.Page.Name.Equals("team"))
                    _teamDeleteRepository.Delete(schedule.Id);
                else
                    _motorDeleteRepository.Delete(schedule.Id);

                Items.Remove(schedule);
            }
        }

        private async void Logout()
        {
            var answer = await _pageDialogService.DisplayActionSheetAsync("Do you want to logout?", "No", "Yes");

            if (answer.Equals("Yes"))
            {
                await _identityService.LogoutAsync();

                var parameter = new NavigationParameters() { { "logout", true } };
                await NavigationService.NavigateAsync("/LoginPage", parameter);
            }

        }
    }
}
