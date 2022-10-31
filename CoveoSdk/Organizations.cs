using CoveoSdk.Models;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace CoveoSdk;

public class Organizations
{
    public static JsonSerializerOptions jsonSerializerOptions = new JsonSerializerOptions() { DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull };
    private readonly HttpClient _httpClient;

    static Organizations()
    {
        jsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        jsonSerializerOptions.Converters.Add(new Converters.UnixEpochMillisecondsConverter());
    }

    public static Organizations BuildDevClient(string apiKey)
    {
        var httpClient = new HttpClient();
        httpClient.BaseAddress = new Uri($"https://platformdev.cloud.coveo.com/rest/");
        httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", apiKey);
        return new Organizations(httpClient);
    }

    public Organizations(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<IReadOnlyList<Models.OrganizationModel>> GetOrganizationsAsync(CancellationToken ct = default)
    {
        var organizationModels = await _httpClient.GetFromJsonPagedAsync<Models.OrganizationModel>("organizations?order=asc&sortBy=displayName", 100, ct);
        return organizationModels;
    }

}
