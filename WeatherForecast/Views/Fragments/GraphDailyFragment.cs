using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using Com.Telerik.Widget.Chart.Visualization.CartesianChart;
using Com.Telerik.Widget.Chart.Visualization.CartesianChart.Axes;
using Com.Telerik.Widget.Chart.Visualization.CartesianChart.Series.Categorical;
using Java.Util;
using WeatherForecast.Infrastructure.Models.GraphBindingModels;

namespace WeatherForecast.Views.Fragments
{
    public class GraphDailyFragment : Fragment
    {
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);


            // Create your fragment here
        }

        public void InitializeFragment(List<(string, double)> points)
        {
            ArrayList dataList = new ArrayList(points.Count);
            var activity = this.Activity;
            RadCartesianChartView chartView = new RadCartesianChartView(activity);
            points.ForEach(tuple => dataList.Add(new HourResult {DateTime = tuple.Item1, Temperature = tuple.Item2}));

            SplineAreaSeries series = new SplineAreaSeries
            {
                CategoryBinding = new HourResultDataBinding {PropertyName = nameof(HourResult.DateTime)},
                ValueBinding = new HourResultDataBinding {PropertyName = nameof(HourResult.Temperature)},
                Data = dataList,
                StrokeColor = Resource.Color.primaryDarkBlue,
                FillColor = Resource.Color.primaryBlue
            };
            chartView.Series.Add(series);
            chartView.HorizontalAxis=new CategoricalAxis();
            chartView.VerticalAxis=new LinearAxis();
            activity.FindViewById<ScrollView>(Resource.Id.graphView).AddView(chartView);
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
             return inflater.Inflate(Resource.Layout.graphDailyFragment, container, false);
        }
    }
}