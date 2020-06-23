using System;
using System.Collections.Generic;
using System.Text;

namespace SchedulerApp.Services.IdentityService
{
    public interface IDatabaseIdentityService
    {
        string GetDbAuthenticationToken();
    }
}
