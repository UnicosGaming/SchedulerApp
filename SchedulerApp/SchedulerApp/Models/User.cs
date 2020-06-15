using System;
using System.Collections.Generic;
using System.Text;

namespace SchedulerApp.Models
{
    public class User : ModelBase<User>
    {
        public User(string id) : base(id)
        {
        }

        public string Name { get; set; }
        public Group Group { get; set; }
    }
}
