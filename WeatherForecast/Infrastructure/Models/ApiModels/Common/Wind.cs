using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace WeatherForecast.Infrastructure.Models.ApiModels.Common
{
    public class Wind
    {
        /// <summary>
        /// Wind speed. Unit Default: meter/sec, Metric: meter/sec, Imperial: miles/hour.
        /// </summary>
        [JsonProperty("speed")]
        public double Speed { get; set; }

        /// <summary>
        /// Wind direction, degrees (meteorological)
        /// </summary>
        [JsonProperty("deg")]
        public double Degree { get; set; }

        public string Direction => _directions.First(x => x.Key(this)).Value;

        private readonly Dictionary<Predicate<Wind>, string> _directions = new Dictionary<Predicate<Wind>, string>()
        {
            {(wind => wind.Degree >= 0 && wind.Degree < 45), "W"},
            {(wind => wind.Degree >= 45 && wind.Degree < 90), "NW"},
            {(wind => wind.Degree >= 90 && wind.Degree < 135), "N"},
            {(wind => wind.Degree >= 135 && wind.Degree < 180), "NE"},
            {(wind => wind.Degree >= 180 && wind.Degree < 225), "E"},
            {(wind => wind.Degree >= 225 && wind.Degree < 270), "SE"},
            {(wind => wind.Degree >= 270 && wind.Degree < 315), "S"},
            {(wind => wind.Degree >= 315 && wind.Degree < 45), "SW"},
        };
    }
}