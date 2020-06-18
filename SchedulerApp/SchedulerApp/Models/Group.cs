using System;
using System.Collections.Generic;
using System.Text;

namespace SchedulerApp.Models
{
    public class Group: IdentityModelBase
    {
        public Group(string id) : base(id)
        {
        }

        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsReadOnly { get; set; }

        public List<Team> Teams { get; set; }
    }
}
