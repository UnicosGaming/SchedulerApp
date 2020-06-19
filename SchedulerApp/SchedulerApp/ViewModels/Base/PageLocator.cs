using System;
using System.Collections.Generic;
using System.Text;

namespace SchedulerApp.ViewModels.Base
{
    public static class PageLocator
    {
        private static Dictionary<string, string> _pages => new Dictionary<string, string>
        {
            {"team", "TeamSchedulePage"},
            {"motor", "MotorSchedulePage"}
        };

        public static string GetPage(string type)
        {
            if (_pages.ContainsKey(type))
                return _pages[type];

            throw new Exception("Page type does not exist");
        }
    }
}
