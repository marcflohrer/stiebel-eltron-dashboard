using Microsoft.Extensions.Configuration;
using StiebelEltronApiServer.Models;
using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace StiebelEltronApiServer.Services.HtmlServices
{
    public class ServiceWeltFacade : IServiceWeltFacade
    {
        private string _serviceWeltBaseUrl;
        private string _serviceWeltUser;
        private string _serviceWeltPassword;


        public ServiceWeltFacade(IConfiguration configuration)
        {
            _serviceWeltBaseUrl = configuration["ServiceWeltUrl"] as string ?? throw new Exception("Missing configuration 'ServiceWeltUrl'");
            _serviceWeltUser = configuration["ServiceWeltUser"] as string ?? throw new Exception("Missing configuration 'ServiceWeltUser'");
            _serviceWeltPassword = configuration["ServiceWeltPassword"] as string ?? throw new Exception("Missing configuration 'ServiceWeltPassword'");
        }

        public async Task<ServiceWelt> GetHeatPumpWebsiteAsync(string sessionId)
        {
            var body = $"make=send&user={_serviceWeltUser}&pass={_serviceWeltPassword}";
            var heatPumpUrl = BuildHeatPumpUrl(_serviceWeltBaseUrl);
            var httpResponseMessage = await PostUrl(_serviceWeltBaseUrl, heatPumpUrl, body, new Guid().ToString()); ;
            var content = await httpResponseMessage.Content.ReadAsStringAsync();
            return new ServiceWelt()
            {
                HtmlDocument = content
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

        private static HttpClient BuildHttpClient(string baseUrl, string sessionId, HttpClientHandler handler)
        {
            var httpClient = new HttpClient(handler);
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.BaseAddress = new Uri(baseUrl);
            httpClient.DefaultRequestHeaders.Add("Cookie", $"PHPSESSID={sessionId}");
            httpClient.DefaultRequestHeaders.Add("accept-language", "de-DE");
            httpClient.DefaultRequestHeaders.Add("accept", "application/xhtml+xml");
            httpClient.DefaultRequestHeaders.Add("accept-encoding", "gzip, deflate");
            httpClient.DefaultRequestHeaders.Add("Referer", baseUrl);
            return httpClient;
        }

        private static async Task<HttpResponseMessage> PostUrl(string baseUrl, string fullUrl, string content, string sessionId)
        {
            CookieContainer cookieContainer = new CookieContainer();
            using HttpClientHandler handler = new HttpClientHandler
            {
                UseDefaultCredentials = true,
                AllowAutoRedirect = true,
                UseCookies = true,
                CookieContainer = cookieContainer
            };
            using var httpClient = BuildHttpClient(baseUrl, sessionId, handler);
            var response = await httpClient.PostAsync(fullUrl, new StringContent(content, Encoding.ASCII, "application/x-www-form-urlencoded"));
            response.EnsureSuccessStatusCode();
            return response;
        }

        private static async Task<HttpResponseMessage> GetUrl(string baseUrl, string fullUrl, string sessionId)
        {
            using var httpClientHandler = new HttpClientHandler
            {
                UseCookies = true
            };
            using var httpClient = BuildHttpClient(baseUrl, sessionId, httpClientHandler);
            var httpResponseMessage = httpClient.GetAsync(fullUrl);
            return await httpResponseMessage;
        }
    }
}
