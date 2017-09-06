using System.IO;
using Environment = System.Environment;

namespace WeatherForecast.Infrastructure.Helpers
{
    public class Global
    {
        static readonly string WeatherFileName = "weather.txt";
        static readonly string Folder = System.Environment.GetFolderPath(Environment.SpecialFolder.Personal);
        public static string GetWeatherFileName() => Path.Combine(Folder, WeatherFileName);

        public static int GetIcon(string type)
        {
            if (type == "01d")
                return Resource.Drawable.p01d;
            if (type == "01n")
                return Resource.Drawable.p01n;
            if (type == "02d")
                return Resource.Drawable.p02d;
            if (type == "02n")
                return Resource.Drawable.p02n;
            if (type == "03d" || type == "03n")
                return Resource.Drawable.p03;
            if (type == "04d" || type == "04n")
                return Resource.Drawable.p04;
            if (type == "09d" || type == "09n")
                return Resource.Drawable.p09;
            if (type == "10d")
                return Resource.Drawable.p10d;
            if (type == "10n")
                return Resource.Drawable.p10n;
            if (type == "11d" || type == "11n")
                return Resource.Drawable.p11;
            if (type == "13d" || type == "13n")
                return Resource.Drawable.p13;
            return Resource.Drawable.p50;
        }
    }
}