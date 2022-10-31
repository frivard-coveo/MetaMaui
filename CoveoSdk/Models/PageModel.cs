using Microsoft.AspNetCore.WebUtilities;
using System.Net.Http.Json;

namespace CoveoSdk.Models;

public record PageModel<T>
{
    public List<T> items { get; init; } = default!;
    public int totalPages { get; init; } = default!;
    public int totalEntries { get; init; } = default!;
}

public static class PageExtensions
{
    public static async Task<IReadOnlyList<T>> GetFromJsonPagedAsync<T>(this HttpClient _httpClient, string query, int perPage = 100, CancellationToken ct = default) where T: new()
    {
        List<T> values = new ();
        int page = 0;
        var param = new Dictionary<string, string>() { { "page", page.ToString() }, { "perPage", perPage.ToString() } };
        int nbResults = 0;

        do
        {
            param["page"] = page.ToString();
            param["perPage"] = perPage.ToString();
            var uri = new Uri(QueryHelpers.AddQueryString(query, param), UriKind.Relative);
            var batch = await _httpClient.GetFromJsonAsync<PageModel<T>>(
                uri, Organizations.jsonSerializerOptions, ct
                );
            if (batch != null)
            {
                nbResults = batch.items.Count;
                values.AddRange(batch.items);
                ++page;
            }
        } while (nbResults == perPage);
        return values;
    }
}
