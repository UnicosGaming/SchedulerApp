using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using SchedulerApp.Models;
using SchedulerApp.Services.IdentityService;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SchedulerApp.ViewModels
{
    public class SplashPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private readonly IIdentityService _identityService;

        private bool _isTaskRunning;
        public bool IsTaskRunning
        {
            get { return _isTaskRunning; }
            set { SetProperty(ref _isTaskRunning, value); }
        }

        public SplashPageViewModel(INavigationService navigationService, IIdentityService identityService):base(navigationService)
        {
            _navigationService = navigationService;
            _identityService = identityService;

            MessagingCenter.Subscribe<IIdentityService, User>(this, "validation_ok", (_, user) => { ValidationOk(user); });
            MessagingCenter.Subscribe<IIdentityService>(this, "login_silent_error", (_) => { ValidationError(); });

            TryValidate();
        }

        private void TryValidate()
        {
            try
            {
                IsTaskRunning = true;
                _identityService.LoginSilentAsync();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("### Login silent failed ###");
            }
        }

        private async void ValidationOk(User userInfo)
        {
            IsTaskRunning = false;
            var param = new NavigationParameters() { { "user", userInfo } };
            await _navigationService.NavigateAsync("/MainPage", param);
        }

        private async void ValidationError()
        {
            IsTaskRunning = false;
            await _navigationService.NavigateAsync("/LoginPage");
        }


    }
}
