using System;
using System.Collections.Generic;
using System.Linq;
using Android.Util;
using SQLite.Net.Interop;
using WeatherForecast.Abstractions;
using WeatherForecast.Models;
using WeatherForecast.Models.ApiModels;
using WeatherForecast.Models.ApiModels.Common;

namespace WeatherForecast.Infrastructure
{
    internal class MainModelRepository : IRepository<MainModel>
    {
        private readonly string _connectionName;

        public MainModelRepository()
        {
            var name = "WeatherForecast.db";
            var folder = System.Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            _connectionName = System.IO.Path.Combine(folder, name);
            TryCreateDb();
        }

        private bool TryCreateDb()
        {
            try
            {
                using (var conn = Extensions.CreateConnection(_connectionName))
                {
                    conn.CreateTable<Coord>(CreateFlags.AutoIncPK);
                    conn.CreateTable<Weather>(CreateFlags.AutoIncPK);
                    conn.CreateTable<WeatherType>(CreateFlags.AutoIncPK);
                    conn.CreateTable<Wind>(CreateFlags.AutoIncPK);
                    conn.CreateTable<Main>(CreateFlags.AutoIncPK);
                    conn.CreateTable<Clouds>(CreateFlags.AutoIncPK);
                    conn.CreateTable<Sys>(CreateFlags.AutoIncPK);
                    conn.CreateTable<List>(CreateFlags.AutoIncPK);
                    conn.CreateTable<FiveDaysWeather>(CreateFlags.AutoIncPK);
                    conn.CreateTable<CityCurrrentWeather>(CreateFlags.AutoIncPK);
                    conn.CreateTable<City>(CreateFlags.AutoIncPK);
                    conn.CreateTable<MainModel>(CreateFlags.AutoIncPK);
                    return true;
                }
            }
            catch (Exception exception)
            {
                Log.Error("SQLite error", exception.Message);
                return false;
            }
        }

        public bool Insert(MainModel @object)
        {
            try
            {
                using (var conn = Extensions.CreateConnection(_connectionName))
                {
                    conn.Insert(@object);
                    return true;
                }
            }
            catch (Exception exception)
            {
                Log.Error("SQLite error", exception.Message);
                return false;
            }
        }

        public bool Update(MainModel @object)
        {
            try
            {
                using (var conn = Extensions.CreateConnection(_connectionName))
                {
                    conn.Update(@object);
                    return true;
                }
            }
            catch (Exception exception)
            {
                Log.Error("SQLite error", exception.Message);
                return false;
            }
        }

        public bool Delete(MainModel @object)
        {
            try
            {
                using (var conn = Extensions.CreateConnection(_connectionName))
                {
                    conn.Delete(@object);
                    return true;
                }
            }
            catch (Exception exception)
            {
                Log.Error("SQLite error", exception.Message);
                return false;
            }
        }

        public MainModel GetObject(Func<MainModel, bool> condition)
        {
            try
            {
                using (var conn = Extensions.CreateConnection(_connectionName))
                {
                    return conn.Get<MainModel>(condition);
                }
            }
            catch (Exception exception)
            {
                Log.Error("SQLite error", exception.Message);
                return null;
            }
        }

        public List<MainModel> GetObjects()
        {
            try
            {
                using (var conn = Extensions.CreateConnection(_connectionName))
                {
                    return conn.Query<MainModel>("select * from MainModels");
                }
            }
            catch (Exception exception)
            {
                Log.Error("SQLite error", exception.Message);
                return new List<MainModel>();
            }
        }

        public void Clear()
        {
            using (var conn = Extensions.CreateConnection(_connectionName))
            {
                conn.DeleteAll<MainModel>();
            }
        }

        public bool Any()
        {
            var list = GetObjects();
            return list != null && list.Any();
        }

        public MainModel First() => GetObjects().FirstOrDefault();

        public MainModel Last() => GetObjects().LastOrDefault();
    }
}