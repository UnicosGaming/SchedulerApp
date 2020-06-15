using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using SchedulerApp.Models;
using SchedulerApp.Services.DataService;
using SchedulerApp.Services.IdentityService;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Input;
using System.Xml.Schema;

namespace SchedulerApp.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private readonly IPageDialogService _pageDialogService;
        private readonly IDataService _dataService;
        private readonly IIdentityService _identityService;
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

        private User _currentUser;
        public User CurrentUser
        {
            get => _currentUser;
            set => SetProperty(ref _currentUser, value);
        }

        ObservableCollection<Schedule> _items;
        public ObservableCollection<Schedule> Items
        {
            get => _items;
            set => SetProperty(ref _items, value);
        }

        public MainPageViewModel(INavigationService navigationService, IPageDialogService pageDialogService, IDataService dataService, IIdentityService identityService) : base(navigationService)
        {
            _navigationService = navigationService;
            _pageDialogService = pageDialogService;
            _dataService = dataService;
            _identityService = identityService;
            ItemTappedCommand = new DelegateCommand<Schedule>((x) => EditSchedule(x));
            AddCommand = new DelegateCommand(() => NavigationService.NavigateAsync("SchedulePage"));
            DeleteCommand = new DelegateCommand<Schedule>((x) => DeleteSchedule(x));
            LogoutCommand = new DelegateCommand(() => Logout());
        }

        /// <summary>
        /// Initialize the list with the schedules
        /// </summary>
        /// <param name="parameters"></param>
        public override async void Initialize(INavigationParameters parameters)
        {
            // TODO: Refactor
            try
            {
                IsTaskRunning = true;

                // User Info
                var user = parameters["user"] as User;
                if (user != null)
                {
                    _currentUser = user;
                    HeaderText = $"Welcome {user.Name} [{user.Group.Name}]";

                }

                // Load schedules
                var schedules = await _dataService.Get();
                Items = new ObservableCollection<Schedule>(schedules.OrderBy(x => x.Date));
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"### [MainPageViewModel - Initialize] Error: {ex.Message}");
            }
            finally
            {
                IsTaskRunning = false;
            }
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            var schedule = parameters["model"] as Schedule;

            if (schedule != null)
                Items.Add(schedule);
        }

        private async void EditSchedule(Schedule schedule)
        {
            _originalItem = schedule.Clone();

            if (schedule != null)
            {
                var parameters = new NavigationParameters() {
                    { "model", schedule },
                    {"original", _originalItem }
                };
                await NavigationService.NavigateAsync("SchedulePage", parameters);
            }
        }

        private async void DeleteSchedule(Schedule schedule)
        {
            var answer = await _pageDialogService.DisplayActionSheetAsync($"Delete {schedule.Competition}", "No", "Yes");

            if (answer.Equals("Yes"))
            {
                _dataService.Delete(schedule.Id);
                Items.Remove(schedule);
            }
        }

        private async void Logout()
        {
            var answer = await _pageDialogService.DisplayActionSheetAsync("Do you want to logout?", "No", "Yes");

            if (answer.Equals("Yes"))
            {
                await _identityService.LogoutAsync();

                await _navigationService.NavigateAsync("/LoginPage");
            }

        }
    }
}
