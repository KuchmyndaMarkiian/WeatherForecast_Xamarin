﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Android.Util;
using WeatherForecast.Abstractions;
using WeatherForecast.Models;
using WeatherForecast.Models.ApiModels.Common;

namespace WeatherForecast.Infrastructure.Parsers
{
    class CityParsingFactory : IParsingFactory<City>
    {
        public List<City> ParseText(IEnumerable<string> textLines)
        {
            var enumerable = textLines as List<string> ?? textLines.ToList();
            if (!enumerable.Any())
                return null;
            return enumerable.AsParallel()
                .Select(FormatLinesToCity)
                .Except(new List<City> {null}.AsParallel())
                .ToList();
            //Local function C# 7.0+
            City FormatLinesToCity(string line)
            {
                try
                {
                    var items = line.Split('\t');
                    if (items.Length == 5 && !string.IsNullOrEmpty(items[0]))
                        return new City
                        {
                            CityId = int.Parse(items[0]),
                            Name = items[1],
                            CountryCode = items[4],
                            Coord = new Coord
                            {
                                Longtitude = double.Parse(items[3], CultureInfo.InvariantCulture),
                                Latitude = double.Parse(items[2], CultureInfo.InvariantCulture)
                            }
                        };
                }
                catch (Exception exception)
                {
                    Log.Error("ERROR", exception.Message);
                }
                return null;
            }
        }
    }
}