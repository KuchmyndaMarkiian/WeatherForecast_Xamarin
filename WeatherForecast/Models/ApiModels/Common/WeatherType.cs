using System;
using WeatherForecast.Abstractions;

namespace WeatherForecast.Models.ApiModels.Common
{
    [Serializable]
    public class WeatherType : SqLiteEntityBase, ICloneable<WeatherType>, IWeatherType
    {
        public WeatherType Clone() => new WeatherType {Volume = Volume};

        public double Volume { get; set; }
    }
}