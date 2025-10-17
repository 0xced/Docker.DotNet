namespace Docker.DotNet;

internal sealed class JsonSerializer
{
    public static JsonSerializer Instance { get; }
        = new JsonSerializer();

    public HttpContent GetHttpContent<T>(JsonTypeInfo<T> jsonTypeInfo, T value)
    {
        return new StringContent(Serialize(jsonTypeInfo, value), Encoding.UTF8, "application/json");
    }

    public string Serialize<T>(JsonTypeInfo<T> jsonTypeInfo, T value)
    {
        return System.Text.Json.JsonSerializer.Serialize(value, jsonTypeInfo);
    }

    public byte[] SerializeToUtf8Bytes<T>(JsonTypeInfo<T> jsonTypeInfo, T value)
    {
        return System.Text.Json.JsonSerializer.SerializeToUtf8Bytes(value, jsonTypeInfo);
    }

    public T Deserialize<T>(JsonTypeInfo<T> jsonTypeInfo, byte[] json)
    {
        return System.Text.Json.JsonSerializer.Deserialize(json, jsonTypeInfo);
    }

    public Task<T> DeserializeAsync<T>(JsonTypeInfo<T> jsonTypeInfo, HttpContent content, CancellationToken cancellationToken)
    {
        return content.ReadFromJsonAsync(jsonTypeInfo, cancellationToken);
    }

    public async IAsyncEnumerable<T> DeserializeAsync<T>(JsonTypeInfo<T> jsonTypeInfo, Stream stream, [EnumeratorCancellation] CancellationToken cancellationToken)
    {
        var reader = PipeReader.Create(stream);

        while (true)
        {
            var result = await reader.ReadAsync(cancellationToken)
                .ConfigureAwait(false);

            var buffer = result.Buffer;

            while (!buffer.IsEmpty && TryParseJson(ref buffer, out var jsonDocument))
            {
                yield return jsonDocument.Deserialize(jsonTypeInfo);
            }

            if (result.IsCompleted)
            {
                break;
            }

            reader.AdvanceTo(buffer.Start, buffer.End);
        }

        await reader.CompleteAsync();
    }

    private static bool TryParseJson(ref ReadOnlySequence<byte> buffer, out JsonDocument jsonDocument)
    {
        var reader = new Utf8JsonReader(buffer, isFinalBlock: false, default);

        if (JsonDocument.TryParseValue(ref reader, out jsonDocument))
        {
            buffer = buffer.Slice(reader.BytesConsumed);
            return true;
        }

        return false;
    }
}