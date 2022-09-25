using Serilog;
using StiebelEltronDashboard.Models;
using System;
using System.Net.Http;

using System.Threading.Tasks;

namespace StiebelEltronDashboard.Services.HtmlServices
{
    public class ServiceWeltFacadeBase
    {
        public IHttpClientFactory HttpClientFactory { get; }
        public string ServiceWeltUser { get; }
        public string ServiceWeltPassword { get; }

        public static string ServiceWeltPostClientName => "ServiceWeltPostClient";
        public static string ServiceWeltGetClientName => "ServiceWeltGetClient";

        public ServiceWeltFacadeBase(IHttpClientFactory httpClientFactory, string serviceWeltUser, string serviceWeltPassword)
        {
            HttpClientFactory = httpClientFactory;
            ServiceWeltUser = serviceWeltUser;
            ServiceWeltPassword = serviceWeltPassword;
        }

        protected HttpClient CreatePostHttpClient(string sessionId)
        {
            Log.Debug($"-->CreatePostHttpClient");
            var httpClient = ServiceWeltFacadeHelper.AddSessionCookie(HttpClientFactory.CreateClient(ServiceWeltPostClientName), sessionId);
            if (httpClient == null)
            {
                Log.Error($"<--CreatePostHttpClient: httpclient is null.");
            }
            Log.Debug($"<--CreatePostHttpClient");
            return httpClient;
        }

        public async Task<ServiceWelt> GetWebsiteContentAsync(string sessionId, string fullUrl)
        {
            var httpContent = $"make=send&user={ServiceWeltUser}&pass={ServiceWeltPassword}";

            using var httpClient = CreatePostHttpClient(sessionId);

            var httpResponseMessage = await ServiceWeltFacadeHelper.PostUrl(httpClient, fullUrl, httpContent);
            var content = await httpResponseMessage.Content.ReadAsStringAsync();

            return new ServiceWelt()
            {
                HtmlDocument = content
            };
        }

        protected HttpClient BuildGetHttpClient(string sessionId)
            => ServiceWeltFacadeHelper.AddSessionCookie(HttpClientFactory.CreateClient(ServiceWeltGetClientName), sessionId);

        protected async Task<HttpResponseMessage> GetUrl(string sessionId)
        {
            using var httpClient = BuildGetHttpClient(sessionId);
            Log.Debug($"<--GetUrl base: {httpClient?.BaseAddress?.ToString()}");

            var urlWithParameters = ServiceWeltFacadeHelper
                .ConcatBaseUrlAndPageAddress(httpClient?.BaseAddress?.ToString(), "?s=1,1");

            Log.Debug($"<--GetUrl urlWithParameters {urlWithParameters}");
            var httpResponseMessage = httpClient.GetAsync(urlWithParameters);
            return await httpResponseMessage;
        }
    }
}

