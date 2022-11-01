using MetaMaui.Models;
using System.Text.Json;

namespace MetaMaui.Services.Metadata
{
    public class MetadataService : IMetadataService
    {
        private CoveoSdk.Search _searchClient;
        public MetadataService(CoveoSdk.Search searchClient)
        {
            _searchClient = searchClient;
        }

        public async Task<IReadOnlyList<SourceMetadataModel>> GetSourceMetadataAsync(string source, CancellationToken ct = default)
        {
            var metaList = new List<SourceMetadataModel>();
            var queryResults = await _searchClient.QueryAsync(query: "@coveo_metadatasampling", advancedQuery: $"@source={source}", viewAllContent: true, ct);

            byte[] data;
            foreach (var docId in queryResults.results.Select(r => r.uniqueId))
            {
                try
                {
                    data = await _searchClient.GetDocumentDatastreamAsync(docId, "allmetadata", viewAllContent: true, ct);
                }
                catch (Exception)
                {
                    continue;
                }
                try
                {
                    var dataStr = System.Text.UTF8Encoding.UTF8.GetString(data);
                    var metas = JsonSerializer.Deserialize<List<MetaForOrigin>>(data);
                    if (metas != null)
                    {
                        foreach (var origin in metas)
                        {
                            foreach(var kvps in origin.Values)
                            {
                                var existingRecord = metaList.FirstOrDefault(m => string.Equals(m.Key, kvps.Key, StringComparison.Ordinal));
                                if (existingRecord == null)
                                {
                                    existingRecord = new SourceMetadataModel() { Key = kvps.Key, IsMapped = false, Origins = new List<string>(), TopValues = new List<string>() };
                                    metaList.Add(existingRecord);
                                }
                                if(!existingRecord.Origins.Any(o => string.Equals(o, origin.Origin, StringComparison.Ordinal)))
                                {
                                    existingRecord.Origins.Add(origin.Origin);
                                }
                                foreach (var val in kvps.Value)
                                {
                                    existingRecord.TopValues.Add(val.ToString());
                                }
                            }
                        }
                    }
                }
                catch (System.Text.Json.JsonException)
                {
                    Console.WriteLine("Unable to deserialize the answer. The json string was:");
                    string problematicData = System.Text.UTF8Encoding.UTF8.GetString(data);
                    Console.WriteLine(problematicData);
                    throw;
                }
            }

            // keep only top 5 values
            foreach(var meta in metaList)
            {
                meta.TopValues = meta.TopValues.Take(5).ToList();
            }

            return metaList;
        }
    }
}
