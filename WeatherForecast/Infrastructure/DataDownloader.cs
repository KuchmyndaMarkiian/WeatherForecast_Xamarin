﻿using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using WeatherForecast.Infrastructure.Abstractions;
using WeatherForecast.Infrastructure.Models;
using WeatherForecast.Infrastructure.Parsers;

namespace WeatherForecast.Infrastructure
{
    class DataDownloader : IDisposable
    {
        private readonly HttpClient _client = new HttpClient();

        public async Task<List<CityModel>> DownloadCities()
        {
            var listResult = new List<CityModel>();
            var result = await _client.SendAsync(
                new HttpRequestMessage(HttpMethod.Get, "http://openweathermap.org/help/city_list.txt"));
            if (result.IsSuccessStatusCode)
            {
                var body = await result.Content.ReadAsStringAsync();
                string[] splited = body.Substring(body.IndexOf("\n", StringComparison.Ordinal) + 1).Split('\n');
                IParsingFactory<CityModel> factory = new CityParsingFactory();
                listResult = factory.ParseText(splited);
            }
            return listResult;
        }

        public void Dispose()
        {
            _client?.Dispose();
        }
    }
}