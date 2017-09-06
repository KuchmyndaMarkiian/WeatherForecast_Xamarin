using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.Content.Res;
using Android.OS;
using Android.Views;
using Android.Widget;

using WeatherForecast.Abstractions;
using WeatherForecast.Infrastructure;
using WeatherForecast.Infrastructure.Helpers;
using WeatherForecast.Models;
using WeatherForecast.Models.ApiModels;
using WeatherForecast.Models.ApiModels.Common;

namespace WeatherForecast.Activities
{
    [Activity(Label = "Weather Forecast", MainLauncher = true, Theme = "@style/NoActionBar")]
    public class LoadingActivity : Activity
    {
        #region Fields
        
        private LinearLayout _searchBox;
        private AutoCompleteTextView _autoCompleteTextView;
        private readonly CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();
        private List<City> _cities;

        #endregion

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.loading);
            _searchBox = FindViewById<LinearLayout>(Resource.Id.searchBox);
            _autoCompleteTextView = FindViewById<AutoCompleteTextView>(Resource.Id.autocompleteView);
       
            
            FindViewById<Button>(Resource.Id.loadingNextButton).Click += (sender, args) =>
            {
                var typed = _autoCompleteTextView.Text;
                if (!string.IsNullOrEmpty(typed))
                {
                    City founded = _cities.First(x => $"{x.Name},{x.CountryCode}".Equals(typed));
                    if (founded == null) return;
                    PassModelAndGo(new MainModel {CurrentModel = founded.Clone()});
                }
                else
                {
                    Toast.MakeText(this, "Empty field)", ToastLength.Short).Show();
                }
            };
        }

        private void PassModelAndGo(MainModel model)
        {
            Intent intent = new Intent(this, typeof(MainActivity));
            intent.PutExtra("city", model);
            Finish();
            StartActivity(intent);
        }

        protected override void OnResume()
        {
            base.OnResume();
            var progressDialog = new Dialog(this);
            progressDialog.InitializeLoadingDialog();
            progressDialog.Show();
            Task.Run(() =>
            {
                var  deserialized=DeviceSerialization.Deserialize<MainModel>(Global.GetWeatherFileName());
                if (deserialized != null)
                {
                    PassModelAndGo(deserialized);
                }
               
                while (!_cancellationTokenSource.Token.IsCancellationRequested) ;
                RunOnUiThread(() =>
                {
                    progressDialog.Dismiss();
                    _searchBox.Visibility = ViewStates.Visible;
                    _autoCompleteTextView.Adapter = new ArrayAdapter<string>(this,
                        Android.Resource.Layout.SimpleDropDownItem1Line,
                        _cities.AsParallel().Select(x => $"{x.Name},{x.CountryCode}").ToArray());
                });
            });

            Task.Run(async () =>
            {
                var stream = Assets.Open("CityList.txt", Access.Random);
                //Downloads data from the embedded asset
                _cities = await new DataDownloader().DownloadCities(stream);
                _cancellationTokenSource.Cancel();
            });
        }

        protected override void OnDestroy()
        {
            _cancellationTokenSource.Cancel();
            _cancellationTokenSource.Dispose();
            base.OnDestroy();
        }
    }
}