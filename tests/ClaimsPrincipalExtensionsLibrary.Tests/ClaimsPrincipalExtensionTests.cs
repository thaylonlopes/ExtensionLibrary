using System.Linq;
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

    [Fact]
    public void ClaimRoles_ShouldReturnAllRoles()
    {
        var identity = new ClaimsIdentity(new[]
        {
            new Claim("role", "Admin"),
            new Claim("role", "User")
        });
        var principal = new ClaimsPrincipal(identity);

        var roles = principal.ClaimRoles().ToList();
        Assert.Equal(2, roles.Count);
        Assert.Contains("Admin", roles);
        Assert.Contains("User", roles);
    }

    [Fact]
    public void AllClaims_WithEmptyPrincipal_ShouldReturnEmptyEnumerable()
    {
        var principal = new ClaimsPrincipal(new ClaimsIdentity());
        var claims = principal.AllClaims();
        Assert.Empty(claims);
    }

    [Fact]
    public void ClaimSub_WhenMissing_ShouldReturnNull()
    {
        var principal = new ClaimsPrincipal(new ClaimsIdentity());
        Assert.Null(principal.ClaimSub());
    }
}