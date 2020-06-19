using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using SchedulerApp.Models;
using SchedulerApp.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace SchedulerApp.ViewModels
{
    public class TeamSelectionPageViewModel : ViewModelBase
    {
        public DelegateCommand<Team> ItemTappedCommand { get; private set; }

        private User _curretUser;
        private readonly IPageDialogService _pageDialogService;

        public User CurrentUSer
        {
            get => _curretUser;
            set => SetProperty(ref _curretUser, value);
        }

        public TeamSelectionPageViewModel(INavigationService navigationService, IPageDialogService pageDialogService) : base(navigationService)
        {
            _pageDialogService = pageDialogService;

            ItemTappedCommand = new DelegateCommand<Team>((x) => NavigateToDetails(x));
            
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            var user = parameters["user"] as User;

            if (user != null)
            {
                CurrentUser = user;
            }
        }

        private async void NavigateToDetails(Team team)
        {
            try
            {
                var pageType = PageLocator.GetPage(team.Page.Name);

                await NavigationService.NavigateAsync($"{pageType}");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"### [ERROR] AddSchedule: {ex.Message}");
                await _pageDialogService.DisplayAlertAsync("Error on AddSchedule", "Cannot create a new schedule", "OK");
            }
        }
    }
}
