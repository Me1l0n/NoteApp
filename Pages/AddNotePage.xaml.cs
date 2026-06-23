using NoteApp.Models;
using NoteApp.Services;

namespace NoteApp.Pages;

public partial class AddNotePage : ContentPage
{
    private readonly DatabaseService _databaseService;
    private readonly LocationService _locationService;
    private string _photoPath;

    public AddNotePage(DatabaseService databaseService, LocationService locationService)
    {
        InitializeComponent();
        _databaseService = databaseService;
        _locationService = locationService;
    }

    private async void OnTakePhotoClicked(object sender, EventArgs e)
    {
        var photo = await MediaPicker.Default.CapturePhotoAsync();
        await LoadPhotoAsync(photo);
    }

    private async void OnPickPhotoClicked(object sender, EventArgs e)
    {
        var photo = await MediaPicker.Default.PickPhotoAsync();
        await LoadPhotoAsync(photo);
    }

    private async Task LoadPhotoAsync(FileResult photo)
    {
        if (photo == null) return;
        var newFile = Path.Combine(FileSystem.AppDataDirectory, Guid.NewGuid().ToString() + ".jpg");
        using var stream = await photo.OpenReadAsync();
        using var newStream = File.OpenWrite(newFile);
        await stream.CopyToAsync(newStream);
        _photoPath = newFile;
        NoteImage.Source = ImageSource.FromFile(_photoPath);
    }

    private async void OnSaveClicked(object sender, EventArgs e)
    {
        var location = await _locationService.GetCurrentLocationAsync();
        var note = new Note
        {
            Title = TitleEntry.Text,
            PhotoPath = _photoPath,
            Latitude = location?.Latitude ?? 0,
            Longitude = location?.Longitude ?? 0,
            CreatedAt = DateTime.Now
        };

        _databaseService.AddNote(note);
        await Shell.Current.GoToAsync("..");
    }
}