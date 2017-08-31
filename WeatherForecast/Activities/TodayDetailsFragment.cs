using Android.App;
using Android.OS;
using Android.Views;
using Android.Widget;
using WeatherForecast.Abstractions;
using WeatherForecast.Infrastructure.Helpers;
using WeatherForecast.Models;

namespace WeatherForecast.Activities
{
    public class TodayDetailsFragment : Fragment,IFragmentViewModelBase
    {
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            return inflater.Inflate(Resource.Layout.todayDetailsFragment, container, false);
        }

        public void InitializeViewModel()
        {
            var model = JsonConverter.Read<TodayDetailFragmentModel>(Arguments.GetString("todayDetailModel"));
            Activity.FindViewById<TextView>(Resource.Id.cloudinessHeader).Text =
                $"Cloudiness: {model.Clouds.All}%";
            Activity.FindViewById<TextView>(Resource.Id.windSpeedHeader).Text =
                $"Wind speed: {model.Wind.Speed}m/s";
            Activity.FindViewById<TextView>(Resource.Id.windDirectionHeader).Text =
                $"Direction: {model.Wind.Direction()}";
            Activity.FindViewById<TextView>(Resource.Id.humidityHeader).Text =
                $"Humidity: {model.Main.Humidity}%";
            Activity.FindViewById<TextView>(Resource.Id.pressureHeader).Text =
                $"Pressure: {model.Main.Pressure}hPa";
            Activity.FindViewById<TextView>(Resource.Id.sunriseHeader).Text =
                $"Sunrise: {model.Sys.SunriseHour}";
            Activity.FindViewById<TextView>(Resource.Id.sunsetHeader).Text =
                $"Sunset: {model.Sys.SunsetHour}";
        }
    }
}