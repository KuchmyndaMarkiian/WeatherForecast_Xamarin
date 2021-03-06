using System;
using Newtonsoft.Json;
using WeatherForecast.Abstractions;

namespace WeatherForecast.Models.ApiModels.Common
{
    [Serializable]
    public class Main : SqLiteEntityBase, ICloneable<Main>
    {
        /// <summary>
        /// Temperature. Unit Default: Kelvin, Metric: Celsius, Imperial: Fahrenheit.
        /// </summary>
        [JsonProperty("temp")]
        public double Temp { get; set; }

        /// <summary>
        /// Atmospheric pressure (on the sea level, if there is no sea_level or grnd_level data), hPa
        /// </summary>
        [JsonProperty("pressure")]
        public double Pressure { get; set; }

        /// <summary>
        /// Humidity %
        /// </summary>
        [JsonProperty("humidity")]
        public double Humidity { get; set; }

        /// <summary>
        /// Minimum temperature at the moment. This is deviation from current temp that is 
        /// possible for large cities and megalopolises geographically expanded 
        /// (use these parameter optionally). 
        /// Unit Default: Kelvin, Metric: Celsius, Imperial: Fahrenheit.
        /// </summary>
        [JsonProperty("temp_min")]
        public double TempMin { get; set; }

        /// <summary>
        /// Maximum temperature at the moment. This is deviation from current temp that is 
        /// possible for large cities and megalopolises geographically expanded 
        /// (use these parameter optionally). 
        /// Unit Default: Kelvin, Metric: Celsius, Imperial: Fahrenheit.
        /// </summary>
        [JsonProperty("temp_max")]
        public double TempMax { get; set; }

        public Main Clone() => new Main
        {
            TempMax = TempMax,
            Temp = Temp,
            TempMin = TempMin,
            Humidity = Humidity,
            Pressure = Pressure
        };
    }
}