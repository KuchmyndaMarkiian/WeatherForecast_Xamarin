using System;
using System.Collections.Generic;
using System.Linq;
using Realms;

namespace WeatherForecast.Abstractions
{
    interface IMemoryManipulator:IDisposable
    {
        bool IsExists<T>() where T : SQLiteEntityBase;
        void Write<T>(T data) where T : SQLiteEntityBase;
        void Clear<T>() where T : SQLiteEntityBase;
        IQueryable<T> Read<T>(Func<T, bool> condition) where T : SQLiteEntityBase;
    }
}