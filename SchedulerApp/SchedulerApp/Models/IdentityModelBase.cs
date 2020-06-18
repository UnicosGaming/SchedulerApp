using System;
using System.Collections.Generic;
using System.Text;

namespace SchedulerApp.Models
{
    public class IdentityModelBase
    {
        public string Id { get; private set; }

        public IdentityModelBase(string id) => Id = id;
    }
}
