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

        public async Task<User> GetUserInfoAsync(string token)
        {
            var user = await RequestUserInfoAsync(token);

            var group = await RequestGroupAsync(token);

            user.Group = group;

            return user;
        }

        private async Task<User> RequestUserInfoAsync(string token)
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

        private async Task<Group> RequestGroupAsync(string token)
        {
            HttpRequestMessage message = new HttpRequestMessage(HttpMethod.Get, "https://graph.microsoft.com/v1.0/me/memberOf");
            message.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            HttpResponseMessage response = await _requestService.SendRequestMessageAsync(message);
            string responseString = await response.Content.ReadAsStringAsync();

            var groupObj = JObject.Parse(responseString);

            if (groupObj.ContainsKey("code"))
                throw new Exception((string)groupObj["code"]);

            return new Group((string)groupObj["value"][1]["id"])
            {
                Name = (string)groupObj["value"][1]["displayName"],
                Description = (string)groupObj["value"][1]["description"]
            };
        }
    }
}
