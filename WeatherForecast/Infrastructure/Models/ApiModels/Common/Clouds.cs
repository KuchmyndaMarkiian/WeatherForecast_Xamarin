using Newtonsoft.Json;

namespace WeatherForecast.Infrastructure.Models.ApiModels.Common
{
    public class Clouds
    {
        /// <summary>
        ///  Cloudiness, %
        /// </summary>
        [JsonProperty("all")]
        public double All { get; set; }
    }
}