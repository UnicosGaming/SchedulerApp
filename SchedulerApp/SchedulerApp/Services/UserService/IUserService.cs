using SchedulerApp.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SchedulerApp.Services.UserService
{
    public interface IUserService
    {
        Task<User> GetUserInfoAsync(string token);
    }
}
