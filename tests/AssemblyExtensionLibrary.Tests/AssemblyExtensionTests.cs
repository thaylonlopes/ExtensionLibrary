using System;
using System.Reflection;
using AssemblyExtensionLibrary;
using Xunit;

namespace AssemblyExtensionLibrary.Tests;

public class AssemblyExtensionTests
{
    [Fact]
    public void FileInfo_ShouldHandleAssemblySafely()
    {
        var assembly = typeof(AssemblyExtensionTests).Assembly;
        var fileInfo = assembly.FileInfo();

        Assert.True(fileInfo != null || string.IsNullOrWhiteSpace(assembly.Location));
    }

    [Fact]
    public void GetLoadableTypes_ShouldReturnTypesWithoutException()
    {
        var assembly = typeof(AssemblyExtensionTests).Assembly;
        var types = assembly.GetLoadableTypes();

        Assert.NotEmpty(types);
    }
}