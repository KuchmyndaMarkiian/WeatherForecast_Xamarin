using System;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Support.V7.App;
using Android.Widget;
using WeatherForecast.Infrastructure;
using WeatherForecast.Infrastructure.Helpers;
using WeatherForecast.Infrastructure.Models.ApiModels;
using WeatherForecast.Views.Fragments;
using City = WeatherForecast.Infrastructure.Models.City;

namespace WeatherForecast.Views.Activities
{
    [Activity(Label = "MainActivity",Theme = "@style/NoActionBar")]
    public class MainActivity : Activity
    {
        private Dialog _progressDialog;
        private City _currentModel;
        private readonly CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();

        private CityCurrrentWeather _currentDayWeather;
        private FiveDaysWeather _fiveDaysWeather;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.mainWindow);

            _progressDialog = new Dialog(this);
            _progressDialog.InitializeLoadingDialog();
            var tran = FragmentManager.BeginTransaction();
            tran.Add(Resource.Id.todayFragmentContainer, new TodayFragment(), nameof(TodayFragment));
            tran.Add(Resource.Id.todayDetailFragmentContainer, new TodayDetailsFragment(),
                nameof(TodayDetailsFragment));
            tran.Add(Resource.Id.graphFragmentContainer, new GraphDailyFragment(), nameof(GraphDailyFragment));
            tran.Commit();
            Intent current = Intent;
            _currentModel = current.GetExtra<City>("city");
        }

        protected override void OnResume()
        {
            base.OnResume();
            _progressDialog.Show();
            Task.Run(() =>
            {
                while (!_cancellationTokenSource.Token.IsCancellationRequested) ;
                RunOnUiThread(() =>
                {
                    _progressDialog.Dismiss();
                    RunOnUiThread(() =>
                    {
                        #region Current Fragment

                        var todayFragment = FragmentManager.FindFragmentByTag(nameof(TodayFragment));
                        todayFragment.Activity.FindViewById<TextView>(Resource.Id.cityHeader)
                            .Text = _currentModel.ToString();
                        todayFragment.Activity.FindViewById<TextView>(Resource.Id.currentTemperture)
                            .Text = Math.Round(_currentDayWeather.Main.Temp).ToString(CultureInfo.InvariantCulture);
                        todayFragment.Activity.FindViewById<TextView>(Resource.Id.rangeTemperture)
                                .Text =
                            $"{Math.Round(_currentDayWeather.Main.TempMin)}...{Math.Round(_currentDayWeather.Main.TempMax)}";

                        #endregion

                        #region Detail Fragment

                        var todayDetailFragment = FragmentManager.FindFragmentByTag(nameof(TodayDetailsFragment));
                        todayDetailFragment.Activity.FindViewById<TextView>(Resource.Id.cloudinessHeader).Text =
                            $"Cloudiness: {_currentDayWeather.Clouds.All}%";
                        todayDetailFragment.Activity.FindViewById<TextView>(Resource.Id.windSpeedHeader).Text =
                            $"Wind speed: {_currentDayWeather.Wind.Speed}m/s";
                        todayDetailFragment.Activity.FindViewById<TextView>(Resource.Id.windDirectionHeader).Text =
                            $"Direction: {_currentDayWeather.Wind.Direction}";
                        todayDetailFragment.Activity.FindViewById<TextView>(Resource.Id.humidityHeader).Text =
                            $"Humidity: {_currentDayWeather.Main.Humidity}%";
                        todayDetailFragment.Activity.FindViewById<TextView>(Resource.Id.pressureHeader).Text =
                            $"Pressure: {_currentDayWeather.Main.Pressure}hPa";
                        todayDetailFragment.Activity.FindViewById<TextView>(Resource.Id.sunriseHeader).Text =
                            $"Sunrise: {_currentDayWeather.Sys.SunriseHour}";
                        todayDetailFragment.Activity.FindViewById<TextView>(Resource.Id.sunsetHeader).Text =
                            $"Sunset: {_currentDayWeather.Sys.SunsetHour}";

                        #endregion

                        #region Graph Fragment

                        var graphFragment = FragmentManager.FindFragmentByTag(nameof(GraphDailyFragment));
                        if (graphFragment is GraphDailyFragment fragment)
                        {
                            fragment.InitializeFragment(_fiveDaysWeather.List.Select(x=> (x.DatetimeText.Replace(' ','\t'), x.Main.Temp)).ToList());
                        }

                        #endregion

                        _progressDialog.Dismiss();
                    });
                });
            });

            Task.Run(() =>
            {
                Task.WaitAll(Task.Run(() => _currentDayWeather = new OpenWeatherProvider()
                    .GetData<CityCurrrentWeather>(OpenWeatherProvider.UrlParameters.Current,
                        ("id", _currentModel.Id.ToString()), OpenWeatherProvider.UrlParameters.Metric)
                    .Result), Task.Run(() => _fiveDaysWeather = new OpenWeatherProvider()
                    .GetData<FiveDaysWeather>(OpenWeatherProvider.UrlParameters.Daily5,
                        ("id", _currentModel.Id.ToString()), OpenWeatherProvider.UrlParameters.Metric)
                    .Result));
                _cancellationTokenSource.Cancel();
            });
        }
    }
}