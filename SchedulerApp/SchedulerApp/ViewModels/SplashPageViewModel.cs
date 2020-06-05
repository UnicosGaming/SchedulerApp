using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using SchedulerApp.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SchedulerApp.ViewModels
{
    public class SplashPageViewModel : BindableBase
    {
        private readonly INavigationService _navigationService;
        private readonly IIdentityService _identityService;

        public SplashPageViewModel(INavigationService navigationService, IIdentityService identityService)
        {
            _navigationService = navigationService;
            _identityService = identityService;

            MessagingCenter.Subscribe<IIdentityService>(this, "validation_ok", (_) => { _navigationService.NavigateAsync("/MainPage"); });
            MessagingCenter.Subscribe<IIdentityService>(this, "login_silent_error", (_) => { _navigationService.NavigateAsync("/LoginPage"); });
            try
            {
                _identityService.LoginSilentAsync();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("### Login silent failed ###");
            }


        }


    }
}
