using Prism;
using Prism.Ioc;
using SchedulerApp.ViewModels;
using SchedulerApp.Views;
using Xamarin.Essentials.Interfaces;
using Xamarin.Essentials.Implementation;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SchedulerApp.Services.IdentityService;
using Newtonsoft.Json;
using SchedulerApp.Configuration;
using System.IO;
using SchedulerApp.Services.DataService;
using Prism.Unity;
using SchedulerApp.Services.Request;
using SchedulerApp.Services.UserService;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
[assembly: ExportFont("UnicaOne-Regular.ttf")]
[assembly: ExportFont("Roboto-Regular.ttf")]
[assembly: ExportFont("Audiowide-Regular.ttf")]
namespace SchedulerApp
{
    public partial class App
    {
        //public static object ParentWindow { get; set; }

        /* 
         * The Xamarin Forms XAML Previewer in Visual Studio uses System.Activator.CreateInstance.
         * This imposes a limitation in which the App class must have a default constructor. 
         * App(IPlatformInitializer initializer = null) cannot be handled by the Activator.
         */
        public App() : this(null) { }

        public App(IPlatformInitializer initializer) : base(initializer) { }

        protected override async void OnInitialized()
        {
            InitializeComponent();

            //await NavigationService.NavigateAsync("/SplashPage");
            await NavigationService.NavigateAsync("NavigationPage/MainPage");
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<IAppInfo, AppInfoImplementation>();
            containerRegistry.RegisterSingleton<IIdentityService, IdentityService>();
            containerRegistry.RegisterSingleton<IDataService, FakeDataService>();
            containerRegistry.RegisterSingleton<IRequestService, RequestService>();
            containerRegistry.Register<IUserService, UserService>(); // Transient registration

            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<MainPage, MainPageViewModel>();
            containerRegistry.RegisterForNavigation<SplashPage, SplashPageViewModel>();
            containerRegistry.RegisterForNavigation<LoginPage, LoginPageViewModel>();
            containerRegistry.RegisterForNavigation<SchedulePage, SchedulePageViewModel>();
        }
    }
}
