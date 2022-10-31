using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetaMaui.ViewModels
{
    public class SourcesViewModel : ObservableObject
    {
        public ObservableCollectionEx<CoveoSdk.Models.SourceModel> Sources { get; private set;}
        public string PageTitle => "Oh wow, this is binding! Here are your sources.";

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
                await Sources.ReloadDataAsync(
                    async innerList =>
                    {
                        var srcs = await _sourceClient.GetsourcesAsync();
                        foreach (var src in srcs)
                        {
                            Sources.Add(src);
                        }
                    });
                isInitialized = true;
            }
        }
    }
}
