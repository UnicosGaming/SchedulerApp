using Microsoft.Identity.Client;
using SchedulerApp.Configuration;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SchedulerApp.Services.IdentityService
{
    public class IdentityService : IIdentityService
    {
        private IPublicClientApplication PCA = null;
        private string Username = string.Empty;
        private IEnumerable<IAccount> _accounts;

        public IdentityService()
        {

            PCA = PublicClientApplicationBuilder.Create(Secrets.ClientID)
                .WithRedirectUri($"msal{Secrets.ClientID}://auth")
                .WithIosKeychainSecurityGroup("com.microsoft.adalcache")
                .Build();

            LoadAccounts();
            
        }
        public async Task LoginAsync(object parentWindow)
        {
            Debug.WriteLine("### LogInAsync() ###");

            try
            {
                AuthenticationResult authResult = await PCA.AcquireTokenInteractive(Secrets.Scopes)
                    .WithParentActivityOrWindow(parentWindow)
                    .ExecuteAsync();

                ValidateAuth(authResult);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("### LogInAsync Exception ###");

                throw;
                
            }
        }
        public async Task LoginSilentAsync()
        {
            Debug.WriteLine("### LogInSilentAsync() ###");
            try
            {
                IAccount firstAccount = _accounts.FirstOrDefault();
                AuthenticationResult authResult = await PCA.AcquireTokenSilent(Secrets.Scopes, firstAccount).ExecuteAsync();

                ValidateAuth(authResult);
            }
            catch (MsalUiRequiredException msalEx)
            {
                Debug.WriteLine("### MsalUiRequiredException ###");

                // TODO: Maybe this message is not sent. Check SplashScreen page
                MessagingCenter.Send<IIdentityService>(this, "login_silent_error");
            }
        }
        public Task LogoutAsync()
        {
            Debug.WriteLine("### LogoutAsync ###");
            throw new NotImplementedException();
        }

        private async void LoadAccounts()
        {
            _accounts = await PCA.GetAccountsAsync();
        }

        private async Task ValidateAuth(AuthenticationResult authResult)
        {
            Debug.WriteLine("### ValidateAuth() ###");
            if (authResult != null)
            {
                try
                {
                    var content = await GetHttpContentWithTokenAsync(authResult.AccessToken);
                    Debug.WriteLine("### ValidateAuth - OK ###");
                    MessagingCenter.Send<IIdentityService>(this, "validation_ok");
                }
                catch (Exception ex)
                {
                    string message = string.Format($"API call to graph failed: {ex.Message}");
                    Debug.WriteLine("### ValidateAuth - ERROR ###");
                    MessagingCenter.Send<IIdentityService>(this, "validation_error");
                }
            }
        }

        private async Task<string> GetHttpContentWithTokenAsync(string token)
        {
            Debug.WriteLine("### GetHttpContentWithTokenAsync() ###");
            try
            {
                //get data from API
                HttpClient client = new HttpClient();
                // Request user info
                HttpRequestMessage message = new HttpRequestMessage(HttpMethod.Get, "https://graph.microsoft.com/v1.0/me");
                // Request user groups
                //HttpRequestMessage message = new HttpRequestMessage(HttpMethod.Get, "https://graph.microsoft.com/v1.0/me/memberOf");
                message.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                HttpResponseMessage response = await client.SendAsync(message);
                string responseString = await response.Content.ReadAsStringAsync();

                return responseString;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("### GetHttpContentWithTokenAsync Exception ###");
                throw;
            }
        }
    }
}
