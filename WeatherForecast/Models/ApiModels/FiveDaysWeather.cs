using System.Collections.Generic;
using Newtonsoft.Json;
using WeatherForecast.Models.ApiModels.Common;

namespace WeatherForecast.Models.ApiModels
{
    public class List
    {
        [JsonProperty("dt")]
        public int Datetime { get; set; }
        [JsonProperty("main")]
        public Main Main { get; set; }
        [JsonProperty("weather")]
        public List<Weather> Weather { get; set; }
        [JsonProperty("clouds")]
        public Clouds Clouds { get; set; }
        [JsonProperty("wind")]
        public Wind Wind { get; set; }
        [JsonProperty("rain")]
        public Rain Rain { get; set; }
        [JsonProperty("sys")]
        public Sys Sys { get; set; }
        [JsonProperty("dt_txt")]
        public string DatetimeText { get; set; }
    }
    

    public class FiveDaysWeather
    {
        [JsonProperty("cod")]
        public string Cod { get; set; }
        [JsonProperty("message")]
        public double Message { get; set; }
        [JsonProperty("cnt")]
        public int Count { get; set; }
        [JsonProperty("list")]
        public List<List> List { get; set; }
        [JsonProperty("city")]
        public City City { get; set; }
    }
}