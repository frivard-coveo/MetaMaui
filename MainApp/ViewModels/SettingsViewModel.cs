using CommunityToolkit.Mvvm.ComponentModel;
using MetaMaui.Services.Settings;

namespace MetaMaui.ViewModels
{
    public class SettingsViewModel : ObservableObject
    {
        private readonly ISettingsService _settingsService;

        public string ApiKey {
            get => _settingsService.ApiKey;
            set => _settingsService.ApiKey = value;
            }

        public string OrganizationId { 
            get => _settingsService.OrganizationId;
            set => _settingsService.OrganizationId = value;
            }

        public SettingsViewModel(ISettingsService settingsService)
        {
            _settingsService = settingsService;
        }
    }
}
