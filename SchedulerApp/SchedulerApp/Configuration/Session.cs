using SchedulerApp.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace SchedulerApp.Configuration
{
    public static class Session
    {
        private static User _currentUser;
        public static User CurrentUser => _currentUser;

        public static void SetUser(User user) => _currentUser = user;

    }
}
