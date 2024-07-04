using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json.Serialization.Metadata;

namespace Docker.DotNet
{
    internal static class JsonRequestContent
    {
        public static JsonRequestContent<T> Create<T>(T val, JsonSerializer serializer, JsonTypeInfo<T> typeInfo)
        {
            return new JsonRequestContent<T>(val, serializer, typeInfo);
        }
    }

    internal class JsonRequestContent<T> : IRequestContent
    {
        private readonly T _value;
        private readonly JsonSerializer _serializer;
        private readonly JsonTypeInfo<T> _typeInfo;

        public JsonRequestContent(T val, JsonSerializer serializer, JsonTypeInfo<T> typeInfo)
        {
            if (EqualityComparer<T>.Default.Equals(val))
            {
                throw new ArgumentNullException(nameof(val));
            }

            if (serializer == null)
            {
                throw new ArgumentNullException(nameof(serializer));
            }

            this._value = val;
            this._serializer = serializer;
            this._typeInfo = typeInfo;
        }

        public HttpContent GetContent()
        {
            return this._serializer.GetHttpContent(_value, _typeInfo);
        }
    }
}