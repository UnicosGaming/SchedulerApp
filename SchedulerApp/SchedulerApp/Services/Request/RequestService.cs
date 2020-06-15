using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SchedulerApp.Services.Request
{
    public class RequestService : IRequestService
    {
        private readonly HttpClient _httpClient;

        public RequestService()
        {
            _httpClient = new HttpClient();
        }
        public async Task<HttpResponseMessage> SendRequestMessageAsync(HttpRequestMessage message)
        {
            return await _httpClient.SendAsync(message);
        }
    }
}
