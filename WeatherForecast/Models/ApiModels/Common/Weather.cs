using System;
using Newtonsoft.Json;
using WeatherForecast.Abstractions;

namespace WeatherForecast.Models.ApiModels.Common
{
    [Serializable]
    public class Weather : SqLiteEntityBase, ICloneable<Weather>
    {
        /// <summary>
        /// Weather condition id
        /// </summary>
        [JsonProperty("id")]
        public int WeatherId { get; set; }

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

        public Weather Clone() => new Weather {Main = Main, WeatherId = WeatherId, Description = Description, Icon = Icon};
    }
}