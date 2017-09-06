namespace WeatherForecast.Models
{
    public class TodayFragmentModel
    {
        public string City { get; set; }
        public double Temperature { get; set; }
        public double MaxTemperature { get; set; }
        public double MinTemperature { get; set; }
        public int Icon { get; set; }
    }
}