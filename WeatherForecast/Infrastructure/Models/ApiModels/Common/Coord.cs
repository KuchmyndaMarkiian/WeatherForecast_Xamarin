using Newtonsoft.Json;

namespace WeatherForecast.Infrastructure.Models.ApiModels.Common
{
    public class Coord
    {
        /// <summary>
        /// City geo location, longitude
        /// </summary>
        [JsonProperty("lon")]
        public double Longtitude { get; set; }
        /// <summary>
        /// City geo location, latitude
        /// </summary>
        [JsonProperty("lat")]
        public double Latitude { get; set; }
    }
}