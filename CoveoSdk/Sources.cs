using CoveoSdk.Models;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace CoveoSdk;

public class Sources
{
    public static JsonSerializerOptions jsonSerializerOptions = new JsonSerializerOptions() { DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull };
    private readonly HttpClient _httpClient;

    static Sources()
    {
        jsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        jsonSerializerOptions.Converters.Add(new Converters.UnixEpochMillisecondsConverter());
    }

    public static Sources BuildDevClient(string orgId, string apiKey)
    {
        var httpClient = new HttpClient();
        httpClient.BaseAddress = new Uri($"https://platformdev.cloud.coveo.com/rest/organizations/{orgId}/");
        httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", apiKey);
        return new Sources(httpClient);
    }

    public Sources(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<IReadOnlyList<Models.SourceModel>> GetsourcesAsync(CancellationToken ct = default)
    {
        var sourceModels = await _httpClient.GetFromJsonAsync<List<Models.SourceModel>>($"sources?page=0&perPage100&sortingOrder=ASC&sortingType=NAME", ct);
        if(sourceModels == null)
        {
            sourceModels = new List<Models.SourceModel>();
        }
        return sourceModels;
    }

}
