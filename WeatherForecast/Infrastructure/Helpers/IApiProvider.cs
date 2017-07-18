using System.Threading.Tasks;

namespace WeatherForecast.Infrastructure.Helpers
{
    interface IApiProvider
    {
        string IdCode { get;}
        string HostUrl { get; }
        Task<T> GetData<T>(string url, params (string, string)[] parameters);
    }
}