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
}