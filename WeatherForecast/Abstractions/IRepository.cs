using System;
using System.Collections.Generic;

namespace WeatherForecast.Abstractions
{
    interface IRepository<T>
    {
        bool Insert(T @object);
        bool Update(T @object);
        bool Delete(T @object);
        T GetObject(Func<T, bool> condition);
        List<T> GetObjects();
        void Clear();
        bool Any();
        T First();
        T Last();
    }
}