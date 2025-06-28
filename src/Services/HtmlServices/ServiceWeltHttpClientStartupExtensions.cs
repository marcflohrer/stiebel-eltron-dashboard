using Microsoft.Extensions.DependencyInjection;
using Polly;
using Polly.Contrib.WaitAndRetry;
using Serilog;
using System;
using System.Net;
using System.Net.Http;

namespace StiebelEltronDashboard.Services.HtmlServices;

public static class ServiceWeltHttpClientStartupExtensions
{
    public static IServiceCollection AddHttpClientForServiceWelt(this IServiceCollection services, Uri serviceWeltBaseUrl)
    {
        services.ConfigurePostHttpClient(serviceWeltBaseUrl);
        services.ConfigureGetHttpClient(serviceWeltBaseUrl);
        return services;
    }

    private static void ConfigurePostHttpClient(this IServiceCollection services, Uri serviceWeltBaseUrl)
    {
        services.AddHttpClient(ServiceWeltFacade.ServiceWeltPostClientName, options =>
        {
            options.BaseAddress = serviceWeltBaseUrl;
        }).ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler()
        {
            UseDefaultCredentials = true,
            AllowAutoRedirect = true,
            UseCookies = false,
            CookieContainer = new CookieContainer()
        })
        .AddRetryPolicy();
    }

    private static void ConfigureGetHttpClient(this IServiceCollection services, Uri serviceWeltBaseUrl)
    {
        services.AddHttpClient(ServiceWeltFacade.ServiceWeltGetClientName, options =>
        {
            options.BaseAddress = serviceWeltBaseUrl;
        }).ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler()
        {
            UseCookies = false
        })
        .AddRetryPolicy();
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

    private static IHttpClientBuilder AddRetryPolicy(this IHttpClientBuilder httpClientBuilder)
        => httpClientBuilder.AddTransientHttpErrorPolicy(
        policyBuilder => policyBuilder
            .WaitAndRetryAsync(Backoff.DecorrelatedJitterBackoffV2(
                TimeSpan.FromSeconds(1), 5)));
}
