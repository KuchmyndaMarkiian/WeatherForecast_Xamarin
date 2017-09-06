using System;
using System.Globalization;
using Android.App;  
using Android.OS;
using Android.Views;
using Android.Widget;
// ReSharper disable once RedundantUsingDirective
using WeatherForecast.Abstractions;
using WeatherForecast.Infrastructure.Helpers;
using WeatherForecast.Models;

namespace WeatherForecast.Activities
{
    public class TodayFragment : Fragment,IFragmentViewModelBase
    {
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            return inflater.Inflate(Resource.Layout.todayFragment, container, false);
        }

        public void InitializeViewModel()
        {
            var model = JsonConverter.Read<TodayFragmentModel>(Arguments.GetString("todayLessModel"));
            Activity.FindViewById<TextView>(Resource.Id.cityHeader)
                .Text = $"{model.City}";
            Activity.FindViewById<TextView>(Resource.Id.currentTemperture)
                .Text = Math.Round(model.MinTemperature).ToString(CultureInfo.InvariantCulture);
            Activity.FindViewById<TextView>(Resource.Id.rangeTemperture)
                    .Text =
                $"{Math.Round(model.MinTemperature)}...{Math.Round(model.MaxTemperature)}";
            Activity.FindViewById<ImageView>(Resource.Id.weatherIcon).SetImageResource(model.Icon);
        }
    }
}