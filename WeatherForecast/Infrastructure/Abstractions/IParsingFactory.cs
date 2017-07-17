using System.Collections.Generic;
using System.Security.Policy;

namespace WeatherForecast.Infrastructure.Abstractions
{
    interface IParsingFactory<T>
    {
        List<T> ParseText(string[] textLines);
    }
}