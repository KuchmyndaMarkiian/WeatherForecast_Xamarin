using Newtonsoft.Json;

namespace WeatherForecast.Infrastructure.Models.ApiModels.Common
{
    interface IWeatherType
    {
        /// <summary>
        /// ____ volume for last 3 hours, mm
        /// </summary>
        [JsonProperty("3h")]
        double Volume { get; set; }
    }
}