using System.Collections.Generic;
using Newtonsoft.Json;
using WeatherForecast.Infrastructure.Models.ApiModels.Common;

namespace WeatherForecast.Infrastructure.Models.ApiModels
{
    public class Sys
    {
        [JsonProperty("type")]
        public int Type { get; set; }
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("message")]
        public double Message { get; set; }
        /// <summary>
        /// Country code (GB, JP etc.)
        /// </summary>
        [JsonProperty("country")]
        public string Country { get; set; }
        /// <summary>
        /// Sunrise time, unix, UTC
        /// </summary>
        [JsonProperty("sunrise")]
        public int Sunrise { get; set; }
        /// <summary>
        /// Sunset time, unix, UTC
        /// </summary>
        [JsonProperty("sunset")]
        public int Sunset { get; set; }
    }

    public class CityCurrrentWeather
    {
        [JsonProperty("coord")]
        public Coord Coord { get; set; }
        [JsonProperty("weather")]
        public List<Weather> Weather { get; set; }
        [JsonProperty("base")]
        public string Base { get; set; }
        [JsonProperty("main")]
        public Main Main { get; set; }
        [JsonProperty("visibility")]
        public int Visibility { get; set; }
        [JsonProperty("wind")]
        public Wind Wind { get; set; }
        [JsonProperty("clouds")]
        public Clouds Clouds { get; set; }
        [JsonProperty("dt")]
        public int Dt { get; set; }
        [JsonProperty("sys")]
        public Sys Sys { get; set; }
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("cod")]
        public int Cod { get; set; }
    }
}