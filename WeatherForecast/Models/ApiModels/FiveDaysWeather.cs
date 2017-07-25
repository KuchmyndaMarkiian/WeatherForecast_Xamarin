using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Realms;
using WeatherForecast.Abstractions;
using JsonConverter = WeatherForecast.Infrastructure.Abstractions.JsonConverter;

namespace WeatherForecast.Models.ApiModels
{
    public class FiveDaysWeather: RealmObject, ICloneable<FiveDaysWeather>
    {
        [JsonProperty("cod")]
        public string Cod { get; set; }
        [JsonProperty("message")]
        public double Message { get; set; }
        [JsonProperty("cnt")]
        public int Count { get; set; }

        [JsonProperty("list"), Ignored]
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