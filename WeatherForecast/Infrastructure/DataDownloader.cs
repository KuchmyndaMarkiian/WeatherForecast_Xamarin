using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Android.Util;
using WeatherForecast.Infrastructure.Abstractions;
using WeatherForecast.Infrastructure.Models;
using WeatherForecast.Infrastructure.Parsers;

namespace WeatherForecast.Infrastructure
{
    class DataDownloader : IDisposable
    {
        private readonly HttpClient _client = new HttpClient();

        public async Task<List<City>> DownloadCities()
        {
            try
            {
                var listResult = new List<City>();
                var result = await _client.SendAsync(
                    new HttpRequestMessage(HttpMethod.Get, "http://openweathermap.org/help/city_list.txt"));
                if (result.IsSuccessStatusCode)
                {
                    var body = await result.Content.ReadAsStringAsync();
                    var splited = body.Substring(body.IndexOf("\n", StringComparison.Ordinal) + 1).Split('\n').ToList();
                    IParsingFactory<City> factory = new CityParsingFactory();
                    listResult = factory.ParseText(splited);
                }
                return listResult;
            }
            catch (Exception exception)
            {
                Log.Error("ERROR",exception.Message);
                return null;
            }
        }

        public void Dispose()
        {
            _client?.Dispose();
        }
    }
}