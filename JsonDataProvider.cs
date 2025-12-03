using System.Text.Json;

public class JsonDataProvider<T> : IDataProvider<T>
{
    private readonly string _filePath;
    public JsonDataProvider(string filePath) => _filePath = filePath;

    public IEnumerable<T> Load()
    {
        if (!File.Exists(_filePath)) return new List<T>();
        var json = File.ReadAllText(_filePath);
        return JsonSerializer.Deserialize<List<T>>(json) ?? new List<T>();
    }

    public void Save(IEnumerable<T> items)
    {
        var json = JsonSerializer.Serialize(items);
        File.WriteAllText(_filePath, json);
    }
}
