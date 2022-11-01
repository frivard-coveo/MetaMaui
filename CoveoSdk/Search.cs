using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Threading.Tasks;
using CoveoSdk.Models;
using System.Net.Http.Json;
using Microsoft.AspNetCore.WebUtilities;
using System.Collections;

namespace CoveoSdk
{
    public class Search
    {
        public static JsonSerializerOptions jsonSerializerOptions = new JsonSerializerOptions() { DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull };
        private readonly HttpClient _httpClient;
        public static string _orgId;

        static Search()
        {
            jsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            jsonSerializerOptions.Converters.Add(new Converters.UnixEpochMillisecondsConverter());
            _orgId = "";
        }

        public static Search BuildDevClient(string orgId, string apiKey)
        {
            var httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri($"https://platformdev.cloud.coveo.com/rest/search/");
            httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", apiKey);
            return new Search(httpClient);
        }

        public Search(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<RestQueryResponse> QueryAsync(string query, string? advancedQuery = null, bool viewAllContent = false, CancellationToken ct = default)
        {
            const int resultsPerPage = 1000;
            int page = 0;
            var param = new Dictionary<string, string>() {
                { "organizationId", _orgId },
                { "numberOfResults", resultsPerPage.ToString() },
                { "q", query},
                { "viewAllContent", viewAllContent.ToString()} };
            if (advancedQuery != null)
            {
                param.Add("aq", advancedQuery);
            }
            var uri = new Uri(QueryHelpers.AddQueryString("v2", param), UriKind.Relative);
            RestQueryResponse response = (await _httpClient.GetFromJsonAsync<RestQueryResponse>(uri, ct)) ?? throw new Exception("Could not parse response");
            if (response.totalCount > resultsPerPage && response.results.Count == resultsPerPage)
            {
                // more data!
                int firstResult = 0;
                param.Add("firstResult", firstResult.ToString());
                do
                {
                    page++;
                    firstResult += resultsPerPage;
                    try
                    {
                        param["firstResult"] = firstResult.ToString();
                        uri = new Uri(QueryHelpers.AddQueryString("v2", param), UriKind.Relative);
                        RestQueryResponse pagedResponse = (await _httpClient.GetFromJsonAsync<RestQueryResponse>(uri, ct)) ?? throw new Exception("Could not parse response");
                        response.results.AddRange(pagedResponse.results);
                        if (pagedResponse.results.Count < resultsPerPage)
                        {
                            break;
                        }
                    }
                    catch (Exception)
                    {
                        break;
                    }
                } while (response.results.Count < response.totalCount);

            }

            return response;
        }

        public async Task<byte[]> GetDocumentDatastreamAsync(string documentId, string stream, bool viewAllContent = false, CancellationToken ct = default)
        {
            var param = new Dictionary<string, string>() {
                { "organizationId", _orgId },
                { "uniqueId", documentId },
                { "dataStream", stream},
                { "viewAllContent", viewAllContent.ToString()} 
            };
            var uri = new Uri(QueryHelpers.AddQueryString("v2", param), UriKind.Relative);
            var response = await _httpClient.GetAsync(uri, ct);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsByteArrayAsync();
        }
    }
}
