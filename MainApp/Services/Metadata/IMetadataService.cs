using MetaMaui.Models;

namespace MetaMaui.Services.Metadata
{
    public interface IMetadataService
    {
        Task<IReadOnlyList<SourceMetadataModel>> GetSourceMetadataAsync(string source, CancellationToken ct = default);
    }
}
