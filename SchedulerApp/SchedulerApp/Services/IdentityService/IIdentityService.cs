﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SchedulerApp.Services.IdentityService
{
    public interface IIdentityService
    {
        Task LoginSilentAsync();
        Task LoginAsync(object parentWindow);
        Task LogoutAsync();
    }
}