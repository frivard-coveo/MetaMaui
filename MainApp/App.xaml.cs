using MetaMaui.Services;

namespace MetaMaui;

public partial class App : Application
{
    public App(INavigationService navigationService)
    {
        InitializeComponent();

        MainPage = new AppShell(navigationService);
    }
}
