using Microsoft.Identity.Client;
using Newtonsoft.Json.Linq;
using SchedulerApp.Configuration;
using SchedulerApp.Models;
using SchedulerApp.Providers;
using SchedulerApp.Services.UserService;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Unity;
using Xamarin.Forms;

namespace SchedulerApp.Services.IdentityService
{
    public class IdentityService : IIdentityService
    {
        private IPublicClientApplication PCA = null;
        private IEnumerable<IAccount> _accounts;
        private readonly IUserService _userService;
        private readonly IUnityContainer _unityContainer;

        public IdentityService(IUserService userService, IUnityContainer unityContainer)
        {
            _userService = userService;
            _unityContainer = unityContainer;

            PCA = PublicClientApplicationBuilder.Create(Secrets.ClientID)
                .WithRedirectUri($"msal{Secrets.ClientID}://auth")
                .WithIosKeychainSecurityGroup("com.microsoft.adalcache")
                .Build();

            LoadAccounts();

        }
        /// <summary>
        /// Try a silent validation, if it's fails with a MsalUiRequiredException exception
        /// then try with an interactive validation
        /// </summary>
        /// <returns>User info</returns>
        public async Task<User> LoginAsync()
        {
            try
            {
                return await TryLoginSilentAsync();
            }
            catch (MsalUiRequiredException)
            {
                if (_unityContainer.IsRegistered<IParentWindowProvider>())
                {
                    Debug.WriteLine("### Is Registered ###");
                    var parentPovider = _unityContainer.Resolve<IParentWindowProvider>();

                    return await TryLoginInteractiveAsync(parentPovider.Parent);
                }
                else
                {
                    throw new Exception("Cannot get ParentWindowProvider");
                }

            }
            catch (Exception ex)
            {
                throw;
            }
        }
        private async Task<User> TryLoginInteractiveAsync(object parentWindow)
        {
            Debug.WriteLine("### LogInAsync() ###");

            try
            {
                AuthenticationResult authResult = await PCA.AcquireTokenInteractive(Secrets.Scopes)
                    .WithParentActivityOrWindow(parentWindow)
                    .ExecuteAsync();

                return await ValidateAuth(authResult);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("### LogInAsync Exception ###");
                throw;
            }
        }
        private async Task<User> TryLoginSilentAsync()
        {
            Debug.WriteLine("### LogInSilentAsync() ###");
            try
            {
                IAccount firstAccount = _accounts.FirstOrDefault();
                AuthenticationResult authResult = await PCA.AcquireTokenSilent(Secrets.Scopes, firstAccount).ExecuteAsync();

                return await ValidateAuth(authResult);
            }
            catch (MsalUiRequiredException)
            {
                Debug.WriteLine("### MsalUiRequiredException ###");
                throw;
            }
            catch (Exception) { throw; }
        }
        public async Task LogoutAsync()
        {
            Debug.WriteLine("### LogoutAsync ###");
            while (_accounts.Any())
            {
                await PCA.RemoveAsync(_accounts.FirstOrDefault());
                _accounts = await PCA.GetAccountsAsync();
            }
        }

        private async void LoadAccounts()
        {
            _accounts = await PCA.GetAccountsAsync();
        }

        private async Task<User> ValidateAuth(AuthenticationResult authResult)
        {
            Debug.WriteLine("### ValidateAuth() ###");
            try
            {
                if (authResult != null)
                {
                    var userInfo = await _userService.GetUserInfoAsync(authResult.AccessToken);
                    
                    Debug.WriteLine("### ValidateAuth - OK ###");
                    
                    return userInfo;
                }
                else
                {
                    throw new Exception("Error on ValidateAuth");
                }
            }
            catch (Exception ex)
            {
                string message = string.Format($"Validation error: {ex.Message}");
                Debug.WriteLine($"### ValidateAuth - ERROR: {message} ###");

                throw;
            }
        }
    }
}
