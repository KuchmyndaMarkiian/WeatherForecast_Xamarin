using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

using WeatherForecast.Abstractions;
using WeatherForecast.Models.ApiModels.Common;
using JsonConverter = WeatherForecast.Infrastructure.Helpers.JsonConverter;

namespace WeatherForecast.Models.ApiModels
{
    [Serializable]
    public class FiveDaysWeather: SqLiteEntityBase, ICloneable<FiveDaysWeather>
    {
        [JsonProperty("cod")]
        public string Cod { get; set; }
        [JsonProperty("message")]
        public double Message { get; set; }
        [JsonProperty("cnt")]
        public int Count { get; set; }

        [JsonProperty("list")]
        public List<List> List { get; set; }

        public string ListJson
        {
            get => JsonConverter.Convert(List);
            set => List = JsonConverter.Read<List<List>>(value);
        }

        [JsonProperty("city")]
        public City City { get; set; }

        public FiveDaysWeather Clone()
        {
            return new FiveDaysWeather
            {
                Cod = Cod,
                Count = Count,
                Message = Message,
                City = City.Clone(),
                List = List.Select(x => x.Clone()).ToList()
            };
        }
    }
}