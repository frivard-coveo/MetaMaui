using CommunityToolkit.Mvvm.ComponentModel;
using MetaMaui.Services;
using MetaMaui.Services.Settings;
using System.Windows.Input;

namespace MetaMaui.ViewModels
{
    public class SourcesViewModel : ObservableObject
    {
        public ObservableCollectionEx<CoveoSdk.Models.SourceModel> Sources { get; private set;}
        public string PageTitle => "Oh wow, this is binding! Here are your sources.";

        private CoveoSdk.Models.SourceModel _selectedSource;
        public CoveoSdk.Models.SourceModel SelectedSource
        {
            get => _selectedSource;
            set
            {
                SetProperty(ref _selectedSource, value);
                _settingsService.SourceId = value.id;
            }
        }

        public ICommand NavigateToMetadata { get; private set; }

        private readonly CoveoSdk.Sources _sourceClient;
        private readonly ISettingsService _settingsService;
        private bool isInitialized = false;

        public SourcesViewModel(CoveoSdk.Sources sourcesClient, INavigationService navigationService, ISettingsService settingsService)
        {
            Sources = new ObservableCollectionEx<CoveoSdk.Models.SourceModel>();
            _sourceClient = sourcesClient;
            _settingsService = settingsService;
            NavigateToMetadata = new Command(obj =>
            {
                if(obj is CoveoSdk.Models.SourceModel source)
                {
                    navigationService.NavigateToAsync("Metadata", new Dictionary<string, object> { { "Source", source } });
                }
            });
        }

        public async Task InitializeAsync()
        {
            if (!isInitialized)
            {
                Sources.ReloadData(await _sourceClient.GetsourcesAsync());
                if (Sources.Any())
                {
                    // try to find the previously selected source
                    string previousId = _settingsService.SourceId;
                    if (!string.IsNullOrEmpty(previousId))
                    {
                        var previouslySelected = Sources.FirstOrDefault(src => string.Equals(src.id, previousId, StringComparison.Ordinal));
                        if(previouslySelected != null)
                        {
                            SelectedSource = previouslySelected;
                        }
                        else
                        {
                            SelectedSource = Sources.First();
                        }
                    }
                    else
                    {
                        SelectedSource = Sources.First();
                    }
                }
                isInitialized = true;
            }
        }
    }
}
