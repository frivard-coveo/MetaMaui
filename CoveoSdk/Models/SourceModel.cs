using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace CoveoSdk.Models;

public record SourceModel
{
    public string id { get; init; } = default!;
    public string name { get; init; } = default!;
    public string owner { get; init; } = default!;
    public string resourceId { get; init; } = default!;
    public string sourceType { get; init; } = default!;
}
