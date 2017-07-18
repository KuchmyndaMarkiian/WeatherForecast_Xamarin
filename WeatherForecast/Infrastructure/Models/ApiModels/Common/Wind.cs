using Newtonsoft.Json;

namespace WeatherForecast.Infrastructure.Models.ApiModels.Common
{
    public class Wind
    {
        /// <summary>
        /// Wind speed. Unit Default: meter/sec, Metric: meter/sec, Imperial: miles/hour.
        /// </summary>
        [JsonProperty("speed")]
        public double Speed { get; set; }
        /// <summary>
        /// Wind direction, degrees (meteorological)
        /// </summary>
        [JsonProperty("deg")]
        public double Degree { get; set; }
    }
}