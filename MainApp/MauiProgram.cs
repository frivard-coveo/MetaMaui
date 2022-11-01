using CoveoSdk;
using MetaMaui.Services;
using MetaMaui.Services.Metadata;
using MetaMaui.Services.Settings;
using Syncfusion.Maui.Core.Hosting;
using Syncfusion.Maui.DataGrid.Hosting;

namespace MetaMaui;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });
        builder.RegisterAppServices();
        builder.RegisterViewModels();
        builder.RegisterViews();
        var settingsService = builder.Services.BuildServiceProvider().GetService<ISettingsService>();
        builder.Services.AddCoveoSdk(settingsService.OrganizationId, settingsService.ApiKey);
        builder.ConfigureSyncfusionCore();
        builder.ConfigureSyncfusionDataGrid();

        return builder.Build();
    }
    public static MauiAppBuilder RegisterAppServices(this MauiAppBuilder mauiAppBuilder)
    {
        mauiAppBuilder.Services.AddSingleton<ISettingsService, SettingsService>();
        mauiAppBuilder.Services.AddSingleton<INavigationService, MauiNavigationService>();
        mauiAppBuilder.Services.AddSingleton<IMetadataService, MetadataService>();
        return mauiAppBuilder;
    }

    public static MauiAppBuilder RegisterViewModels(this MauiAppBuilder mauiAppBuilder)
    {
        mauiAppBuilder.Services.AddSingleton<ViewModels.SettingsViewModel>();
        mauiAppBuilder.Services.AddSingleton<ViewModels.SourcesViewModel>();
        mauiAppBuilder.Services.AddSingleton<ViewModels.MetadataViewModel>();
        return mauiAppBuilder;
    }

    public static MauiAppBuilder RegisterViews(this MauiAppBuilder mauiAppBuilder)
    {
        mauiAppBuilder.Services.AddSingleton<Views.SettingsView>();
        mauiAppBuilder.Services.AddSingleton<Views.SourcesView>();
        mauiAppBuilder.Services.AddSingleton<Views.MetadataView>();
        return mauiAppBuilder;
    }
}
