namespace MetaMaui.Services.Settings
{
    public class SettingsService : ISettingsService
    {
        #region Setting Constants

        private const string PrefIdOrgId = "orgId";
        private const string PrefIdApiKey = "token";

        private readonly string ApiKeyDefault = string.Empty;
        private readonly string OrgIdDefault = string.Empty;

        #endregion

        public string ApiKey {
            get => Preferences.Get(PrefIdApiKey, ApiKeyDefault);
            set => Preferences.Set(PrefIdApiKey, value);
        }

        public string OrganizationId {
            get => Preferences.Get(PrefIdOrgId, OrgIdDefault);
            set => Preferences.Set(PrefIdOrgId, value);
        }
    }
}
