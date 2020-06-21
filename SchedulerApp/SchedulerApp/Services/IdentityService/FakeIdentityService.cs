using SchedulerApp.Configuration;
using SchedulerApp.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SchedulerApp.Services.IdentityService
{
    public class FakeIdentityService : IIdentityService
    {
        public Task<User> Login()
        {
            return Task.FromResult(new User(Guid.NewGuid().ToString())
            {
                Name = "User A",
                Group = new Group("5B8853D2-5908-4048-9796-2F7797A91E05")
                {
                    Name = "Admin",
                    Description = "Admin group"
                }
            });
        }

        public Task LogoutAsync() {
            Session.SetUser(null);
            return Task.FromResult(true); 
        }
    }
}
