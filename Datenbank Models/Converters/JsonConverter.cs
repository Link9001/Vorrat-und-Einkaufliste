using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Database_Models.Converters;
internal static class JsonConverter
{
    public static void Serialize<T>(T toSerialize, string nameOfFile, string rootPath)
    {
        if (new DirectoryInfo(rootPath).Exists)
        {
            string finalPath = Path.Combine(rootPath, nameOfFile + ".json");
            JsonSerializerOptions options = new()
            {
                ReferenceHandler = ReferenceHandler.Preserve,
                WriteIndented = true
            };
            using FileStream file = File.Create(finalPath);
            file.Write(System.Text.Json.JsonSerializer.SerializeToUtf8Bytes(toSerialize, options));
        }
    }

    public static void Deserialize<T>(ref T toDeserialize, string nameOfFile, string rootPath)
    {
        string path = Path.Combine(rootPath, nameOfFile + ".json");
        if (File.Exists(path))
        {
            using StreamReader file = File.OpenText(path);
            IsoDateTimeConverter converter = new() { DateTimeFormat = "dd/MM/yyyy" };
            toDeserialize = JsonConvert.DeserializeObject<T>(file.ReadToEnd(), converter) ?? Activator.CreateInstance<T>();
        }
        else
        {
            if (toDeserialize == null)
            {
                toDeserialize = Activator.CreateInstance<T>();
            }
        }
    }
}