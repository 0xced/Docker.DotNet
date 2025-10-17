namespace Docker.DotNet;

/// <summary>
/// Handles serialization of objects like Lists, Arrays, etc.
/// </summary>
internal class EnumerableQueryStringConverter : IQueryStringConverter
{
    public bool CanConvert(Type t)
    {
        return typeof(IEnumerable<string>).IsAssignableFrom(t);
    }

    public string[] Convert(object o)
    {
        Debug.Assert(o is IEnumerable<string>);

        return ((IEnumerable<string>)o).ToArray();
    }
}