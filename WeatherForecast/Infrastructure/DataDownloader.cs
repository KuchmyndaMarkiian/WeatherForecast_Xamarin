using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Android.Util;
using WeatherForecast.Abstractions;
using WeatherForecast.Infrastructure.Parsers;
using WeatherForecast.Models;

namespace WeatherForecast.Infrastructure
{
    class DataDownloader
    {
        public async Task<List<City>> DownloadCities(Stream stream)
        {
            try
            {
                var body = await new StreamReader(stream).ReadToEndAsync();
                var splited = body.Substring(body.IndexOf("\n", StringComparison.Ordinal) + 1).Split('\n').ToList();
                IParsingFactory<City> factory = new CityParsingFactory();
                return factory.ParseText(splited);
            }
            catch (Exception exception)
            {
                Log.Error("ERROR",exception.Message);
                return null;
            }
        }
    }
}