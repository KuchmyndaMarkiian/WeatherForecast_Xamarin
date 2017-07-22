using System;
using System.Collections.Generic;
using System.Linq;
using Android.App;
using Android.OS;
using Android.Views;
using Android.Widget;
using MikePhil.Charting.Charts;
using MikePhil.Charting.Components;
using MikePhil.Charting.Data;
using MikePhil.Charting.Formatter;

namespace WeatherForecast.Activities
{
    public class GraphDailyFragment : Fragment
    {
        class AxisValueFormatter : IndexAxisValueFormatter
        {
            private readonly string[] _headers;

            internal AxisValueFormatter(params string[] headers) : base(headers)
            {
                _headers = headers;
            }

            public override string GetFormattedValue(float p0, AxisBase p1)
            {
                return _headers[(int) p0];
            }
        }

        public void InitializeFragment(List<(string date, double temperature)> points)
        {
            var activity = this.Activity;
            float i = 0f;
            var dataList = new List<Entry>();
            points.ForEach(tuple => dataList.Add(new Entry(i++, (float) tuple.temperature)));
            var dataSet = new LineDataSet(dataList, "Set");
            dataSet.SetDrawFilled(true);
            dataSet.FillColor=Resource.Color.primaryLight;
            dataSet.FillAlpha = 255;
            dataSet.SetDrawFilled(true);
            dataSet.CubicIntensity = 0.5f;
            dataSet.CircleRadius = 0f;
            dataSet.Label = "";
            dataSet.ValueTextSize = 36f;
            var chart = new LineChart(activity)
            {
                Data = new LineData(dataSet),
                LayoutParameters = new ViewGroup.LayoutParams(ViewGroup.LayoutParams.MatchParent,
                    ViewGroup.LayoutParams.MatchParent),
                Description = new Description() {Text = ""}
            };
            chart.AnimateXY(500, 500);

            #region Axis

            var xAxis = chart.XAxis;
            xAxis.Granularity = 0.5F;
            int year = DateTime.Now.Year;
            xAxis.SetAvoidFirstLastClipping(true);
            xAxis.SetDrawGridLines(false);
            xAxis.SetDrawAxisLine(false);
            xAxis.MEntries = points.Select(x => (float)x.temperature).ToList();
            xAxis.ValueFormatter = new AxisValueFormatter(points
                .Select(x => x.date.Replace("00:00", "00").Replace($"{year}-", ""))
                .ToArray());
            xAxis.Position = XAxis.XAxisPosition.Bottom;
            xAxis.GranularityEnabled = true;
            xAxis.SetDrawLabels(true);
            xAxis.LabelCount = points.Count;

            var yAxis=chart.AxisLeft;
            yAxis.SetDrawGridLines(false);
            yAxis.SetDrawAxisLine(false);
            yAxis.SetDrawLabels(false);
            yAxis = chart.AxisRight;
            yAxis.SetDrawGridLines(false);
            yAxis.SetDrawAxisLine(false);
            yAxis.SetDrawLabels(false);

            #endregion
            chart.Description=new Description();
            chart.Description.Text = "";
            chart.Legend.Enabled = false;
            activity.FindViewById<RelativeLayout>(Resource.Id.graphView).AddView(chart);
            chart.Invalidate();
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            return inflater.Inflate(Resource.Layout.graphDailyFragment, container, false);
        }
    }
}