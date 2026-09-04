using System;
using DateTimeExtensionsLibrary;
using Xunit;

namespace DateTimeExtensionsLibrary.Tests;

public class DateTimeExtensionTests
{
    [Fact]
    public void BusinessDaysBetween_ShouldCalculateCorrectly()
    {
        var start = new DateTime(2026, 9, 1); // Terça-feira
        var end = new DateTime(2026, 9, 7);   // Segunda-feira seguinte
        var start = new DateTime(2026, 9, 1);
        var end = new DateTime(2026, 9, 7);

        int businessDays = start.BusinessDaysBetween(end);
        Assert.Equal(5, businessDays);
    }

    [Fact]
    public void DaysUntil_ShouldCalculateRemainingDays()
    {
        var today = new DateTime(2026, 9, 1);
        var future = new DateTime(2026, 9, 10);

        Assert.Equal(9, today.DaysUntil(future));
    }

    [Fact]
    public void IsWeekend_And_IsBusinessDay_ShouldIdentifyCorrectly()
    {
        var saturday = new DateTime(2026, 9, 5);
        var sunday = new DateTime(2026, 9, 6);
        var monday = new DateTime(2026, 9, 7);

        Assert.True(saturday.IsWeekend());
        Assert.True(sunday.IsWeekend());
        Assert.False(monday.IsWeekend());

        Assert.False(saturday.IsBusinessDay());
        Assert.True(monday.IsBusinessDay());
    }

    [Fact]
    public void IsLeapYear_ShouldIdentifyLeapYears()
    {
        var leap = new DateTime(2024, 1, 1);
        var nonLeap = new DateTime(2025, 1, 1);

        Assert.True(leap.IsLeapYear());
        Assert.False(nonLeap.IsLeapYear());
    }

    [Fact]
    public void Chunks_ShouldSplitRangeCorrectly()
    {
        var start = new DateTime(2026, 9, 1);
        var end = new DateTime(2026, 9, 15);

        var chunks = start.Chunks(end, days: 5);
        Assert.NotEmpty(chunks);
        Assert.Equal(start.Date, chunks[0].Item1.Date);
    }
}