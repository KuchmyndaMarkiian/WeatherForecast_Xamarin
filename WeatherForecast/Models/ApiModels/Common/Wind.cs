using System;
using Newtonsoft.Json;
using WeatherForecast.Abstractions;

namespace WeatherForecast.Models.ApiModels.Common
{
    [Serializable]
    public class Wind : SqLiteEntityBase, ICloneable<Wind>
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

        public string Direction()
        {
            if (Degree >= 0 && Degree < 45)
                return "W";
            if (Degree >= 45 && Degree < 90)
                return "NW";
            if (Degree >= 90 && Degree < 135)
                return "N";
            if (Degree >= 135 && Degree < 180)
                return "NE";
            if (Degree >= 180 && Degree < 225)
                return "E";
            if (Degree >= 225 && Degree < 270)
                return "SE";
            if (Degree >= 270 && Degree < 315)
                return "S";
            return "SW";
        }
        public Wind Clone() => new Wind {Degree = Degree, Speed = Speed};
    }
}