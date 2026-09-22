using System;
using DateTimeExtensionsLibrary;
using Xunit;

namespace DateTimeExtensionsLibrary.Tests;

public class DateTimeExtensionsTestsUtcEpoch
{
    [Fact]
    public void EnsureUtc_ShouldReturnSameInstanceWhenAlreadyUtc()
    {
        var utcDate = new DateTime(2026, 9, 16, 12, 0, 0, DateTimeKind.Utc);
        var result = utcDate.EnsureUtc();

        Assert.Equal(DateTimeKind.Utc, result.Kind);
        Assert.Equal(utcDate, result);
    }

    [Fact]
    public void EnsureUtc_ShouldConvertCorrectlyWhenKindIsLocal()
    {
        var localDate = new DateTime(2026, 9, 16, 12, 0, 0, DateTimeKind.Local);
        var result = localDate.EnsureUtc();

        Assert.Equal(DateTimeKind.Utc, result.Kind);
        Assert.Equal(localDate.ToUniversalTime(), result);
    }

    [Fact]
    public void EnsureUtc_ShouldSpecifyKindUtcWithoutModifyingTicksWhenKindIsUnspecified()
    {
        var unspecifiedDate = new DateTime(2026, 9, 16, 12, 0, 0, DateTimeKind.Unspecified);
        var result = unspecifiedDate.EnsureUtc();

        Assert.Equal(DateTimeKind.Utc, result.Kind);
        Assert.Equal(unspecifiedDate.Ticks, result.Ticks);
    }

    [Fact]
    public void UnixTimeMilliseconds_ShouldProvideAccurateBidirectionalConversion()
    {
        var epochOrigin = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        long epochOriginMs = epochOrigin.ToUnixTimeMilliseconds();

        Assert.Equal(0L, epochOriginMs);
        Assert.Equal(epochOrigin, epochOriginMs.FromUnixTimeMilliseconds());

        var knownDate = new DateTime(2026, 9, 16, 9, 45, 47, DateTimeKind.Utc);
        long ms = knownDate.ToUnixTimeMilliseconds();

        var roundTrip = ms.FromUnixTimeMilliseconds();
        Assert.Equal(knownDate, roundTrip);
        Assert.Equal(DateTimeKind.Utc, roundTrip.Kind);
    }

    [Fact]
    public void StartOfMonth_ShouldReturnFirstDayAtMidnightPreservingKind()
    {
        var dateUtc = new DateTime(2026, 9, 16, 15, 30, 45, DateTimeKind.Utc);
        var startUtc = dateUtc.StartOfMonth();

        Assert.Equal(new DateTime(2026, 9, 1, 0, 0, 0, 0, DateTimeKind.Utc), startUtc);
        Assert.Equal(DateTimeKind.Utc, startUtc.Kind);

        var dateLocal = new DateTime(2026, 9, 16, 15, 30, 45, DateTimeKind.Local);
        var startLocal = dateLocal.StartOfMonth();

        Assert.Equal(new DateTime(2026, 9, 1, 0, 0, 0, 0, DateTimeKind.Local), startLocal);
        Assert.Equal(DateTimeKind.Local, startLocal.Kind);
    }

    [Fact]
    public void EndOfMonth_ShouldReturnLastDayAtLastMillisecondPreservingKind()
    {
        var dateSeptember = new DateTime(2026, 9, 5, 10, 0, 0, DateTimeKind.Utc);
        var endSeptember = dateSeptember.EndOfMonth();

        Assert.Equal(new DateTime(2026, 9, 30, 23, 59, 59, 999, DateTimeKind.Utc), endSeptember);
        Assert.Equal(DateTimeKind.Utc, endSeptember.Kind);
    }

    [Fact]
    public void EndOfMonth_ShouldCorrectlyCalculateFebruaryInLeapYear()
    {
        var leapFeb = new DateTime(2024, 2, 10, 8, 0, 0, DateTimeKind.Utc);
        var endLeapFeb = leapFeb.EndOfMonth();

        Assert.Equal(29, endLeapFeb.Day);
        Assert.Equal(new DateTime(2024, 2, 29, 23, 59, 59, 999, DateTimeKind.Utc), endLeapFeb);
    }

    [Fact]
    public void EndOfMonth_ShouldCorrectlyCalculateFebruaryInNonLeapYear()
    {
        var nonLeapFeb = new DateTime(2025, 2, 10, 8, 0, 0, DateTimeKind.Utc);
        var endNonLeapFeb = nonLeapFeb.EndOfMonth();

        Assert.Equal(28, endNonLeapFeb.Day);
        Assert.Equal(new DateTime(2025, 2, 28, 23, 59, 59, 999, DateTimeKind.Utc), endNonLeapFeb);
    }
}
