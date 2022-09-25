using Serilog;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace StiebelEltronDashboard.Services.HtmlServices
{
    public static class ServiceWeltFacadeHelper
    {
        public static HttpClient AddSessionCookie(HttpClient httpClient, string sessionId)
        {
            httpClient.DefaultRequestHeaders.Add("Cookie", $"PHPSESSID={sessionId}");
            return httpClient;
        }

        public static async Task<HttpResponseMessage> PostUrl(
            HttpClient httpClient,
            string fullUrl,
            string httpContent)
        {
            var response = await httpClient.PostAsync(fullUrl,
                new StringContent(httpContent, Encoding.ASCII,
                "application/x-www-form-urlencoded"));

            response.EnsureSuccessStatusCode();
            return response;
        }

        public static string ConcatBaseUrlAndPageAddress(string baseUrl, string pageAddress)
        {
            var slash = string.Empty;
            if (!baseUrl.EndsWith("/"))
            {
                slash = "/";
            }
            var heatPumpUrl = baseUrl + slash;
            heatPumpUrl += $"{pageAddress}";
            return heatPumpUrl;
        }
    }
}

