using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
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
        private readonly IIdentityService _identityService;
        private readonly IPageDialogService _pageDialogService;

        public SplashPageViewModel(INavigationService navigationService, IIdentityService identityService, IPageDialogService pageDialogService) : base(navigationService)
        {
            _identityService = identityService;
            _pageDialogService = pageDialogService;
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            TryLogin();
        }

        private async void TryLogin()
        {
            try
            {
                IsTaskRunning = true;

                var user = await _identityService.LoginAsync();

                var param = new NavigationParameters() { { "user", user } };
                await NavigationService.NavigateAsync("NavigationPage/MainPage", param);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("### Login silent failed ###");
                await _pageDialogService.DisplayAlertAsync("Splash Error", $"{ex.Message}", "OK");
            }
            finally
            {
                IsTaskRunning = false;
            }
        }
    }
}
