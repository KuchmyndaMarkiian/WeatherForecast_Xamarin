using System;
using System.Linq;
using Realms;

namespace WeatherForecast.Infrastructure
{
    interface IMemoryManipulator:IDisposable
    {
        bool IsExists<T>() where T : RealmObject;
        void Write<T>(T data) where T : RealmObject;
        void Clear<T>() where T : RealmObject;
        IQueryable<T> Read<T>(Func<T, bool> condition) where T : RealmObject;
    }
}