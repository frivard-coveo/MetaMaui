using CoveoSdk;

namespace MetaMaui;

public static class MauiProgram
{
    private const string PrefIdOrgId = "orgId";
    private const string PrefIdToken = "token";

    public static MauiApp CreateMauiApp()
    {
        //TODO  get default values for the OrgId and ApiKey/Token
        var orgIdFromEnv = Environment.GetEnvironmentVariable("CoveoOrgId");
        if (!string.IsNullOrEmpty(orgIdFromEnv))
        {
            Preferences.Set(PrefIdOrgId, orgIdFromEnv);
        }
        var apiKeyFromEnv = Environment.GetEnvironmentVariable("CoveoApiKey");
        if (!string.IsNullOrEmpty(apiKeyFromEnv))
        {
            Preferences.Set(PrefIdToken, apiKeyFromEnv);
        }

        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });
        builder.Services.AddCoveoSdk(Preferences.Get(PrefIdOrgId, ""), Preferences.Get(PrefIdToken, ""));
        builder.RegisterViewModels();
        builder.RegisterViews();

        return builder.Build();
    }

    public static MauiAppBuilder RegisterViewModels(this MauiAppBuilder mauiAppBuilder)
    {
        mauiAppBuilder.Services.AddSingleton<ViewModels.SourcesViewModel>();
        return mauiAppBuilder;
    }

    public static MauiAppBuilder RegisterViews(this MauiAppBuilder mauiAppBuilder)
    {
        mauiAppBuilder.Services.AddSingleton<Views.SourcesView>();
        return mauiAppBuilder;
    }
}
