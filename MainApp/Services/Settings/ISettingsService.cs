namespace MetaMaui.Services.Settings
{
    public interface ISettingsService
    {
        string ApiKey { get; set;}
        string OrganizationId { get; set; }
        string SourceId { get; set; }
    }
}
