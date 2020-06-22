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
using SchedulerApp.Services.Request;
using SchedulerApp.Services.UserService;
using SchedulerApp.Repositories;
using Unity;
using SchedulerApp.Models;

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

            await NavigationService.NavigateAsync("/LoginPage");
            //await NavigationService.NavigateAsync("NavigationPage/MainPage");
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<IAppInfo, AppInfoImplementation>();

            RegisterRepositories(containerRegistry);

            RegisterServices(containerRegistry);

            RegisterNavigationPages(containerRegistry);
        }

        private static void RegisterNavigationPages(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<MainPage, MainPageViewModel>();
            containerRegistry.RegisterForNavigation<LoginPage, LoginPageViewModel>();
            containerRegistry.RegisterForNavigation<TeamSchedulePage, TeamSchedulePageViewModel>();
            containerRegistry.RegisterForNavigation<TeamSelectionPage, TeamSelectionPageViewModel>();
            containerRegistry.RegisterForNavigation<MotorSchedulePage, MotorSchedulePageViewModel>();
        }

        private static void RegisterServices(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<IIdentityService, FakeIdentityService>();
            //containerRegistry.RegisterSingleton<IIdentityService, IdentityService>();
            containerRegistry.RegisterSingleton<IDataService, FakeDataService>();
            containerRegistry.RegisterSingleton<ISqlDataService, SqlDataService>();
            containerRegistry.RegisterSingleton<IRequestService, RequestService>();
            containerRegistry.Register<IUserService, UserService>(); // Transient registration
        }

        private static void RegisterRepositories(IContainerRegistry containerRegistry)
        {
            containerRegistry.Register<IGroupRepository, GroupRepository>();
            containerRegistry.Register<ITeamRepository, TeamRepository>();
            containerRegistry.Register<IPageRepository, PageRepository>();
            containerRegistry.Register(typeof(IWriteRepository<TeamSchedule>), typeof(TeamScheduleRepository));
            containerRegistry.Register(typeof(IWriteRepository<MotorSchedule>), typeof(MotorScheduleRepository));
            containerRegistry.Register(typeof(IReadRepository<Schedule>), typeof(ScheduleRepository));
            containerRegistry.Register(typeof(IReadRepository<TeamSchedule>), typeof(TeamScheduleRepository));
            containerRegistry.Register(typeof(IReadRepository<MotorSchedule>), typeof(MotorScheduleRepository));
        }
    }
}
