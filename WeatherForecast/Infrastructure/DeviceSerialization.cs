using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace WeatherForecast.Infrastructure
{
    public class DeviceSerialization
    {
        public static void Serialize<T>(T data, string file)
        {
            var formatter = new BinaryFormatter();
            using (FileStream stream=new FileStream(file,FileMode.OpenOrCreate,FileAccess.ReadWrite))
            {
                formatter.Serialize(stream,data);
            }
        }

        public static T Deserialize<T>(string file)
        {
            var formatter = new BinaryFormatter();
            object res;
            FileInfo fileInfo=new FileInfo(file);
            if (fileInfo.Exists)
            {
                using (FileStream stream = new FileStream(file, FileMode.Open, FileAccess.ReadWrite))
                {
                    res = formatter.Deserialize(stream);
                }
                return (T) res;
            }
            return default(T);
        }
    }
}