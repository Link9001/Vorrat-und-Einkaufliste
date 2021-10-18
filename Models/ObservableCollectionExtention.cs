using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace DataModel
{
    public static class ObservableCollectionExtention
    {
        public static void Dispose<T>(this ObservableCollection<T> collection, string nameOfCollection, bool save = true)
        {
            if (save)
            {
                DirectoryInfo directoryInfo = Directory.CreateDirectory(AppDomain.CurrentDomain.FriendlyName);
                string finalPath = Path.Combine(Environment.CurrentDirectory, directoryInfo.FullName, nameOfCollection + ".json");
                JsonSerializerOptions options = new()
                {
                    ReferenceHandler = ReferenceHandler.Preserve,
                    WriteIndented = true
                };
                using FileStream file = File.Create(finalPath);
                file.Write(System.Text.Json.JsonSerializer.SerializeToUtf8Bytes(collection, options));
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0060:Nicht verwendete Parameter entfernen", Justification = "Hier wird die Collection mit Daten abgefüllt und so benutzt")]
        public static void LoadList<T>(this ObservableCollection<T> collection, string nameOfListToLoad)
        {
            string path = Path.Combine(Environment.CurrentDirectory, AppDomain.CurrentDomain.FriendlyName, nameOfListToLoad + ".json");
            if (File.Exists(path))
            {
                using StreamReader file = File.OpenText(path);
                IsoDateTimeConverter converter = new() { DateTimeFormat = "dd/MM/yyyy" };
                var extrectedList = JsonConvert.DeserializeObject<ObservableCollection<T>>(file.ReadToEnd(), converter);
                foreach (var item in extrectedList){
                    collection.Add(item);
                }
            }
        }

        public static void AddCollectionToThis<T>(this ObservableCollection<T> @this, IEnumerable<T> collection)
        {
            foreach (T item in collection)
            {
                @this.Add(item);
            }
        }
    }
}
