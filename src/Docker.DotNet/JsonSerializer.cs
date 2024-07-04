using System.Buffers;
using System.Collections.Generic;
using System.IO;
using System.IO.Pipelines;
using System.Net.Http;
using System.Net.Http.Json;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Text.Json.Serialization.Metadata;
using System.Threading;
using System.Threading.Tasks;

namespace Docker.DotNet
{
    /// <summary>
    /// Facade for <see cref="System.Text.Json.JsonSerializer"/> serialization.
    /// </summary>
    internal class JsonSerializer
    {
        // Adapted from https://github.com/dotnet/runtime/issues/33030#issuecomment-1524227075
        public async IAsyncEnumerable<T> Deserialize<T>(Stream stream, JsonTypeInfo<T> typeInfo, [EnumeratorCancellation] CancellationToken cancellationToken)
        {
            var reader = PipeReader.Create(stream);
            while (true)
            {
                var result = await reader.ReadAsync(cancellationToken);
                var buffer = result.Buffer;
                while (!buffer.IsEmpty && TryParseJson(ref buffer, out var jsonDocument))
                {
                    yield return jsonDocument.Deserialize(typeInfo);
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

        public T DeserializeObject<T>(byte[] json, JsonTypeInfo<T> typeInfo)
        {
            return System.Text.Json.JsonSerializer.Deserialize(json, typeInfo);
        }

        public byte[] SerializeObject<T>(T value, JsonTypeInfo<T> typeInfo)
        {
            return System.Text.Json.JsonSerializer.SerializeToUtf8Bytes(value, typeInfo);
        }

        public JsonContent GetHttpContent<T>(T value, JsonTypeInfo<T> typeInfo)
        {
            return JsonContent.Create(value, typeInfo);
        }

        public async Task<T> DeserializeAsync<T>(HttpContent content, JsonTypeInfo<T> typeInfo, CancellationToken token)
        {
            return await content.ReadFromJsonAsync(typeInfo, token)
                .ConfigureAwait(false);
        }
    }
}
