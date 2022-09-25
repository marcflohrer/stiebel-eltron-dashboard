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
        private readonly string _serviceWeltUser;
        private readonly string _serviceWeltPassword;
        public static string ServiceWeltPostClientName => "ServiceWeltPostClient";
        public static string ServiceWeltGetClientName => "ServiceWeltGetClient";

        public IHttpClientFactory HttpClientFactory { get; }

        public ServiceWeltFacade(IConfiguration configuration, IHttpClientFactory httpClientFactory)
        {
            _serviceWeltUser = configuration["ServiceWeltUser"] as string ?? throw new Exception("Missing configuration 'ServiceWeltUser'");
            _serviceWeltPassword = configuration["ServiceWeltPassword"] as string ?? throw new Exception("Missing configuration 'ServiceWeltPassword'");
            HttpClientFactory = httpClientFactory;
        }

        public async Task<ServiceWelt> ReadLanguageSettingAsync(string sessionId)
        {
            var httpContent = $"make=send&user={_serviceWeltUser}&pass={_serviceWeltPassword}";
            Log.Debug($"ReadLanguageSettingAsync {httpContent}");

            using var httpClient = CreatePostHttpClient(sessionId);
            var fullUrl = BuildReadLanguageUrlWithParameters(httpClient?.BaseAddress?.ToString());

            var httpResponseMessage = await PostUrl(httpClient, fullUrl, httpContent);
            var content = await httpResponseMessage.Content.ReadAsStringAsync();
            Log.Debug($"ReadLanguageSettingAsync content {content}");
            return new ServiceWelt()
            {
                HtmlDocument = content
            };
        }

        private static string BuildReadLanguageUrlWithParameters(string baseUrl)
        {
            Log.Debug($"-->BuildReadLanguageUrlWithParameters {baseUrl}");
            var slash = string.Empty;
            if (!baseUrl.EndsWith("/"))
            {
                slash = "/";
            }
            var result = baseUrl + slash;
            result += "?s=5,3";
            Log.Debug($"<--BuildReadLanguageUrlWithParameters {result}");
            return result;
        }

        public async Task<ServiceWelt> GetHeatPumpWebsiteAsync(string sessionId)
        {
            var httpContent = $"make=send&user={_serviceWeltUser}&pass={_serviceWeltPassword}";
            Log.Debug($"GetHeatPumpWebsiteAsync {httpContent}");

            using var httpClient = CreatePostHttpClient(sessionId);
            var fullUrl = BuildHeatPumpUrlWithParameters(httpClient?.BaseAddress?.ToString(), "?s=1,1");

            var httpResponseMessage = await PostUrl(httpClient, fullUrl, httpContent);
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

        private static async Task<HttpResponseMessage> PostUrl(HttpClient httpClient,
            string fullUrl,
            string httpContent)
        {
            var response = await httpClient.PostAsync(fullUrl, new StringContent(httpContent, Encoding.ASCII, "application/x-www-form-urlencoded"));
            Log.Debug($"PostUrl: ISG response status is {response.StatusCode}");
            response.EnsureSuccessStatusCode();
            return response;
        }

        private async Task<HttpResponseMessage> GetUrl(string sessionId)
        {
            using var httpClient = BuildGetHttpClient(sessionId);
            Log.Debug($"<--GetUrl base: {httpClient?.BaseAddress?.ToString()}");
            var urlWithParameters = BuildHeatPumpUrlWithParameters(httpClient?.BaseAddress?.ToString(), "?s=1,1");
            Log.Debug($"<--GetUrl urlWithParameters {urlWithParameters}");
            var httpResponseMessage = httpClient.GetAsync(urlWithParameters);
            return await httpResponseMessage;
        }

        private static string BuildHeatPumpUrlWithParameters(string baseUrl, string pageAddress)
        {
            Log.Debug($"-->BuildHeatPumpUrlWithParameters {baseUrl}");
            var slash = string.Empty;
            if (!baseUrl.EndsWith("/"))
            {
                slash = "/";
            }
            var heatPumpUrl = baseUrl + slash;
            heatPumpUrl += $"{pageAddress}";
            Log.Debug($"<--BuildHeatPumpUrlWithParameters {heatPumpUrl}");
            return heatPumpUrl;
        }

        public async Task SetLanguageAsync(string sessionId, string language)
        {
            var httpContent = $"make=send&user={_serviceWeltUser}&pass={_serviceWeltPassword}&data=[{{\"name\":\"valspracheeinstellung\",\"value\":\"{language}\"}}]";
            Log.Debug($"SetLanguageAsync {httpContent}");

            using var httpClient = CreatePostHttpClient(sessionId);
            var fullUrl = BuildHeatPumpUrlWithParameters(httpClient?.BaseAddress?.ToString(), "save.php");
            Log.Information($"SetLanguageAsync {fullUrl}, :: {httpContent}");
            var httpResponseMessage = await PostUrl(httpClient, fullUrl, httpContent);
        }
    }
}
