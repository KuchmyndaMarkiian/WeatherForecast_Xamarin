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
using WeatherForecast.Infrastructure.Helpers;
using WeatherForecast.Infrastructure.Models;

namespace WeatherForecast.Views.Activities
{
    [Activity(Label = "Weather Forecast", MainLauncher = true, Theme = "@style/NoActionBar")]
    public class LoadingActivity : Activity
    {
        private Dialog _progressDialog;
        private LinearLayout _searchBox;
        private AutoCompleteTextView _autoCompleteTextView;

        private readonly CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();
        private List<City> _cities;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.loading);
            _searchBox = FindViewById<LinearLayout>(Resource.Id.searchBox);
            _autoCompleteTextView = FindViewById<AutoCompleteTextView>(Resource.Id.autocompleteView);
            _progressDialog=new Dialog(this);
            _progressDialog.InitializeLoadingDialog();
            FindViewById<Button>(Resource.Id.loadingNextButton).Click += (sender, args) =>
            {
                var typed = _autoCompleteTextView.Text;
                City founded = _cities.First(x => $"{x.Name},{x.CountryCode}".Equals(typed));
                if (founded == null) return;
                Intent intent = new Intent(this, typeof(MainActivity));
                intent.PutExtra("city", founded);
                Finish();
                StartActivity(intent);
            };
            _autoCompleteTextView.Text = "Lviv,UA";
            _autoCompleteTextView.AfterTextChanged += (sender, args) =>
            {
                if (sender is AutoCompleteTextView view)
                {
                    string typed = view.Text;
                    //TODO: Need store in memory.
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
            _progressDialog.Show();
            Task.Run(() =>
            {
                while (!_cancellationTokenSource.Token.IsCancellationRequested) ;
                RunOnUiThread(() =>
                {
                    _progressDialog.Dismiss();
                    _searchBox.Visibility = ViewStates.Visible;
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