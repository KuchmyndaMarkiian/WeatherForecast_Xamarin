using System;
using System.Collections.Generic;
using System.Net.Http;
using WeatherForecast.Infrastructure.Abstractions;
using WeatherForecast.Infrastructure.Models;

namespace WeatherForecast.Infrastructure.Parsers
{
    class CityParsingFactory : IParsingFactory<CityModel>
    {
        public List<CityModel> ParseText(string[] textLines)
        {
            if (textLines == null || textLines.Length == 0)
                return null;
            List<CityModel> cities = new List<CityModel>();
            foreach (string line in textLines)
            {
                string[] items = line.Split('\t');
                if (items.Length == 5 && !string.IsNullOrEmpty(items[0]))
                    cities.Add(new CityModel{Id = int.Parse(items[0]), Name = items[1], CountryCode = items[4] });
            }
            return cities;
        }
    }
}