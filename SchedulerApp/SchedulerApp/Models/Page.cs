using System;
using System.Collections.Generic;
using System.Text;

namespace SchedulerApp.Models
{
    public class Page : IdentityModelBase
    {
        public Page(string id) : base(id)
        {
        }

        public string Name { get; set; }
    }
}
