using System;
using System.Collections.Generic;
using System.Net.Http;
using WeatherForecast.Infrastructure.Abstractions;
using WeatherForecast.Infrastructure.Models;
using WeatherForecast.Infrastructure.Models.ApiModels.Common;

namespace WeatherForecast.Infrastructure.Parsers
{
    class CityParsingFactory : IParsingFactory<City>
    {
        public List<City> ParseText(string[] textLines)
        {
            if (textLines == null || textLines.Length == 0)
                return null;
            List<City> cities = new List<City>();
            foreach (string line in textLines)
            {
                string[] items = line.Split('\t');
                if (items.Length == 5 && !string.IsNullOrEmpty(items[0]))
                    cities.Add(new City
                    {
                        Id = int.Parse(items[0]),
                        Name = items[1],
                        CountryCode = items[4],
                        Coord = new Coord() {Longtitude = Double.Parse(items[3]), Latitude = Double.Parse(items[2])}
                    });
            }
            return cities;
        }
    }
}