using Newtonsoft.Json;
using Realms;
using WeatherForecast.Abstractions;

namespace WeatherForecast.Models.ApiModels.Common
{
    public class Weather : RealmObject, ICloneable<Weather>
    {
        /// <summary>
        /// Weather condition id
        /// </summary>
        [JsonProperty("id")]
        public int Id { get; set; }

        /// <summary>
        /// Group of weather parameters (Rain, Snow, Extreme etc.)
        /// </summary>
        [JsonProperty("main")]
        public string Main { get; set; }

        /// <summary>
        /// Weather condition within the group
        /// </summary>
        [JsonProperty("description")]
        public string Description { get; set; }

        /// <summary>
        /// Weather icon id
        /// </summary>
        [JsonProperty("icon")]
        public string Icon { get; set; }

        public Weather Clone() => new Weather {Main = Main, Id = Id, Description = Description, Icon = Icon};
    }
}