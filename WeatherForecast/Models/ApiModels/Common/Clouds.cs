using Newtonsoft.Json;

namespace WeatherForecast.Models.ApiModels.Common
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