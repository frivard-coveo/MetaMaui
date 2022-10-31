using MetaMaui.ViewModels;

namespace MetaMaui.Views;

public partial class SourcesView : ContentPage
{
	public SourcesView(SourcesViewModel viewModel)
	{
        BindingContext = viewModel;
		InitializeComponent();
	}

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        if (BindingContext is SourcesViewModel svm)
        {
            await svm.InitializeAsync();
        }
    }
}