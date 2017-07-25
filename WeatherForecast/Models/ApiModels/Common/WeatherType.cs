using Realms;
using WeatherForecast.Abstractions;

namespace WeatherForecast.Models.ApiModels.Common
{
    public class WeatherType : RealmObject, ICloneable<WeatherType>, IWeatherType
    {
        public WeatherType Clone() => new WeatherType {Volume = Volume};

        public double Volume { get; set; }
    }
}