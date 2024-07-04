using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Docker.DotNet.Models;

namespace Docker.DotNet
{
    internal class ConfigOperations : IConfigOperations
    {
        private readonly DockerClient _client;

        internal ConfigOperations(DockerClient client)
        {
            this._client = client;
        }

        async Task<IList<SwarmConfig>> IConfigOperations.ListConfigsAsync(CancellationToken cancellationToken)
        {
            return await this._client.MakeRequestAsync(this._client.NoErrorHandlers, HttpMethod.Get, "configs", DockerJsonSerializerContext.Default.SwarmConfigArray, cancellationToken).ConfigureAwait(false);
        }

        async Task<SwarmCreateConfigResponse> IConfigOperations.CreateConfigAsync(SwarmCreateConfigParameters body, CancellationToken cancellationToken)
        {
            if (body == null)
            {
                throw new ArgumentNullException(nameof(body));
            }

            var data = JsonRequestContent.Create(body.Config, this._client.JsonSerializer, DockerJsonSerializerContext.Default.SwarmConfigSpec);
            return await this._client.MakeRequestAsync(this._client.NoErrorHandlers, HttpMethod.Post, "configs/create", null, data, DockerJsonSerializerContext.Default.SwarmCreateConfigResponse, cancellationToken).ConfigureAwait(false);
        }

        async Task<SwarmConfig> IConfigOperations.InspectConfigAsync(string id, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new ArgumentNullException(nameof(id));
            }

            return await this._client.MakeRequestAsync(this._client.NoErrorHandlers, HttpMethod.Get, $"configs/{id}", DockerJsonSerializerContext.Default.SwarmConfig, cancellationToken).ConfigureAwait(false);
        }

        Task IConfigOperations.RemoveConfigAsync(string id, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new ArgumentNullException(nameof(id));
            }

            return this._client.MakeRequestAsync(this._client.NoErrorHandlers, HttpMethod.Delete, $"configs/{id}", cancellationToken);
        }
    }
}
