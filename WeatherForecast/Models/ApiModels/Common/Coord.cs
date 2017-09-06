using System;
using Newtonsoft.Json;
using WeatherForecast.Abstractions;

namespace WeatherForecast.Models.ApiModels.Common
{
    [Serializable]
    public class Coord:SqLiteEntityBase,ICloneable<Coord>
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