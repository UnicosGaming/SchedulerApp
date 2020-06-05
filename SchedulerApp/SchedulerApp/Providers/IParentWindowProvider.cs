using System;
using System.Collections.Generic;
using System.Text;

namespace SchedulerApp.Providers
{
    public interface IParentWindowProvider
    {
        object Parent { get; }
    }
}
