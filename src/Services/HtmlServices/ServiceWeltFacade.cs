using Microsoft.Extensions.Configuration;
using Serilog;
using StiebelEltronDashboard.Models;
using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace StiebelEltronDashboard.Services.HtmlServices
{
    public class ServiceWeltFacade : IServiceWeltFacade
    {
        private string _serviceWeltUser;
        private string _serviceWeltPassword;
        public static string ServiceWeltPostClientName => "ServiceWeltPostClient";
        public static string ServiceWeltGetClientName => "ServiceWeltGetClient";

        public IHttpClientFactory HttpClientFactory { get; }

        public ServiceWeltFacade(IConfiguration configuration, IHttpClientFactory httpClientFactory)
        {
            _serviceWeltUser = configuration["ServiceWeltUser"] as string ?? throw new Exception("Missing configuration 'ServiceWeltUser'");
            _serviceWeltPassword = configuration["ServiceWeltPassword"] as string ?? throw new Exception("Missing configuration 'ServiceWeltPassword'");
            HttpClientFactory = httpClientFactory;
        }

        public async Task<ServiceWelt> GetHeatPumpWebsiteAsync(string sessionId)
        {
            var httpContent = $"make=send&user={_serviceWeltUser}&pass={_serviceWeltPassword}";
            Log.Debug($"GetHeatPumpWebsiteAsync {httpContent}");
            var httpResponseMessage = await PostUrl(httpContent, new Guid().ToString());
            var content = await httpResponseMessage.Content.ReadAsStringAsync();
            Log.Debug($"GetHeatPumpWebsiteAsync content {content}");
            return new ServiceWelt()
            {
                HtmlDocument = content
            };
        }

        private HttpClient CreatePostHttpClient(string sessionId)
        {
            Log.Debug($"-->CreatePostHttpClient");
            var httpClient = AddSessionCookie(HttpClientFactory.CreateClient(ServiceWeltPostClientName), sessionId);
            if (httpClient == null)
            {
                Log.Error($"<--CreatePostHttpClient: httpclient is null.");
            }
            Log.Debug($"<--CreatePostHttpClient");
            return httpClient;
        }

        private HttpClient BuildGetHttpClient(string sessionId)
            => AddSessionCookie(HttpClientFactory.CreateClient(ServiceWeltGetClientName), sessionId);

        private static HttpClient AddSessionCookie(HttpClient httpClient, string sessionId)
        {
            httpClient.DefaultRequestHeaders.Add("Cookie", $"PHPSESSID={sessionId}");
            return httpClient;
        }

        private async Task<HttpResponseMessage> PostUrl(string httpContent, string sessionId)
        {
            using var httpClient = CreatePostHttpClient(sessionId);
            var fullUrl = BuildHeatPumpUrlWithParameters(httpClient?.BaseAddress?.ToString());
            var response = await httpClient.PostAsync(fullUrl, new StringContent(httpContent, Encoding.ASCII, "application/x-www-form-urlencoded"));
            Log.Debug($"PostUrl: ISG response status is {response.StatusCode}");
            response.EnsureSuccessStatusCode();
            return response;
        }

        private async Task<HttpResponseMessage> GetUrl(string sessionId)
        {
            using var httpClient = BuildGetHttpClient(sessionId);
            Log.Debug($"<--GetUrl base: {httpClient?.BaseAddress?.ToString()}");
            var urlWithParameters = BuildHeatPumpUrlWithParameters(httpClient?.BaseAddress?.ToString());
            Log.Debug($"<--GetUrl urlWithParameters {urlWithParameters}");
            var httpResponseMessage = httpClient.GetAsync(urlWithParameters);
            return await httpResponseMessage;
        }

        private static string BuildHeatPumpUrlWithParameters(string baseUrl)
        {
            Log.Debug($"-->BuildHeatPumpUrl {baseUrl}");
            var slash = string.Empty;
            if (!baseUrl.EndsWith("/"))
            {
                slash = "/";
            }
            var heatPumpUrl = baseUrl + slash;
            heatPumpUrl += "?s=1,1";
            Log.Debug($"<--BuildHeatPumpUrl {heatPumpUrl}");
            return heatPumpUrl;
        }
    }
}
