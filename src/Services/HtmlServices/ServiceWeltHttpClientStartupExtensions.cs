using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using System;
using System.Net;
using System.Net.Http;

namespace StiebelEltronDashboard.Services.HtmlServices;

public static class ServiceWeltHttpClientStartupExtensions
{
    public static IServiceCollection AddHttpClientForServiceWelt(this IServiceCollection services, Uri serviceWeltBaseUrl)
    {
        ArgumentNullException.ThrowIfNull(nameof(serviceWeltBaseUrl));

        services.ConfigurePostHttpClient(serviceWeltBaseUrl);
        services.ConfigureGetHttpClient(serviceWeltBaseUrl);
        return services;
    }

    private static void ConfigurePostHttpClient(this IServiceCollection services, Uri serviceWeltBaseUrl)
    {
        services.ConfigureServiceWeltHttpClientHandler(
                    ServiceWeltFacade.ServiceWeltPostClientName,
                    serviceWeltBaseUrl,
                    new HttpClientHandler()
                    {
                        UseDefaultCredentials = true,
                        AllowAutoRedirect = true,
                        UseCookies = true,
                        CookieContainer = new CookieContainer()
                    }).ConfigureServiceWeltHttpClient(serviceWeltBaseUrl);
    }

    private static void ConfigureGetHttpClient(this IServiceCollection services, Uri serviceWeltBaseUrl)
    {
        services.ConfigureServiceWeltHttpClientHandler(
            ServiceWeltFacade.ServiceWeltGetClientName,
            serviceWeltBaseUrl,
            new HttpClientHandler()
            {
                UseCookies = true
            }).ConfigureServiceWeltHttpClient(serviceWeltBaseUrl);
    }

    private static void ConfigureServiceWeltHttpClient(
        this IHttpClientBuilder httpClientBuilder,
        Uri serviceWeltBaseUrl)
    {
        Log.Debug($"Set Referer to : ${serviceWeltBaseUrl.ToString()}");
        httpClientBuilder.ConfigureHttpClient(httpClient =>
        {
            httpClient.DefaultRequestHeaders.Add("Accept-Language", "de-DE");
            httpClient.DefaultRequestHeaders.Add("accept", "application/xhtml+xml");
            httpClient.DefaultRequestHeaders.Add("accept-encoding", "gzip, deflate");
            httpClient.DefaultRequestHeaders.Add("Referer", serviceWeltBaseUrl.ToString());
        });
    }

    private static IHttpClientBuilder ConfigureServiceWeltHttpClientHandler(
        this IServiceCollection services,
        string name,
        Uri serviceWeltBaseUrl,
        HttpClientHandler httpClienthandler)
    {
        Log.Debug($"Set base url to ${name} Client: ${serviceWeltBaseUrl.ToString()}");
        return services.AddHttpClient(name, options =>
        {
            options.BaseAddress = serviceWeltBaseUrl;
        }).ConfigureHttpMessageHandlerBuilder(builder =>
        {
            builder.PrimaryHandler = httpClienthandler;
            builder.Build();
        });
    }
}
