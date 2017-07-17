using System;

namespace WeatherForecast.Infrastructure.Models
{
    internal class CityModel :IComparable<CityModel>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string CountryCode { get; set; }
        public override string ToString() => $"{Id} {Name},{CountryCode}";
        

        public int CompareTo(CityModel other)
        {
            if (ReferenceEquals(this, other)) return 0;
            if (ReferenceEquals(null, other)) return 1;
            var nameComparison = string.Compare(Name, other.Name, StringComparison.Ordinal);
            if (nameComparison != 0) return nameComparison;
            return string.Compare(CountryCode, other.CountryCode, StringComparison.Ordinal);
        }
    }
}