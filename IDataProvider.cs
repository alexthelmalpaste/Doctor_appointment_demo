public interface IDataProvider<T>
{
    IEnumerable<T> Load();
    void Save(IEnumerable<T> items);
}
