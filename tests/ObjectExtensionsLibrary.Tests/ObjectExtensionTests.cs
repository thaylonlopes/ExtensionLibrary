using System;
using ObjectExtensionsLibrary;
using Xunit;

namespace ObjectExtensionsLibrary.Tests;

public class SampleService
{
    public string Echo(string message) => message;
    public void ThrowError() => throw new ArgumentException("Erro de teste disparado.");
}

public class ObjectExtensionTests
{
    [Fact]
    public void InvokeMethod_ShouldExecuteExistingMethod()
    {
        var service = new SampleService();
        var result = service.InvokeMethod("Echo", "teste");

        Assert.Equal("teste", result);
    }

    [Fact]
    public void InvokeMethod_ShouldPreserveOriginalException()
    {
        var service = new SampleService();

        var ex = Assert.Throws<ArgumentException>(() => service.InvokeMethod("ThrowError"));
        Assert.Equal("Erro de teste disparado.", ex.Message);
    }
}