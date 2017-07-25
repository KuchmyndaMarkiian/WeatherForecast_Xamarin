using Realms;
using WeatherForecast.Abstractions;
using WeatherForecast.Models.ApiModels;

namespace WeatherForecast.Models
{
    internal class MainModel:RealmObject,ICloneable<MainModel>
    {
        public City CurrentModel { get; set; }=new City();
        public CityCurrrentWeather CurrentDayWeather { get; set; }=new CityCurrrentWeather();
        public FiveDaysWeather FiveDaysWeather { get; set; }=new FiveDaysWeather();

        public MainModel Clone()=>new MainModel
        {
            CurrentModel = CurrentModel.Clone(),
            CurrentDayWeather = CurrentDayWeather.Clone(),
            FiveDaysWeather = FiveDaysWeather.Clone()
        };
    }
}