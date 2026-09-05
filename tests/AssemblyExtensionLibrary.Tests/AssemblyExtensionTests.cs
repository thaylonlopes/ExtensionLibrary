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

    [Fact]
    public void GetVersion_ShouldReturnAssemblyVersion()
    {
        var assembly = typeof(AssemblyExtensionTests).Assembly;
        var version = assembly.GetVersion();

        Assert.NotNull(version);
    }

    [Fact]
    public void GetLoadableTypes_WhenNull_ShouldReturnEmptyEnumerable()
    {
        Assembly? nullAssembly = null;
        var types = nullAssembly!.GetLoadableTypes();

        Assert.Empty(types);
    }

  [Fact]
    public void GetEntryAssembly_ShouldExecuteSafely()
    {
        var exception = Record.Exception(() =>
        {
            var entry = AssemblyExtension.GetEntryAssembly();
            if (entry != null)
            {
                _ = entry.FullName;
            }
        });

        Assert.Null(exception);
    }
}