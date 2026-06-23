using NoteApp.Models;
using NoteApp.Services;

namespace NoteApp.Pages;

public partial class MainPage : ContentPage
{
    private readonly DatabaseService _databaseService;

    public MainPage(DatabaseService databaseService)
    {
        InitializeComponent();
        _databaseService = databaseService;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        NotesCollectionView.ItemsSource = _databaseService.GetNotes();
    }

    private async void OnAddClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(AddNotePage));
    }

    private void OnNoteSelected(object sender, SelectionChangedEventArgs e)
    {
        // Логика выбора заметки
    }

    private void OnDeleteNoteClicked(object sender, EventArgs e)
    {
        if (sender is Button button && button.CommandParameter is Note note)
        {
            if (File.Exists(note.PhotoPath)) File.Delete(note.PhotoPath);
            _databaseService.DeleteNote(note);
            NotesCollectionView.ItemsSource = _databaseService.GetNotes();
        }
    }

    private async void OnMapClicked(object sender, EventArgs e)
    {
        if (sender is Button button && button.CommandParameter is Note note)
        {
            await Shell.Current.GoToAsync($"{nameof(MapPage)}?lat={note.Latitude}&lon={note.Longitude}");
        }
    }
}