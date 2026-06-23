using Microsoft.Maui.Controls;

namespace NoteApp.Pages;

[QueryProperty(nameof(Latitude), "lat")]
[QueryProperty(nameof(Longitude), "lon")]
public partial class MapPage : ContentPage
{
    public double Latitude { get; set; }
    public double Longitude { get; set; }

    public MapPage()
    {
        InitializeComponent();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        string html = $"""
        <!DOCTYPE html>
        <html>
        <head>
            <link rel="stylesheet" href="https://unpkg.com/leaflet@1.9.4/dist/leaflet.css" />
            <script src="https://unpkg.com/leaflet@1.9.4/dist/leaflet.js"></script>
        </head>
        <body style="margin:0; padding:0;">
            <div id="map" style="height: 100vh; width: 100vw;"></div>
            <script>
                var map = L.map('map').setView([{Latitude.ToString(System.Globalization.CultureInfo.InvariantCulture)}, {Longitude.ToString(System.Globalization.CultureInfo.InvariantCulture)}], 13);
                L.tileLayer('https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png').addTo(map);
                L.marker([{Latitude.ToString(System.Globalization.CultureInfo.InvariantCulture)}, {Longitude.ToString(System.Globalization.CultureInfo.InvariantCulture)}]).addTo(map);
            </script>
        </body>
        </html>
        """;

        mapWebView.Source = new HtmlWebViewSource { Html = html };
    }
}