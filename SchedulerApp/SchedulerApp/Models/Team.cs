using System;
using System.Collections.Generic;
using System.Text;

namespace SchedulerApp.Models
{
    public class Team : IdentityModelBase
    {
        public Team(string id) : base(id)
        {
        }
        
        public string Name { get; set; }
        public Page Page { get; set; }
    }
}
