using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.Contracts;
using System.Text;

namespace SchedulerApp.Configuration
{
    // This class is ignored by git.

    public static class Secrets
    {
        public static string ClientID => "#ClientID#";
        public const string MsalID = "#MsalID#";
        public static readonly string[] Scopes = { "User.Read" };
        public static string ConnectionString => "#ConnectionString#";
        public static string DatabaseServerName => "#DatabaseServerName#";
        public static string DatabaseName => "#DatabaseName#";
        public static string TenanId => "#TenantID#";
        public static string ClientSecret => "#ClientSecret#";
    }
}
