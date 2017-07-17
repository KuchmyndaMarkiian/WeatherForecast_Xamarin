using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using WeatherForecast.Infrastructure;
using WeatherForecast.Infrastructure.Abstractions;
using WeatherForecast.Infrastructure.Models;

namespace WeatherForecast
{
    [Activity(Label = "MainActivity")]
    public class MainActivity : Activity
    {
        private CityModel _currentModel;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.mainWindow);

            Intent current = Intent;
            _currentModel = current.GetExtra<CityModel>("city");
            FindViewById<TextView>(Resource.Id.tmpInfo).SetText(_currentModel.ToString(),TextView.BufferType.Normal);
        }
    }
}