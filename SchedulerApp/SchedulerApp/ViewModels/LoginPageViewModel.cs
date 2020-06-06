using Prism.Commands;
using Prism.Ioc;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using SchedulerApp.Providers;
using SchedulerApp.Services.IdentityService;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Unity;
using Xamarin.Forms;

namespace SchedulerApp.ViewModels
{
    public class LoginPageViewModel : ViewModelBase
    {
        private readonly IIdentityService _identityService;
        private readonly IUnityContainer _unityContainer;
        private readonly INavigationService _navigationService;

        public DelegateCommand LoginCommand { get; private set; }

        public LoginPageViewModel(IIdentityService identityService, IUnityContainer unityContainer, INavigationService navigationService) : base(navigationService)
        {
            Debug.WriteLine("### Login ViewModel ###");

            LoginCommand = new DelegateCommand(async () => await Login(), () => true);

            MessagingCenter.Subscribe<IIdentityService>(this, "validation_ok", (_) => { _navigationService.NavigateAsync("/MainPage"); });
            MessagingCenter.Subscribe<IIdentityService>(this, "validation_error", (_) => { Debug.WriteLine("### Login error"); });

            _identityService = identityService;
            _unityContainer = unityContainer;
            _navigationService = navigationService;
        }

        private async Task Login()
        {
            Debug.WriteLine("### Login ###");
            if (_unityContainer.IsRegistered<IParentWindowProvider>())
            {
                Debug.WriteLine("### Is Registered ###");
                var parentPovider = _unityContainer.Resolve<IParentWindowProvider>();

                try
                {
                    await _identityService.LoginAsync(parentPovider.Parent);
                    //.ContinueWith((_) => _navigationService.NavigateAsync("MainPage"), TaskContinuationOptions.OnlyOnRanToCompletion)
                    //.Wait();
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("### Login Error ###");
                }

            }

            //await _identityService.LoginAsync(App.ParentWindow);
        }

    }
}
