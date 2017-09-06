using System;
using SQLite.Net.Attributes;

namespace WeatherForecast.Models.ApiModels.Common
{
    [Serializable]
    public abstract class SqLiteEntityBase
    {
           [PrimaryKey, AutoIncrement]
           public int Id { get; set; }
    }
}