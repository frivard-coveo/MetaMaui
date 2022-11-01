namespace MetaMaui.Services.Settings
{
    public class SettingsService : ISettingsService
    {
        #region Setting Constants

        private const string PrefIdOrgId = "orgId";
        private const string PrefIdApiKey = "token";
        private const string PrefIdSourceId = "sourceid";

        private readonly string ApiKeyDefault = string.Empty;
        private readonly string OrgIdDefault = string.Empty;
        private readonly string SourceIdDefault = string.Empty;

        #endregion

        public string ApiKey {
            get => Preferences.Get(PrefIdApiKey, ApiKeyDefault);
            set => Preferences.Set(PrefIdApiKey, value);
        }

        public string OrganizationId {
            get => Preferences.Get(PrefIdOrgId, OrgIdDefault);
            set => Preferences.Set(PrefIdOrgId, value);
        }
        public string SourceId
        {
            get => Preferences.Get(PrefIdSourceId, SourceIdDefault);
            set => Preferences.Set(PrefIdSourceId, value);
        }

    }
}
