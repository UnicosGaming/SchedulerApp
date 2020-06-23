using Microsoft.IdentityModel.Clients.ActiveDirectory;
using SchedulerApp.Configuration;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace SchedulerApp.Services.IdentityService
{
    public class DatabaseIdentityService : IDatabaseIdentityService
    {
        private string _aadInstance = "https://login.windows.net/{0}";
        private string _resourceId = "https://database.windows.net/";
        private AuthenticationResult _authenticationResult;

        public DatabaseIdentityService()
        {
            try
            {
                AuthenticationContext authenticationContext = new AuthenticationContext(string.Format(_aadInstance, Secrets.TenanId));
                ClientCredential clientCredential = new ClientCredential(Secrets.ClientID, Secrets.ClientSecret);
                _authenticationResult = authenticationContext.AcquireTokenAsync(_resourceId, clientCredential).Result;
            }
            catch (Exception)
            {
                Debug.Write("### [ERROR] DatabaseIdentityService ###");
                throw;
            }
            
        }

        public string GetDbAuthenticationToken()
        {
            return _authenticationResult.AccessToken;
        }
    }
}
