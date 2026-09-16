using System;
using System.Text;
using StringExtensionLibrary;
using Xunit;

namespace StringExtensionLibrary.Tests;

public class StringExtensionsTestsAppSec
{
    [Fact]
    public void SanitizeForLog_ShouldReplaceCarriageReturnAndNewlineWithUnderscore()
    {
        string maliciousInput = "admin\r\nLOGIN_SUCCESS\r\nrole=superuser";
        string sanitized = maliciousInput.SanitizeForLog();

        Assert.Equal("admin__LOGIN_SUCCESS__role=superuser", sanitized);
        Assert.DoesNotContain("\r", sanitized);
        Assert.DoesNotContain("\n", sanitized);
    }

    [Fact]
    public void SanitizeForLog_ShouldReturnOriginalInputWhenNullOrEmpty()
    {
        string? nullInput = null;
        string emptyInput = string.Empty;

        Assert.Null(nullInput.SanitizeForLog());
        Assert.Equal(string.Empty, emptyInput.SanitizeForLog());
    }

    [Fact]
    public void SanitizeForLog_ShouldPreserveCleanStringWithoutNewlines()
    {
        string cleanInput = "safe_user_input_12345";
        Assert.Equal("safe_user_input_12345", cleanInput.SanitizeForLog());
    }

    [Theory]
    [InlineData("thaylon@empresa.com", "t*****n@empresa.com")]
    [InlineData("joao.silva@dominio.org", "j********a@dominio.org")]
    [InlineData("ab@empresa.com", "a*@empresa.com")]
    [InlineData("a@empresa.com", "*@empresa.com")]
    public void MaskEmail_ShouldMaskUsernamePreservingExtremitiesAndDomain(string email, string expected)
    {
        Assert.Equal(expected, email.MaskEmail());
    }

    [Fact]
    public void MaskEmail_ShouldHandleNullEmptyAndNonEmailGracefully()
    {
        string? nullEmail = null;
        Assert.Null(nullEmail.MaskEmail());
        Assert.Equal(string.Empty, string.Empty.MaskEmail());
        Assert.Equal("invalid_email_string", "invalid_email_string".MaskEmail());
        Assert.Equal("@domainonly.com", "@domainonly.com".MaskEmail());
    }

    [Fact]
    public void Mask_ShouldMaskMiddleCharactersPreservingPrefixAndSuffix()
    {
        string cardNumber = "1234567812345678";
        string masked = cardNumber.Mask(4, 4, '*');

        Assert.Equal("1234********5678", masked);
    }

    [Fact]
    public void Mask_ShouldReturnEntirelyMaskedStringWhenVisibleLengthExceedsTotalLength()
    {
        string shortToken = "12345";
        string masked = shortToken.Mask(3, 3, '*');

        Assert.Equal("*****", masked);
    }

    [Fact]
    public void Mask_ShouldHandleBoundaryConditionsSafely()
    {
        string? nullInput = null;
        Assert.Null(nullInput.Mask(2, 2));

        string emptyInput = string.Empty;
        Assert.Equal(string.Empty, emptyInput.Mask(2, 2));

        string exactLength = "1234";
        Assert.Equal("****", exactLength.Mask(2, 2));
    }

    [Fact]
    public void TruncateWithEllipsis_ShouldTruncateWhenExceedsMaxCharacters()
    {
        string text = "TL.ExtensionLibrary Governança e Alta Performance";
        string truncated = text.TruncateWithEllipsis(20, "...");

        Assert.Equal(20, truncated.Length);
        Assert.Equal("TL.ExtensionLibra...", truncated);
    }

    [Fact]
    public void TruncateWithEllipsis_ShouldReturnOriginalStringWhenWithinLimit()
    {
        string text = "Curto";
        Assert.Equal("Curto", text.TruncateWithEllipsis(10));
    }

    [Fact]
    public void TruncateWithEllipsis_ShouldHandleEdgeCasesWhenMaxCharactersLessThanOrEqualToEllipsisLength()
    {
        string text = "LongaDescricaoDeExemplo";
        string truncated = text.TruncateWithEllipsis(2, "...");

        Assert.Equal("..", truncated);
        Assert.Equal(2, truncated.Length);
    }

    [Fact]
    public void TruncateWithEllipsis_ShouldHandleNullAndZeroMaxCharacters()
    {
        string? nullInput = null;
        Assert.Null(nullInput.TruncateWithEllipsis(10));

        string text = "QualquerTexto";
        Assert.Equal(string.Empty, text.TruncateWithEllipsis(0));
        Assert.Equal(string.Empty, text.TruncateWithEllipsis(-5));
    }

    [Fact]
    public void TryFromBase64_ShouldDecodeValidBase64Successfully()
    {
        string rawText = "TL.ExtensionLibrary-v0.5.0";
        string base64 = Convert.ToBase64String(Encoding.UTF8.GetBytes(rawText));

        bool success = base64.TryFromBase64(out byte[] bytes);

        Assert.True(success);
        Assert.NotNull(bytes);
        Assert.Equal(rawText, Encoding.UTF8.GetString(bytes));
    }

    [Fact]
    public void TryFromBase64_ShouldFailGracefullyOnInvalidBase64WithoutThrowing()
    {
        string invalidBase64 = "NãoÉUmBase64Válido!@#$";

        bool success = invalidBase64.TryFromBase64(out byte[] bytes);

        Assert.False(success);
        Assert.NotNull(bytes);
        Assert.Empty(bytes);
    }

    [Fact]
    public void TryFromBase64_ShouldFailGracefullyOnNullOrEmptyInput()
    {
        string? nullInput = null;
        bool nullSuccess = nullInput.TryFromBase64(out byte[] nullBytes);
        Assert.False(nullSuccess);
        Assert.Empty(nullBytes);

        bool emptySuccess = string.Empty.TryFromBase64(out byte[] emptyBytes);
        Assert.False(emptySuccess);
        Assert.Empty(emptyBytes);
    }
}
