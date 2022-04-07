using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;

namespace RestAPI.Services
{
    public static class JsonFileService
    {

        public static void SaveJsonFile<T>(List<T> Objects, string fileName) where T : new()
        {
            string jsonObj = JsonConvert.SerializeObject(Objects, Formatting.Indented);
            File.WriteAllText(fileName, jsonObj);
        }

        public static List<T> LoadJsonFile<T>(string fileName) where T : new()
        {
            StreamReader sr = new StreamReader(fileName);
            string jsonString = (File.Exists(fileName)) ? sr.ReadToEnd() : "[]";

            List<T> Objects = JsonConvert.DeserializeObject<List<T>>(jsonString);

            sr.Close();

            return Objects;
        }
    }
}
