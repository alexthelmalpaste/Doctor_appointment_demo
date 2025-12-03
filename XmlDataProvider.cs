using System.Xml.Serialization;

public class XmlDataProvider<T> : IDataProvider<T>
{
    private readonly string _filePath;
    public XmlDataProvider(string filePath) => _filePath = filePath;

    public IEnumerable<T> Load()
    {
        if (!File.Exists(_filePath)) return new List<T>();
        using var stream = File.OpenRead(_filePath);
        var serializer = new XmlSerializer(typeof(List<T>));
        return (List<T>?)serializer.Deserialize(stream) ?? new List<T>();
    }

    public void Save(IEnumerable<T> items)
    {
        using var stream = File.Create(_filePath);
        var serializer = new XmlSerializer(typeof(List<T>));
        serializer.Serialize(stream, items.ToList());
    }
}
