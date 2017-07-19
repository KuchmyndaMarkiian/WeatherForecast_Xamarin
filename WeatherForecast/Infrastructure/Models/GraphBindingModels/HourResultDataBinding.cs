using Com.Telerik.Widget.Chart.Engine.Databinding;
using Java.Lang;

namespace WeatherForecast.Infrastructure.Models.GraphBindingModels
{
    class HourResultDataBinding : DataPointBinding
    {
        public string PropertyName { get; set; }

        public override Object GetValue(Object p0)
        {
            switch (PropertyName)
            {
                case nameof(HourResult.DateTime):
                {
                    return (p0 as HourResult)?.DateTime;
                }
                case nameof(HourResult.Temperature):
                {
                    return (p0 as HourResult)?.Temperature;
                }
                default: return default(Object);
            }
        }
    }
}