using Microsoft.Identity.Client;
using Newtonsoft.Json.Linq;
using SchedulerApp.Models;
using SchedulerApp.Services.Request;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SchedulerApp.Services.UserService
{
    public class UserService: IUserService
    {
        private readonly IRequestService _requestService;

        public UserService(IRequestService requestService)
        {
            _requestService = requestService;
        }

        public async Task<User> GetUserInfo(string token)
        {
            var message = new HttpRequestMessage(HttpMethod.Get, "https://graph.microsoft.com/v1.0/me");
            message.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            HttpResponseMessage response = await _requestService.SendRequestMessageAsync(message);
            string responseString = await response.Content.ReadAsStringAsync();

            var userObj = JObject.Parse(responseString);

            if (userObj.ContainsKey("code"))
                throw new Exception((string)userObj["code"]);

            return new User((string)userObj["id"])
            {
                Name = (string)userObj["displayName"]
            };

        }
    }
}
