using Newtonsoft.Json;
using Realms;
using WeatherForecast.Abstractions;

namespace WeatherForecast.Models.ApiModels.Common
{
    public class Clouds : RealmObject,ICloneable<Clouds>
    {
        /// <summary>
        ///  Cloudiness, %
        /// </summary>
        [JsonProperty("all")]
        public double All { get; set; }

        public Clouds Clone() => new Clouds {All = All};
    }
}