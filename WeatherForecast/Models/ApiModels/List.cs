using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

using SQLiteNetExtensions.Attributes;
using WeatherForecast.Abstractions;
using WeatherForecast.Models.ApiModels.Common;
using JsonConverter = WeatherForecast.Infrastructure.Helpers.JsonConverter;

namespace WeatherForecast.Models.ApiModels
{
    [Serializable]
    public class List:  ICloneable<List>
    {
        [JsonProperty("dt")]
        public int Datetime { get; set; }
        [JsonProperty("main")]
        public Main Main { get; set; }

        [JsonProperty("weather")]
        public List<Weather> Weather { get;  set; }

        [JsonProperty("clouds")]
        public Clouds Clouds { get; set; }
        [JsonProperty("wind")]
        public Wind Wind { get; set; }
        [JsonProperty("rain")]
        public WeatherType Rain { get; set; }
        [JsonProperty("sys")]
        public Sys Sys { get; set; }
        [JsonProperty("dt_txt")]
        public string DatetimeText { get; set; }

        public List Clone()
        {
            return new List
            {
                Datetime = Datetime,
                DatetimeText = DatetimeText,
                Main = Main.Clone(),
                Clouds = Clouds.Clone(),
                Wind = Wind.Clone(),
                Sys = Sys.Clone(),
                Rain = Rain.Clone(),
                Weather = Weather.Select(x => x.Clone()).ToList()
            };
        }
    }
}