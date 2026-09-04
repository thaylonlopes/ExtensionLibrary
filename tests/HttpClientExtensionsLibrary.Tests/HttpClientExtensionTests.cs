using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using HttpClientExtensionsLibrary;
using Xunit;

namespace HttpClientExtensionsLibrary.Tests;

public class TestDelegatingHandler : HttpMessageHandler
{
    private int _invocationCount = 0;
    public int InvocationCount => _invocationCount;

    protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        _invocationCount++;
        if (_invocationCount < 2)
        {
            return Task.FromResult(new HttpResponseMessage(HttpStatusCode.InternalServerError));
        }
        return Task.FromResult(new HttpResponseMessage(HttpStatusCode.OK));
    }
}

public class HttpClientExtensionTests
{
    [Fact]
    public async Task SendWithRetryAsync_ShouldRetryWithFreshRequest()
    {
        var handler = new TestDelegatingHandler();
        var client = new HttpClient(handler);

        var response = await client.SendWithRetryAsync(() => new HttpRequestMessage(HttpMethod.Get, "http://localhost/test"), retryCount: 3);

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.Equal(2, handler.InvocationCount);
    }
}
