using System.Text.Json.Serialization;
using System.Text.Json;
using System.IO;

namespace Service_order_service
{
    public static class JsonStorageService
    {
        public static List<T> LoadFromFile<T>(string fileName)
        {
            if (!File.Exists(fileName))
                return [];

            var json = File.ReadAllText(fileName);
            return JsonSerializer.Deserialize<List<T>>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            }) ?? [];
        }

        public static void SaveToFile<T>(string fileName, List<T> data)
        {
            var json = JsonSerializer.Serialize(data, new JsonSerializerOptions
            {
                WriteIndented = true,
                ReferenceHandler = ReferenceHandler.IgnoreCycles
            });

            File.WriteAllText(fileName, json);
        }
    }
}
