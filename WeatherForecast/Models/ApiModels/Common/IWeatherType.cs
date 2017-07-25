using Newtonsoft.Json;
using WeatherForecast.Abstractions;

namespace WeatherForecast.Models.ApiModels.Common
{
    public interface IWeatherType
    {
        /// <summary>
        /// ____ volume for last 3 hours, mm
        /// </summary>
        [JsonProperty("3h")]
        double Volume { get; set; }
    }
}