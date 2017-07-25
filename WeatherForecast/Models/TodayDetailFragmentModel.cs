using WeatherForecast.Models.ApiModels;
using WeatherForecast.Models.ApiModels.Common;

namespace WeatherForecast.Models
{
    public class TodayDetailFragmentModel
    {
        public Clouds Clouds { get; set; }
        public Wind Wind { get; set; }
        public Main Main { get; set; }
        public Sys Sys { get; set; }
    }
}