using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
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
        public User CurrentUSer
        {
            get => _curretUser;
            set => SetProperty(ref _curretUser, value);
        }

        public TeamSelectionPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            ItemTappedCommand = new DelegateCommand<Team>((x) => Debug.WriteLine(x.Name));
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            var user = parameters["user"] as User;

            if (user != null)
            {
                CurrentUser = user;
            }
        }
    }
}
