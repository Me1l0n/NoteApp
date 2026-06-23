using NoteApp.Services;
using NoteApp.Pages;

namespace NoteApp;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>();

        builder.Services.AddSingleton<DatabaseService>();
        builder.Services.AddSingleton<LocationService>();

        builder.Services.AddTransient<MainPage>();
        builder.Services.AddTransient<AddNotePage>();
        builder.Services.AddTransient<MapPage>();

        return builder.Build();
    }
}