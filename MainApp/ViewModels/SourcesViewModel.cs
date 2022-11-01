using CommunityToolkit.Mvvm.ComponentModel;

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
            }
        }

        private readonly CoveoSdk.Sources _sourceClient;
        private bool isInitialized = false;

        public SourcesViewModel(CoveoSdk.Sources sourcesClient)
        {
            Sources = new ObservableCollectionEx<CoveoSdk.Models.SourceModel>();
            _sourceClient = sourcesClient;
        }

        public async Task InitializeAsync()
        {
            if (!isInitialized)
            {
                Sources.ReloadData(await _sourceClient.GetsourcesAsync());
                isInitialized = true;
            }
        }
    }
}
