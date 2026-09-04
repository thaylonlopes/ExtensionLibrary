using System.Security.Claims;
using ClaimsPrincipalExtensionsLibrary;
using Xunit;

namespace ClaimsPrincipalExtensionsLibrary.Tests;

public class ClaimsPrincipalExtensionTests
{
    [Fact]
    public void ClaimSub_ShouldSupportJwtAndWsFed()
    {
        var jwtPrincipal = new ClaimsPrincipal(new ClaimsIdentity(new[] { new Claim("sub", "user-123") }));
        var wsFedPrincipal = new ClaimsPrincipal(new ClaimsIdentity(new[] { new Claim(ClaimTypes.NameIdentifier, "user-456") }));

        Assert.Equal("user-123", jwtPrincipal.ClaimSub());
        Assert.Equal("user-456", wsFedPrincipal.ClaimSub());
    }

    [Fact]
    public void Email_ShouldSupportJwtAndWsFed()
    {
        var jwtPrincipal = new ClaimsPrincipal(new ClaimsIdentity(new[] { new Claim("email", "dev@example.com") }));
        var wsFedPrincipal = new ClaimsPrincipal(new ClaimsIdentity(new[] { new Claim(ClaimTypes.Email, "corp@example.com") }));

        Assert.Equal("dev@example.com", jwtPrincipal.Email());
        Assert.Equal("corp@example.com", wsFedPrincipal.Email());
    }
}