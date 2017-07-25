using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Realms;
using WeatherForecast.Abstractions;
using WeatherForecast.Models.ApiModels.Common;

namespace WeatherForecast.Models.ApiModels
{
    public class CityCurrrentWeather: RealmObject, ICloneable<CityCurrrentWeather>
    {
        private IList<Weather> _weather;

        [JsonProperty("coord")]
        public Coord Coord { get; set; }

        [JsonProperty("weather")]
        public IList<Weather> Weather => _weather;

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

        public CityCurrrentWeather Clone() => new CityCurrrentWeather {_weather = _weather.Select(x => x.Clone()).ToList(), Main = Main.Clone(),Clouds = Clouds.Clone(),Id = Id,Base = Base,Name = Name};
    }
}