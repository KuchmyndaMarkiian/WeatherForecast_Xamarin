﻿using Android.App;
using Android.OS;
using Android.Views;

namespace WeatherForecast.Views.Fragments
{
    public class TodayFragment : Fragment
    {
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
             return inflater.Inflate(Resource.Layout.todayFragment, container, false);
        }
    }
}