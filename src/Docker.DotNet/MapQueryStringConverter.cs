namespace Docker.DotNet;

internal class MapQueryStringConverter : IQueryStringConverter
{
    public bool CanConvert(Type t)
    {
        return typeof(IDictionary<string, IDictionary<string, bool>>).IsAssignableFrom(t) || typeof(IDictionary<string, string>).IsAssignableFrom(t);
    }

    public string[] Convert(object o)
    {
        Debug.Assert(o != null);

        return o switch
        {
            IDictionary<string, IDictionary<string, bool>> d => [JsonSerializer.Instance.Serialize(Default.IDictionaryStringIDictionaryStringBoolean, d)],
            IDictionary<string, string> d => [JsonSerializer.Instance.Serialize(Default.IDictionaryStringString, d)],
            _ => throw new NotSupportedException($"Cannot convert {o.GetType()}"),
        };
    }
}