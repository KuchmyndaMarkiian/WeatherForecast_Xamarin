using System.Collections.Generic;

namespace WeatherForecast.Abstractions
{
    interface IParsingFactory<T>
    {
        /// <summary>
        /// Parsing a input text to the preset data.
        /// </summary>
        /// <param name="textLines"></param>
        /// <returns></returns>
        List<T> ParseText(IEnumerable<string> textLines);
    }
}