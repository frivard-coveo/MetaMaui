using System.Text.Json;
using System.Text.Json.Serialization;

namespace CoveoSdk.Models;

public record RestException(
    string code,
    string? context
    );

public record RestHighlightResponse(
    int length,
    int offset
    );

public record RestQueryResult(
    string title,
    string uri,
    string printableUri,
    string clickUri,
    string uniqueId,
    string excerpt,
    string firstSentences,
    string summary,
    string flags,
    bool hasHtmlVersion,
    bool hasMobileHtmlVersion,
    int score,
    double percentScore,
    string rankingInfo,
    double rating,
    bool isTopResult,
    bool isRecommendation,
    bool isUserActionView,
    string rankingModifier,
    List<RestHighlightResponse> titleHighlights,
    List<RestHighlightResponse> firstSentencesHighlights,
    List<RestHighlightResponse> excerptHighlights,
    List<RestHighlightResponse> printableUriHighlights,
    List<RestHighlightResponse> summaryHighlights
    )
{
    [JsonExtensionData]
    public Dictionary<string, JsonElement>? ExtensionData { get; set; }
}

public record RestQueryResponse(
    int totalCount,
    int totalCountFiltered,
    int duration,
    int indexDuration,
    int requestDuration,
    string searchUid,
    string pipeline,
    int apiVersion,
    RestException? exception,
    bool isFallbackToAdmin,
    List<string> warnings,
    List<string> errors,
    string index,
    string indexToken,
    List<RestQueryResult> results
    )
{
    [JsonExtensionData]
    public Dictionary<string, JsonElement>? ExtensionData { get; set; }
}
