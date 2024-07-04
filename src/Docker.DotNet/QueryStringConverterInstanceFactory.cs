using System;
using System.Collections.Concurrent;
using System.Diagnostics.CodeAnalysis;

namespace Docker.DotNet
{
    internal class QueryStringConverterInstanceFactory : IQueryStringConverterInstanceFactory
    {
        private static readonly ConcurrentDictionary<Type, IQueryStringConverter> ConverterInstanceRegistry = new ConcurrentDictionary<Type, IQueryStringConverter>();

        public IQueryStringConverter GetConverterInstance(Type t)
        {
            return ConverterInstanceRegistry.GetOrAdd(
                t,
                InitializeConverter);
        }

        private IQueryStringConverter InitializeConverter([DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor)] Type t)
        {
            var instance = Activator.CreateInstance(t) as IQueryStringConverter;
            if (instance == null)
            {
                throw new InvalidOperationException($"Could not get instance of {t.FullName}");
            }
            return instance;
        }
    }
}