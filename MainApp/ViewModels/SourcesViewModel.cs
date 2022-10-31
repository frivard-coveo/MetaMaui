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
        public ObservableCollectionEx<string> Sources { get; private set;}
        public string PageTitle => "Oh wow, this is binding! Here are your sources.";

        public SourcesViewModel()
        {
            Sources = new ObservableCollectionEx<string>();
        }

        public async Task InitializeAsync()
        {
            await Sources.ReloadDataAsync(
                async innerList =>
                {
                    Sources.Add("AAA");
                    await Task.Delay(TimeSpan.FromMilliseconds(50));
                    Sources.Add("SpaceJam");
                    await Task.Delay(TimeSpan.FromMilliseconds(50));
                });
        }
    }
}
