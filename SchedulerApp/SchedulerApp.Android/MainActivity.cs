using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Microsoft.Identity.Client;
using Prism;
using Prism.Ioc;
using SchedulerApp.Providers;

namespace SchedulerApp.Droid
{
    [Activity(Label = "SchedulerApp", Icon = "@mipmap/ic_launcher", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);

            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            global::Xamarin.Forms.FormsMaterial.Init(this, savedInstanceState);
            LoadApplication(new App(new AndroidInitializer(this)));

            //App.ParentWindow = this;
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);

            AuthenticationContinuationHelper.SetAuthenticationContinuationEventArgs(requestCode, resultCode, data);
        }
    }

    public class AndroidInitializer : IPlatformInitializer, IParentWindowProvider
    {
        object _parentWindow;
        object IParentWindowProvider.Parent => _parentWindow;

        public AndroidInitializer(object parent)
        {
            _parentWindow = parent;
        }
        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            System.Diagnostics.Debug.WriteLine("### RegisterType Android ###");
            // Register any platform specific implementations
            containerRegistry.RegisterInstance<IParentWindowProvider>(this);
        }
    }
}

