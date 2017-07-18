using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using WeatherForecast.Infrastructure.Models.ApiModels.Common;

namespace WeatherForecast.Infrastructure.Models
{
    public class City :IComparable<City>
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("country")]
        public string CountryCode { get; set; }
        [JsonProperty("coord")]
        public Coord Coord { get; set; }
        public override string ToString() => $"{Name},{CountryCode}";
        

        public int CompareTo(City other)
        {
            if (ReferenceEquals(this, other)) return 0;
            if (ReferenceEquals(null, other)) return 1;
            var nameComparison = string.Compare(Name, other.Name, StringComparison.Ordinal);
            if (nameComparison != 0) return nameComparison;
            return string.Compare(CountryCode, other.CountryCode, StringComparison.Ordinal);
        }
    }
}