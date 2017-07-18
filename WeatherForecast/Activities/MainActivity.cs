using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using WeatherForecast.Infrastructure;
using WeatherForecast.Infrastructure.Helpers;
using WeatherForecast.Infrastructure.Models;
using WeatherForecast.Infrastructure.Models.ApiModels;
using City = WeatherForecast.Infrastructure.Models.City;

namespace WeatherForecast.Activities
{
    [Activity(Label = "MainActivity")]
    public class MainActivity : Activity
    {
        private City _currentModel;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.mainWindow);

            Intent current = Intent;
            _currentModel = current.GetExtra<City>("city");
            FindViewById<TextView>(Resource.Id.tmpInfo).SetText(_currentModel.ToString(), TextView.BufferType.Normal);
            /*CityModelDetail model = new OpenWeatherProvider()
                .GetData<CityModelDetail>(OpenWeatherProvider.Urls.Current, ("id", _currentModel.Id.ToString())).Result;*/
            /*FiveDaysWeather model = new OpenWeatherProvider()
                .GetData<FiveDaysWeather>(OpenWeatherProvider.Urls.Daily5, ("id", _currentModel.Id.ToString())).Result;*/
        }
    }
}