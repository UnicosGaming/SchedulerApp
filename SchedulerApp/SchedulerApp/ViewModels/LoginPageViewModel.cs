using Prism.Commands;
using Prism.Ioc;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using SchedulerApp.Models;
using SchedulerApp.Providers;
using SchedulerApp.Services.IdentityService;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SchedulerApp.ViewModels
{
    public class LoginPageViewModel : ViewModelBase
    {
        private readonly IIdentityService _identityService;
        private readonly IPageDialogService _pageDialogService;

        public DelegateCommand LoginCommand { get; private set; }

        public LoginPageViewModel(IIdentityService identityService, INavigationService navigationService, IPageDialogService pageDialogService) : base(navigationService)
        {
            _identityService = identityService;
            _pageDialogService = pageDialogService;

            LoginCommand = new DelegateCommand(async () => await Login(), () => true);

        }

        private async Task Login()
        {
            try
            {
                IsTaskRunning = true;

                var user = await _identityService.Login();

                var param = new NavigationParameters() { { "user", user } };
                await NavigationService.NavigateAsync("NavigationPage/MainPage", param);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("### Login Error ###");
                await _pageDialogService.DisplayAlertAsync("Login Error", $"{ex.Message}", "OK");
            }
            finally
            {
                IsTaskRunning = false;
            }
        }
    }
}
