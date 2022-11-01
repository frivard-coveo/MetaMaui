using MetaMaui.Models;
using System.Text.Json;

namespace MetaMaui.Services.Metadata
{
    public class MetadataService : IMetadataService
    {
        private Dictionary<string, List<MetaForOrigin>> _documentsCache = new Dictionary<string, List<MetaForOrigin>>();
        private CoveoSdk.Search _searchClient;
        public MetadataService(CoveoSdk.Search searchClient)
        {
            _searchClient = searchClient;
        }

        public async Task<IReadOnlyList<SourceMetadataModel>> GetSourceMetadataAsync(string source, CancellationToken ct = default)
        {
            if(_documentsCache.Count == 0)
            {
                await RefreshDocuments(source, ct);
            }

            var metaList = new List<SourceMetadataModel>();
            foreach (var docId in _documentsCache.Keys)
            {
                var metas = _documentsCache[docId];
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

            // keep only top 5 values
            foreach(var meta in metaList)
            {
                meta.TopValues = meta.TopValues.Take(5).ToList();
            }

            return metaList;
        }

        public async Task RefreshDocuments(string sourceName, CancellationToken ct = default)
        {
            _documentsCache.Clear();

            var queryResults = await _searchClient.QueryAsync(query: "@coveo_metadatasampling", advancedQuery: $"@source={sourceName}", viewAllContent: true, ct);

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
                        _documentsCache.Add(docId, metas);
                    }
                }
                catch (JsonException)
                {
                    string problematicData = System.Text.UTF8Encoding.UTF8.GetString(data);
                    throw new Exception($"Unable to deserialize the answer. The json string was:{Environment.NewLine}{problematicData}");
                }
            }
        }
    }
}