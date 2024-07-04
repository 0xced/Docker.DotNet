using Docker.DotNet.Models;
using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Docker.DotNet
{
    internal class VolumeOperations : IVolumeOperations
    {
        private readonly DockerClient _client;

        internal VolumeOperations(DockerClient client)
        {
            this._client = client;
        }

        async Task<VolumesListResponse> IVolumeOperations.ListAsync(CancellationToken cancellationToken)
        {
            return await this._client.MakeRequestAsync(this._client.NoErrorHandlers, HttpMethod.Get, "volumes", DockerJsonSerializerContext.Default.VolumesListResponse, cancellationToken).ConfigureAwait(false);
        }

        async Task<VolumesListResponse> IVolumeOperations.ListAsync(VolumesListParameters parameters, CancellationToken cancellationToken)
        {
            var queryParameters = parameters == null ? null : new QueryString<VolumesListParameters>(parameters);
            return await this._client.MakeRequestAsync(this._client.NoErrorHandlers, HttpMethod.Get, "volumes", queryParameters, DockerJsonSerializerContext.Default.VolumesListResponse, cancellationToken).ConfigureAwait(false);
        }

        async Task<VolumeResponse> IVolumeOperations.CreateAsync(VolumesCreateParameters parameters, CancellationToken cancellationToken)
        {
            if (parameters == null)
            {
                throw new ArgumentNullException(nameof(parameters));
            }

            var data = JsonRequestContent.Create(parameters, this._client.JsonSerializer, DockerJsonSerializerContext.Default.VolumesCreateParameters);
            return await this._client.MakeRequestAsync(this._client.NoErrorHandlers, HttpMethod.Post, "volumes/create", null, data, DockerJsonSerializerContext.Default.VolumeResponse, cancellationToken);
        }

        async Task<VolumeResponse> IVolumeOperations.InspectAsync(string name, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException(nameof(name));
            }


            return await this._client.MakeRequestAsync(this._client.NoErrorHandlers, HttpMethod.Get, $"volumes/{name}", DockerJsonSerializerContext.Default.VolumeResponse, cancellationToken).ConfigureAwait(false);
        }

        Task IVolumeOperations.RemoveAsync(string name, bool? force, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException(nameof(name));
            }
            
            return this._client.MakeRequestAsync(this._client.NoErrorHandlers, HttpMethod.Delete, $"volumes/{name}", cancellationToken);
        }

        async Task<VolumesPruneResponse> IVolumeOperations.PruneAsync(VolumesPruneParameters parameters, CancellationToken cancellationToken)
        {
            var queryParameters = parameters == null ? null : new QueryString<VolumesPruneParameters>(parameters);
            return await this._client.MakeRequestAsync(this._client.NoErrorHandlers, HttpMethod.Post, "volumes/prune", queryParameters, DockerJsonSerializerContext.Default.VolumesPruneResponse, cancellationToken);
        }
    }
}