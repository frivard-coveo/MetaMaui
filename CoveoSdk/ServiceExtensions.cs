using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CoveoSdk
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddCoveoSdk(this IServiceCollection services, string? orgId, string apiKey)
        {
            services.AddHttpClient<Organizations>(httpClient =>
            {
                httpClient.BaseAddress = new Uri("https://platformdev.cloud.coveo.com/rest/");
                httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", apiKey);
            });
            if (orgId != null)
            {
                services.AddHttpClient<Sources>(httpClient =>
                {
                    httpClient.BaseAddress = new Uri($"https://platformdev.cloud.coveo.com/rest/organizations/{orgId}/");
                    httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", apiKey);
                });
                services.AddHttpClient<Search>(httpClient =>
                {
                    httpClient.BaseAddress = new Uri($"https://platformdev.cloud.coveo.com/rest/search/");
                    httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", apiKey);
                });
                Search._orgId = orgId;
            }
            return services;
        }
    }
}
