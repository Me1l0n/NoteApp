using Microsoft.Maui.Devices.Sensors;

namespace NoteApp.Services;

public class LocationService
{
    public async Task<Location> GetCurrentLocationAsync()
    {
        try
        {
            var request = new GeolocationRequest(GeolocationAccuracy.Medium, TimeSpan.FromSeconds(10));
            var location = await Geolocation.Default.GetLocationAsync(request);

            if (location == null)
            {
                return await Geolocation.Default.GetLastKnownLocationAsync();
            }

            return location;
        }
        catch (FeatureNotSupportedException)
        {
            return null;
        }
        catch (PermissionException)
        {
            return null;
        }
        catch
        {
            return await Geolocation.Default.GetLastKnownLocationAsync();
        }
    }
}