using System;
using System.Net;
using System.Net.Http;
using M13.Domain.AppSettings;
using M13.Domain.Providers;
using M13.Infrastructure.Providers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace M13.InterviewProject.Middlewares
{
    public static class HttpClientFactoryMiddleware
    {
        public static void AddHttpClients(this IServiceCollection services)
        {
            services.AddHttpClient<ISpellServiceProvider, YandexSpellServiceProvider>()
                .ConfigureHttpClient((serviceProvider, client) =>
                {
                    var options = serviceProvider.GetService<IOptions<AppSettings>>().Value;
                    client.BaseAddress = new Uri(options.YandexSpellServiceOptions.BaseUrl);
                });

            services.AddHttpClient<HttpClientProvider>()
                .ConfigurePrimaryHttpMessageHandler(messageHandler =>
                {
                    var handler = new HttpClientHandler();
                    
                    if (handler.SupportsAutomaticDecompression)
                    {
                        handler.AutomaticDecompression = DecompressionMethods.Deflate;
                    }

                    handler.AllowAutoRedirect = true;

                    return handler;
                });
        }
    }
}