using MetaMaui.ViewModels;

namespace MetaMaui.Views;

public partial class MetadataView : ContentPage
{
    public MetadataView(MetadataViewModel viewModel)
    {
        BindingContext = viewModel;
        InitializeComponent();
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        if (BindingContext is MetadataViewModel vm)
        {
            await vm.InitializeAsync();
        }
    }
}