using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Threading;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using UnityEngine;
using Unity.VisualScripting.FullSerializer;

namespace Datasaver
{
    public class DataSaver
    {
        public DataSaver()
        {

        }

        JsonSerializer serializer = new JsonSerializer();

        SemaphoreSlim sem = new SemaphoreSlim(1, 1);

        /// <summary>
        /// Сериализует переданный объект в строку json и вернет ее
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public string GetSerializeJson<T>(T obj)
        {
            var settings = new JsonSerializerSettings()
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };

            return JsonConvert.SerializeObject(obj, settings);
        }

        /// <summary>
        /// Десериализует строку в указанный тип и вернет его
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="json"></param>
        /// <returns></returns>
        public T DeserializeFromJson<T>(string json)
        {
            return JsonConvert.DeserializeObject<T>(json);
        }

        /// <summary>
        /// Сериализует любой объект по указанному пути
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <param name="path"></param>
        public async Task Serialize<T>(T obj, string path)
        {
            //string _path = path == "" ? Application.persistentDataPath : path;

            string _path = Application.persistentDataPath + path;

            await AsyncSerialize<T>(obj, _path);
        }

        /// <summary>
        /// Десереализует любой json-файл по указанному пути и возвращает типизированный список
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="path"></param>
        /// <returns></returns>
        public async Task<List<T>> Deserialize<T>(string path)
        {
            //string _path = path == "" ? Application.persistentDataPath : path;

            string _path = Application.persistentDataPath + path;

            List<T> result = await AsyncDeserialize<T>(_path);

            return result;
        }

        //private async void SerializeAsync<T>(T obj, string path)
        //{
        //    sem.WaitOne();

        //    Serialize<T>(obj, path);

        //    sem.Release();

        //}

        //private async Task<List<T>> DeserializeAsync<T>( string path)
        //{
        //    sem.WaitOne();

        //    var result = Deserialize<T>(path);

        //    sem.Release();
        //    return result;
        //}

        public async Task AsyncSerialize<T>(T obj, string path)
        {
            //sem.WaitOne();
            //
            //serializer.Converters.Add(new JavaScriptDateTimeConverter());
            //
            //using (StreamWriter sw = new StreamWriter(path))
            //using (JsonWriter writer = new JsonTextWriter(sw))
            //{
            //    serializer.Serialize(writer, obj);
            //}
            //
            //sem.Release();

            await sem.WaitAsync();
            try
            {
                Directory.CreateDirectory(Path.GetDirectoryName(path));

                var serializer = new JsonSerializer
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                    TypeNameHandling = TypeNameHandling.All
                };
                serializer.Error += (sender, args) =>
                {
                    args.ErrorContext.Handled = true;
                };

                using (var sw = new StreamWriter(path, false))
                using (var writer = new JsonTextWriter(sw))
                {
                    serializer.Serialize(writer, obj);
                }
            }
            finally
            {
                sem.Release();
            }
        }

        public async Task<List<T>> AsyncDeserialize<T>(string path)
        {
            //sem.WaitOne();
            //
            //if (!Directory.Exists(path))
            //{
            //    File.CreateText(path);
            //    using (var writer = new StreamWriter(path, false))
            //    {
            //        writer.Write("json");
            //    }
            //}
            //
            //List<T> result = null;
            //using (StreamReader r = new StreamReader(path))
            //{
            //    string json = r.ReadToEnd();
            //    result = JsonConvert.DeserializeObject<List<T>>(json);
            //}
            //
            //sem.Release();
            //return result;
            await sem.WaitAsync();
            try
            {
                if (!File.Exists(path))
                {
                    // создаём пустой JSON-массив
                    File.WriteAllText(path, "[]");
                }

                var settings = new JsonSerializerSettings
                {
                    TypeNameHandling = TypeNameHandling.All
                };

                using (var r = new StreamReader(path))
                {
                    string json = await r.ReadToEndAsync();
                    return JsonConvert.DeserializeObject<List<T>>(json, settings);
                }
            }
            finally
            {
                sem.Release();
            }
        }
    }
}