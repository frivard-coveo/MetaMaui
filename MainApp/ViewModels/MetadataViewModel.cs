using CommunityToolkit.Mvvm.ComponentModel;
using MetaMaui.Services.Metadata;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetaMaui.ViewModels
{
    [QueryProperty(nameof(SourceName), "SourceName")]
    public class MetadataViewModel : ObservableObject
    {
        public string SourceName { get; set; }
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
            if (!isInitialized)
            {
                AllMetas.ReloadData(await _metadataService.GetSourceMetadataAsync(SourceName));
                isInitialized = true;
            }
        }
    }
}
