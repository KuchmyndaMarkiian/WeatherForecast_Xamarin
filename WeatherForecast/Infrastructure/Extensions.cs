using Android.Content;
using WeatherForecast.Infrastructure.Helpers;

namespace WeatherForecast.Infrastructure
{
    /// <summary>
    /// For expangong existing objects
    /// </summary>
    public static class Extensions
    {
        public static Intent PutExtra<T>(this Intent intent, string key, T _object) => intent.PutExtra(key,
            JsonConverter.Convert(_object));

        public static T GetExtra<T>(this Intent intent, string key) =>
            JsonConverter.Read<T>(intent.GetStringExtra(key));
    }
}