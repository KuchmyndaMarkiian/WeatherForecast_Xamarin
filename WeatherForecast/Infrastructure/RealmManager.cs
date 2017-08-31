using System;
using System.Linq;
using Realms;
using WeatherForecast.Abstractions;

namespace WeatherForecast.Infrastructure
{
    class RealmManager : IMemoryManipulator
    {
        private readonly Realm _dbRealm;

        public RealmManager(Realm dbRealm)
        {
            _dbRealm = dbRealm;
        }

        public bool IsExists<T>() where T : RealmObject => _dbRealm.All<T>().Any();

        public void Write<T>(T data) where T : RealmObject
        {
            _dbRealm.Write(() => _dbRealm.Add(data));
        }

        public void Clear<T>() where T : RealmObject
        {
            _dbRealm.RemoveAll<T>();
        }

        //TODO:  Need fix storing and reading(weathers always null)
        public IQueryable<T> Read<T>(Func<T, bool> condition) where T : RealmObject => condition == null
            ? _dbRealm.All<T>()
            : _dbRealm.All<T>().Where(condition).AsQueryable();

        public void Dispose()
        {
            _dbRealm?.Dispose();
        }
    }
}