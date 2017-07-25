using System;
using Newtonsoft.Json;
using Realms;
using WeatherForecast.Abstractions;

namespace WeatherForecast.Models.ApiModels
{
    public class Sys : RealmObject, ICloneable<Sys>
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

        public string SunriseHour => new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddMilliseconds(Sunrise * 1000)
            .ToShortTimeString();


        /// <summary>
        /// Sunset time, unix, UTC
        /// </summary>
        [JsonProperty("sunset")]
        public int Sunset { get; set; }

        public string SunsetHour => new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddMilliseconds(Sunset * 1000)
            .ToShortTimeString();

        public Sys Clone() => new Sys
        {
            Id = Id,
            Message = Message,
            Type = Type,
            Country = Country,
            Sunrise = Sunrise,
            Sunset = Sunset
        };
    }
}