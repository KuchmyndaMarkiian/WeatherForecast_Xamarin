using Newtonsoft.Json;

namespace WeatherForecast.Infrastructure.Helpers
{
    class JsonConverter
    {
        public static string Convert<T>(T data) => JsonConvert.SerializeObject(data);
        public static T Read<T>(string json) => JsonConvert.DeserializeObject<T>(json);
    }
}