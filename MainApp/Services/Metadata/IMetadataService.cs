using MetaMaui.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetaMaui.Services.Metadata
{
    public interface IMetadataService
    {
        Task<IReadOnlyList<SourceMetadataModel>> GetSourceMetadataAsync(string source, CancellationToken ct = default);
    }
}
