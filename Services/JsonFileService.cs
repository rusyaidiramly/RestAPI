using Newtonsoft.Json;
using RestAPI.Models;
using System.Collections.Generic;

namespace RestAPI.Services
{
    public static class JsonFileService
    {
        private static readonly string fileName = "UserList.json";

        public static void SaveJsonFile(List<User> Users)
        {
            string jsonObj = JsonConvert.SerializeObject(Users, Formatting.Indented);
            System.IO.File.WriteAllText(fileName, jsonObj);
        }

        public static List<User> LoadJsonFile()
        {
            string jsonString = (System.IO.File.Exists(fileName))
                                            ? System.IO.File.ReadAllText(fileName)
                                            : "[]";

            List<User> Users = JsonConvert.DeserializeObject<List<User>>(jsonString);

            return Users;
        }
    }
}
