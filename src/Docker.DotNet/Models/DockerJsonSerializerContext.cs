using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Docker.DotNet.Models;

[JsonSerializable(typeof(IList<string>))]
[JsonSerializable(typeof(AuthConfig))]
[JsonSerializable(typeof(Dictionary<string, AuthConfig>))]

[JsonSerializable(typeof(ContainerStatsResponse))]
[JsonSerializable(typeof(JSONMessage))]
[JsonSerializable(typeof(Message))]

[JsonSerializable(typeof(SwarmConfig[]))]
[JsonSerializable(typeof(SwarmConfigSpec))]
[JsonSerializable(typeof(SwarmCreateConfigResponse))]

[JsonSerializable(typeof(ContainerListResponse[]))]
[JsonSerializable(typeof(CreateContainerParameters))]
[JsonSerializable(typeof(CreateContainerResponse))]
[JsonSerializable(typeof(ContainerInspectResponse))]
[JsonSerializable(typeof(ContainerProcessesResponse))]
[JsonSerializable(typeof(ContainerFileSystemChangeResponse[]))]
[JsonSerializable(typeof(ContainerWaitResponse))]
[JsonSerializable(typeof(GetArchiveFromContainerResponse))]
[JsonSerializable(typeof(ContainerPathStatResponse))]
[JsonSerializable(typeof(ContainersPruneResponse))]
[JsonSerializable(typeof(ContainerUpdateParameters))]
[JsonSerializable(typeof(ContainerUpdateResponse))]

[JsonSerializable(typeof(ContainerExecCreateResponse))]
[JsonSerializable(typeof(ContainerExecInspectResponse))]

[JsonSerializable(typeof(ImagesListResponse[]))]
[JsonSerializable(typeof(ImageInspectResponse))]
[JsonSerializable(typeof(ImageHistoryResponse[]))]
[JsonSerializable(typeof(Dictionary<string, string>[]))]
[JsonSerializable(typeof(ImageSearchResponse[]))]
[JsonSerializable(typeof(ImagesPruneResponse))]
[JsonSerializable(typeof(CommitContainerChangesParameters))]
[JsonSerializable(typeof(CommitContainerChangesResponse))]
[JsonSerializable(typeof(ContainerExecCreateParameters))]
[JsonSerializable(typeof(ContainerExecStartParameters))]

[JsonSerializable(typeof(NetworkResponse[]))]
[JsonSerializable(typeof(NetworksCreateParameters))]
[JsonSerializable(typeof(NetworkConnectParameters))]
[JsonSerializable(typeof(NetworkDisconnectParameters))]
[JsonSerializable(typeof(NetworksCreateResponse))]
[JsonSerializable(typeof(NetworksPruneResponse))]

[JsonSerializable(typeof(Plugin[]))]
[JsonSerializable(typeof(PluginPrivilege[]))]
[JsonSerializable(typeof(IList<PluginPrivilege>))]

[JsonSerializable(typeof(Secret[]))]
[JsonSerializable(typeof(SecretSpec))]
[JsonSerializable(typeof(SecretCreateResponse))]

[JsonSerializable(typeof(SwarmInitParameters))]
[JsonSerializable(typeof(SwarmJoinParameters))]
[JsonSerializable(typeof(SwarmUnlockParameters))]
[JsonSerializable(typeof(Spec))]
[JsonSerializable(typeof(ServiceCreateResponse))]
[JsonSerializable(typeof(SwarmUnlockResponse))]
[JsonSerializable(typeof(SwarmService[]))]
[JsonSerializable(typeof(SwarmInspectResponse))]
[JsonSerializable(typeof(ServiceUpdateResponse))]
[JsonSerializable(typeof(NodeUpdateParameters))]
[JsonSerializable(typeof(NodeListResponse[]))]

[JsonSerializable(typeof(VersionResponse))]
[JsonSerializable(typeof(SystemInfoResponse))]

[JsonSerializable(typeof(TaskResponse[]))]

[JsonSerializable(typeof(VolumesListResponse))]
[JsonSerializable(typeof(VolumesCreateParameters))]
[JsonSerializable(typeof(VolumeResponse))]
[JsonSerializable(typeof(VolumesPruneResponse))]

[JsonSourceGenerationOptions(Converters = [
    typeof(JsonEnumMemberConverter<TaskState>),
    typeof(JsonEnumMemberConverter<RestartPolicyKind>),
    typeof(JsonDateTimeConverter),
    typeof(JsonNullableDateTimeConverter),
    typeof(JsonBase64Converter),
])]
internal partial class DockerJsonSerializerContext : JsonSerializerContext;