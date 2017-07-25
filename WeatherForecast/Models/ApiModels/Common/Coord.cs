using Newtonsoft.Json;
using Realms;
using WeatherForecast.Abstractions;

namespace WeatherForecast.Models.ApiModels.Common
{
    public class Coord:RealmObject,ICloneable<Coord>
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

        public Coord Clone() => new Coord {Latitude = Latitude, Longtitude = Longtitude};
    }
}