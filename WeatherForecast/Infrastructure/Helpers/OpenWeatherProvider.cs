﻿using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Android.Util;
using WeatherForecast.Infrastructure.Abstractions;

namespace WeatherForecast.Infrastructure.Helpers
{
    class OpenWeatherProvider : IApiProvider
    {
        public string IdCode { get; } = "&APPID=96de315db02590f7a3f9554b37aed1af";
        public string HostUrl { get; } = "http://api.openweathermap.org/data/2.5/";

        public async Task<T> GetData<T>(string url, params (string, string)[] parameters)
        {
            string createdUrl = $"{HostUrl}{url}{FormatParameters(parameters)}";
            try
            {
                using (var client = new HttpClient())
                {
                    var requestMessage = new HttpRequestMessage(HttpMethod.Get, createdUrl);
                    var responseMessage = client.SendAsync(requestMessage).Result;
                    if (responseMessage.IsSuccessStatusCode)
                    {
                        return JsonConverter.Read<T>(await responseMessage.Content.ReadAsStringAsync());
                    }
                }
            }
            catch (Exception exception)
            {
                Log.Error("error", exception.Message);

            }
            return default(T);

            string FormatParameters((string, string)[] data) =>
                $"?{string.Join("&", data.Select(x => $"{x.Item1}={x.Item2}"))}{IdCode}";
        }

        public struct UrlParameters
        {
            public static string Current => "weather";
            public static string Daily5 => "forecast";
            public static (string, string) Metric => ("units", "metric");
        }

    }
}