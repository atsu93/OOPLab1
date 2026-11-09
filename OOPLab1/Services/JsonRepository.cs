using OOPLab1.Models;
using System.IO;
using System.Text.Json;

namespace OOPLab1.Services
{
    public class JsonTaskRepository : ITaskRepository
    {
        private readonly string _filePath;
        private static readonly JsonSerializerOptions _jsonOptions = new() { WriteIndented = true };

        public JsonTaskRepository(string fileName = "tasks.json")
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(fileName);
            _filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, fileName);
        }

        public void Save(IEnumerable<TodoModel> tasks)
        {
            string jsonString = JsonSerializer.Serialize(tasks, _jsonOptions);
            File.WriteAllText(_filePath, jsonString);
        }

        public List<TodoModel> Load()
        {
            try
            {
                if (!File.Exists(_filePath)) return [];
                string jsonString = File.ReadAllText(_filePath);
                return JsonSerializer.Deserialize<List<TodoModel>>(jsonString) ?? [];
            }
            catch
            {
                return [];
            }
        }
    }
}
