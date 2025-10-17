using System.Text.Json;
using System.Text.Json.Serialization.Metadata;

namespace Docker.DotNet.Tests;

public sealed class JsonRequestContentTests
{
    [Fact]
    public void Constructor_ThrowsArgumentNullException_WhenValueIsNull()
    {
        Assert.Throws<ArgumentNullException>(() => JsonRequestContent.Create(JsonTypeInfo.CreateJsonTypeInfo<object>(JsonSerializerOptions.Default), null));
    }

    [Fact]
    public void Constructor_ThrowsArgumentNullException_WhenJsonTypeInfoIsNull()
    {
        Assert.Throws<ArgumentNullException>(() => JsonRequestContent.Create(null, new object()));
    }

    [Fact]
    public async Task GetContent_Succeeds_WhenValueAndSerializerAreValid()
    {
        var content = JsonRequestContent.Create(DockerClientSerializerContext.Default.UInt64Array, new[] { 1UL });
        using var httpContent = content.GetContent();
        var jsonString = await httpContent.ReadAsStringAsync();
        Assert.Equal("[1]", jsonString);
    }
}