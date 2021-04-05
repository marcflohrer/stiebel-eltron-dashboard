using Microsoft.Extensions.Configuration;
using StiebelEltronApiServer.Models;
using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace StiebelEltronApiServer.Services
{
    public class ServiceWeltFacade : IServiceWeltFacade
    {
        private string _serviceWeltBaseUrl;
        private string _serviceWeltUser;
        private string _serviceWeltPassword;


        public ServiceWeltFacade(IConfiguration configuration)
        {
            _serviceWeltBaseUrl = configuration["ServiceWeltUrl"] as string ?? throw new System.Exception("Missing configuration 'ServiceWeltUrl'");
            _serviceWeltUser = configuration["ServiceWeltUser"] as string ?? throw new System.Exception("Missing configuration 'ServiceWeltUser'");
            _serviceWeltPassword = configuration["ServiceWeltPassword"] as string ?? throw new System.Exception("Missing configuration 'ServiceWeltPassword'");
        }

        public async Task<ServiceWelt> GetHeatPumpWebsiteAsync(string sessionId)
        {
            var heatPumpUrl = BuildHeatPumpUrl(_serviceWeltBaseUrl);
            var httpResponseMessage = await GetUrl(_serviceWeltBaseUrl, heatPumpUrl, sessionId);
            var content = await httpResponseMessage.Content.ReadAsStringAsync();
            if (httpResponseMessage.Headers.TryGetValues("Cookie", out var sessId))
            {
                sessionId = sessId.FirstOrDefault()?.Split("=")[1] ?? "No sessionId found in " + sessId.FirstOrDefault();
            }
            return new ServiceWelt()
            {
                HtmlDocument = content,
                SessionId = sessionId
            };
        }


        private string BuildHeatPumpUrl(string baseUrl)
        {
            var slash = string.Empty;
            if (!baseUrl.EndsWith("/"))
            {
                slash = "/";
            }
            var heatPumpUrl = baseUrl + slash;
            heatPumpUrl += "?s=1,1";
            return heatPumpUrl;
        }

        private static HttpClient BuildHttpClient(string baseUrl, string sessionId)
        {
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.BaseAddress = new Uri(baseUrl);
            httpClient.DefaultRequestHeaders
                .Accept
                .Add(new MediaTypeWithQualityHeaderValue("text/html,application/xhtml+xml,application/xml;q=0.9,image/avif,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.9"));
            httpClient.DefaultRequestHeaders.Add("Content-Type", "application/x-www-form-urlencoded");
            httpClient.DefaultRequestHeaders.Add("Sec-GPC", "1");
            httpClient.DefaultRequestHeaders.Add("Referer", baseUrl);
            httpClient.DefaultRequestHeaders.Add("Accept-Language", "de-DE,de;q=0.9,en-US;q=0.8,en;q=0.7");
            httpClient.DefaultRequestHeaders.Add("Cookie", $"PHPSESSID={sessionId}");
            return httpClient;
        }

        private static async Task<HttpResponseMessage> PostUrl(string baseUrl, string fullUrl, string content, string sessionId)
        {
            var httpClient = BuildHttpClient(baseUrl, sessionId);
            var response = httpClient.PostAsync(fullUrl, new StringContent(content));
            return await response;
        }

        public async Task<string> LoginAsync()
        {
            var response = await PostUrl(_serviceWeltBaseUrl, _serviceWeltBaseUrl, $"make=send&user={_serviceWeltUser}&pass={_serviceWeltPassword}", new Guid().ToString());
            if (response.Headers.TryGetValues("PHPSESSID", out var phpSessionIds))
            {
                return phpSessionIds.FirstOrDefault();
            }
            throw new Exception($"Login failed for user {_serviceWeltUser}! ");
        }

        private static async Task<HttpResponseMessage> GetUrl(string baseUrl, string fullUrl, string sessionId)
        {
            var httpClient = BuildHttpClient(baseUrl, sessionId);
            var httpResponseMessage = httpClient.GetAsync(fullUrl);
            return await httpResponseMessage;
        }
    }
}
