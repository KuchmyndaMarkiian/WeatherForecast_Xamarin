using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Realms;
using WeatherForecast.Abstractions;
using WeatherForecast.Infrastructure;
using WeatherForecast.Infrastructure.Helpers;
using WeatherForecast.Models;
using WeatherForecast.Models.ApiModels;

namespace WeatherForecast.Activities
{
    [Activity(Label = "MainActivity", Theme = "@style/NoActionBar")]
    public class MainActivity : Activity
    {
        #region Fields

        private Dialog _progressDialog;
        private readonly CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();
        private MainModel _model = new MainModel();

        #endregion

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.mainWindow);

            _progressDialog = new Dialog(this);
            _progressDialog.InitializeLoadingDialog();
            var tran = FragmentManager.BeginTransaction();
            tran.Add(Resource.Id.todayFragmentContainer, new TodayFragment {Arguments = new Bundle()},
                nameof(TodayFragment));
            tran.Add(Resource.Id.todayDetailFragmentContainer, new TodayDetailsFragment {Arguments = new Bundle()},
                nameof(TodayDetailsFragment));
            tran.Add(Resource.Id.graphFragmentContainer, new GraphDailyFragment {Arguments = new Bundle()},
                nameof(GraphDailyFragment));
            tran.Commit();
            Intent current = Intent;
            _model = current.GetExtra<MainModel>("city");
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
                    List<Fragment> fragments = new List<Fragment>(3);

                    #region Current Fragment

                    var todayFragment = FragmentManager.FindFragmentByTag(nameof(TodayFragment));
                    Bundle bundle = new Bundle();
                    bundle.PutString("todayLessModel", JsonConverter.Convert(new TodayFragmentModel
                    {
                        City = $"{_model.CurrentModel.Name},{_model.CurrentModel.CountryCode}",
                        Temperature = _model.CurrentDayWeather.Main.Temp,
                        MinTemperature = _model.CurrentDayWeather.Main.TempMin,
                        MaxTemperature = _model.CurrentDayWeather.Main.TempMax
                    }));
                    todayFragment.Arguments.PutAll(bundle);
                    fragments.Add(todayFragment);

                    #endregion

                    #region Detail Fragment

                    var todayDetailFragment = FragmentManager.FindFragmentByTag(nameof(TodayDetailsFragment));
                    bundle = new Bundle();
                    bundle.PutString("todayDetailModel", JsonConverter.Convert(new TodayDetailFragmentModel
                    {
                        Main = _model.CurrentDayWeather.Main,
                        Clouds = _model.CurrentDayWeather.Clouds,
                        Sys = _model.CurrentDayWeather.Sys,
                        Wind = _model.CurrentDayWeather.Wind
                    }));
                    todayDetailFragment.Arguments.PutAll(bundle);
                    fragments.Add(todayDetailFragment);

                    #endregion

                    #region Graph Fragment

                    var graphFragment = FragmentManager.FindFragmentByTag(nameof(GraphDailyFragment));
                    bundle = new Bundle();
                    bundle.PutString("graphPoints",
                        JsonConverter.Convert(
                            _model.FiveDaysWeather.List
                                .Select(x => (date:x.DatetimeText.Replace(' ', '\t'), temperature: x.Main.Temp))
                                .ToList()));
                    graphFragment.Arguments.PutAll(bundle);
                    fragments.Add(graphFragment);

                    #endregion

                    fragments.ForEach(fragment =>
                    {
                        if (fragment is IFragmentViewModelBase modelBase)
                        {
                            modelBase.InitializeViewModel();
                        }
                    });

                    using (IMemoryManipulator manipulator = new RealmManager(Realm.GetInstance()))
                    {
                        if (manipulator.IsExists<MainModel>())
                        {
                            manipulator.Clear<MainModel>();
                        }
                        manipulator.Write(_model);
                    }

                    _progressDialog.Dismiss();

                });
            });
            Task.Run(() =>
            {
                using (IMemoryManipulator manipulator = new RealmManager(Realm.GetInstance()))
                {
                    if (!manipulator.IsExists<MainModel>())
                    {
                        UpdateData();
                    }
                    else
                    {
                        _model = manipulator.Read<MainModel>(null).FirstOrDefault()?.Clone();
                        if (_model == null)
                        {
                            UpdateData();
                        }
                    }
                }
                _cancellationTokenSource.Cancel();
            });
        }

        private void UpdateData()
        {
            Task.WaitAll(Task.Run(() => _model.CurrentDayWeather = new OpenWeatherProvider()
                .GetData<CityCurrrentWeather>(OpenWeatherProvider.UrlParameters.Current,
                    ("id", _model.CurrentModel.Id.ToString()), OpenWeatherProvider.UrlParameters.Metric)
                .Result), Task.Run(() => _model.FiveDaysWeather = new OpenWeatherProvider()
                .GetData<FiveDaysWeather>(OpenWeatherProvider.UrlParameters.Daily5,
                    ("id", _model.CurrentModel.Id.ToString()), OpenWeatherProvider.UrlParameters.Metric)
                .Result));
        }
    }
}
