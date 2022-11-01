using CommunityToolkit.Mvvm.ComponentModel;
using MetaMaui.Services.Metadata;

namespace MetaMaui.ViewModels
{
    [QueryProperty(nameof(Source), "Source")]
    public class MetadataViewModel : ObservableObject
    {
        public CoveoSdk.Models.SourceModel Source { get; set; }
        public ObservableCollectionEx<Models.SourceMetadataModel> AllMetas { get; private set;}

        private readonly IMetadataService _metadataService;
        private bool isInitialized = false;

        public MetadataViewModel(IMetadataService metadataService)
        {
            AllMetas = new ObservableCollectionEx<Models.SourceMetadataModel>();
            _metadataService = metadataService;
        }

        public async Task InitializeAsync()
        {
            if (!isInitialized && Source != null)
            {
                AllMetas.ReloadData(await _metadataService.GetSourceMetadataAsync(Source.name));
                isInitialized = true;
            }
        }
    }
}
