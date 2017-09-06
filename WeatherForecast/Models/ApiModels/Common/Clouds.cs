using System;
using Newtonsoft.Json;

using WeatherForecast.Abstractions;

namespace WeatherForecast.Models.ApiModels.Common
{
    [Serializable]
    public class Clouds : SqLiteEntityBase,ICloneable<Clouds>
    {
        /// <summary>
        ///  Cloudiness, %
        /// </summary>
        [JsonProperty("all")]
        public double All { get; set; }

        public Clouds Clone() => new Clouds {All = All};
    }
}