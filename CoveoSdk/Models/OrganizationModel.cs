using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace CoveoSdk.Models;

public record LicenseModel
{
    [JsonExtensionData]
    public Dictionary<string, JsonElement>? ExtensionData { get; set; }
}

public record BusinessAnalyticsModel
{
    [JsonExtensionData]
    public Dictionary<string, JsonElement>? ExtensionData { get; set; }
}

public record OrganizationOwnerModel
{
    public string email { get; init; } = default!;
}

public record LightProvisioningStatusModel
{
    public enum Status
    {
        HEALTHY, ERROR
    }

    public float currentProvisioningProgress { get; init;} = default!;
    public bool initialProvisioningDone { get; init; } = default!;
    public DateTimeOffset lastProvisioningCompletedDate { get; init; } = default!;
    public bool ongoing { get; init; } = default!;
    public Status status { get; init; } = default!;

}

public record OrganizationStatusModel
{
    public enum Status
    {
        HEALTHY, ERROR
    }
    public enum LifeCycleState
    {
        ACTIVE, PAUSED, DELETING
    }
    public enum ErrorCode
    {
        SEARCH_ERROR, PAUSING_FAILED, RESUMING_FAILED
    }

    public Status status { get; init; } = default!;
    public LifeCycleState lifeCycleState { get; init; } = default!;
    public List<ErrorCode> errorCodes { get; init; } = default!;
    public LightProvisioningStatusModel? provisioningStatus { get; init; } = default;
    public string pauseState { get; init; } = default!; 
    public bool readOnly { get; init; } = default!;
    public bool supportActivated { get; init; } = default!;
}

public record OrganizationModel
{
    public string id { get; init; } = default!;
    public string displayName { get; init; } = default!;
    public DateTimeOffset createdDate { get; init; } = default!;
    public LicenseModel? license { get; init; } = default;
    public BusinessAnalyticsModel? businessAnalytics { get; init; } = default;
    public OrganizationOwnerModel owner { get; init; } = default!;
    public bool readOnly { get; init; } = default!;
    public LightProvisioningStatusModel? provisioningStatus { get; init; } = default;
    public OrganizationStatusModel? organizationStatus { get; init; } = default;
    public bool publicContentOnly { get; init; } = default!;
}
