using System;
using Newtonsoft.Json;

using WeatherForecast.Abstractions;
using WeatherForecast.Models.ApiModels.Common;

namespace WeatherForecast.Models
{
    [Serializable]
    public class City : SqLiteEntityBase, IComparable<City>, ICloneable<City>
    {
        [JsonProperty("id")]
        public int CityId { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("country")]
        public string CountryCode { get; set; }

        [JsonProperty("coord")]
        public Coord Coord { get; set; }

        public City Clone() => new City {CountryCode = CountryCode, CityId = CityId, Name = Name, Coord = Coord.Clone()};

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