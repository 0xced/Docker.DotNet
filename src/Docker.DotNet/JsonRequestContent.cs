namespace Docker.DotNet;

internal class JsonRequestContent
{
    private JsonRequestContent()
    {
    }

    public static IRequestContent Create<T>(JsonTypeInfo<T> jsonTypeInfo, T val) where T : class
    {
        return new JsonRequestContentImpl<T>(val, jsonTypeInfo);
    }

    private class JsonRequestContentImpl<T>(T val, JsonTypeInfo<T> jsonTypeInfo) : IRequestContent
        where T : class
    {
        private readonly T _value = val ?? throw new ArgumentNullException(nameof(val));
        private readonly JsonTypeInfo<T> _jsonTypeInfo = jsonTypeInfo ?? throw new ArgumentNullException(nameof(jsonTypeInfo));

        public HttpContent GetContent() => JsonSerializer.Instance.GetHttpContent(_jsonTypeInfo, _value);
    }
}