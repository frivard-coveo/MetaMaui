using MetaMaui.ViewModels;

namespace MetaMaui.Views;

public partial class SettingsView : ContentPage
{
	public SettingsView(SettingsViewModel viewModel)
	{
        BindingContext = viewModel;
		InitializeComponent();
	}
}