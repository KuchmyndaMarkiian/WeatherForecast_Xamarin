using System.Collections.Generic;

namespace WeatherForecast.Abstractions
{
    interface IParsingFactory<T>
    {
        List<T> ParseText(IEnumerable<string> textLines);
    }
}