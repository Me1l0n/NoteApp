using NoteApp.Pages;

namespace NoteApp;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();

        Routing.RegisterRoute(nameof(AddNotePage), typeof(AddNotePage));
        Routing.RegisterRoute(nameof(MapPage), typeof(MapPage));
    }
}