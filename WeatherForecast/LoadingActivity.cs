using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Views;
using Android.Widget;
using WeatherForecast.Infrastructure;
using WeatherForecast.Infrastructure.Models;

namespace WeatherForecast
{
    [Activity(Label = "Weather Forecast", MainLauncher = true)]
    public class LoadingActivity : Activity
    {
        private ProgressBar _progressDialog;
        private LinearLayout _searchBox;
        private AutoCompleteTextView _autoCompleteTextView;

        private readonly CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();
        private List<CityModel> _cities;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.loading);
            _searchBox = FindViewById<LinearLayout>(Resource.Id.searchBox);
            _autoCompleteTextView = FindViewById<AutoCompleteTextView>(Resource.Id.autocompleteView);
            _progressDialog = FindViewById<ProgressBar>(Resource.Id.loadingProgressBar);
            FindViewById<Button>(Resource.Id.loadingNextButton).Click += (sender, args) =>
            {
                var typed = _autoCompleteTextView.Text;
                CityModel founded = _cities.First(x => $"{x.Name},{x.CountryCode}".Equals(typed));
                if (founded == null) return;
                Intent intent = new Intent(this, typeof(MainActivity));
                intent.PutExtra("city", founded);
                Finish();
                StartActivity(intent);
            };

            _progressDialog.Indeterminate = true;
            _progressDialog.SetProgress(0, true);
            _progressDialog.Max = 100;
            _progressDialog.Visibility = ViewStates.Visible;
            _autoCompleteTextView.TextChanged += (sender, args) =>
            {
                if (sender is AutoCompleteTextView view)
                {
                    string typed = view.Text;
                    view.Adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleDropDownItem1Line,
                        _cities.AsParallel()
                            .Where(w => w.Name.ToLowerInvariant().Contains(typed.ToLowerInvariant()))
                            .Select(x => $"{x.Name},{x.CountryCode}")
                            .ToArray());
                }
            };
        }

        protected override void OnResume()
        {
            base.OnResume();
            Task.Run(() =>
            {
                int i = 0;
                while (!_cancellationTokenSource.Token.IsCancellationRequested)
                {
                    _progressDialog.SetProgress(i++ % 100, true);
                }
                RunOnUiThread(() =>
                {
                    _searchBox.Visibility = ViewStates.Visible;
                    _progressDialog.Visibility = ViewStates.Invisible;
                });
            });

            Task.Run(async () =>
            {
                using (DataDownloader downloader = new DataDownloader())
                {
                    _cities = await downloader.DownloadCities();
                    _cancellationTokenSource.Cancel();
                }
            });
        }
    }
}