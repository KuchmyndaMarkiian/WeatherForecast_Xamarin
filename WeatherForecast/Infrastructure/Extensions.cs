using System;
using Android.Content;
using WeatherForecast.Infrastructure.Abstractions;

namespace WeatherForecast.Infrastructure
{
    public static class Extensions
    {
        public static Intent PutExtra<T>(this Intent intent, string key, T _object) => intent.PutExtra(key,
            JsonConverter.Convert(_object));

        public static T GetExtra<T>(this Intent intent, string key) =>
            JsonConverter.Read<T>(intent.GetStringExtra(key));
    }
}