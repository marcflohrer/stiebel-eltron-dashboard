using Microsoft.Extensions.Configuration;
using Serilog;
using StiebelEltronDashboard.Models;
using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using static StiebelEltronDashboard.Services.HtmlServices.ServiceWeltFacadeHelper;

namespace StiebelEltronDashboard.Services.HtmlServices
{
    public class ServiceWeltFacade : ServiceWeltFacadeBase, IServiceWeltFacade
    {
        private readonly string _serviceWeltUser;
        private readonly string _serviceWeltPassword;

        public ServiceWeltFacade(IConfiguration configuration, IHttpClientFactory httpClientFactory)
            : base(httpClientFactory, configuration["ServiceWeltUser"], configuration["ServiceWeltPassword"])
        {
            _serviceWeltUser = configuration["ServiceWeltUser"] as string ?? throw new Exception("Missing configuration 'ServiceWeltUser'");
            _serviceWeltPassword = configuration["ServiceWeltPassword"] as string ?? throw new Exception("Missing configuration 'ServiceWeltPassword'");
        }

        public async Task<ServiceWelt> ReadLanguageSettingAsync(string sessionId)
        {
            using var httpClient = CreatePostHttpClient(sessionId);
            return await base.GetWebsiteContentAsync(sessionId,
                ConcatBaseUrlAndPageAddress(
                    httpClient?.BaseAddress?.ToString(),
                    "?s=5,3"));
        }

        public async Task<ServiceWelt> GetHeatPumpWebsiteAsync(string sessionId)
        {
            using var httpClient = CreatePostHttpClient(sessionId);
            return await base.GetWebsiteContentAsync(sessionId,
                ConcatBaseUrlAndPageAddress(
                    httpClient?.BaseAddress?.ToString(),
                    "?s=1,1"));
        }

        public async Task SetLanguageAsync(string sessionId, string language)
        {
            var httpContent = $"make=send&user={_serviceWeltUser}&pass={_serviceWeltPassword}&data=[{{\"name\":\"valspracheeinstellung\",\"value\":\"{language}\"}}]";

            using var httpClient = CreatePostHttpClient(sessionId);
            var fullUrl = ConcatBaseUrlAndPageAddress(httpClient?.BaseAddress?.ToString(), "save.php");

            var httpResponseMessage = await PostUrl(httpClient, fullUrl, httpContent);
        }
    }
}
