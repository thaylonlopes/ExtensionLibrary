using System;
using System.Collections.Generic;
using System.Linq;
using CollectionExtensionsLibrary;
using Xunit;

namespace CollectionExtensionsLibrary.Tests;

public class CollectionExtensionTests
{
    [Fact]
    public void ChunkBy_ShouldPartitionSequenceLinearly()
    {
        var numbers = Enumerable.Range(1, 10).ToList();
        var chunks = numbers.ChunkBy(3).ToList();

        Assert.Equal(4, chunks.Count);
        Assert.Equal(new[] { 1, 2, 3 }, chunks[0]);
        Assert.Equal(new[] { 4, 5, 6 }, chunks[1]);
        Assert.Equal(new[] { 7, 8, 9 }, chunks[2]);
        Assert.Equal(new[] { 10 }, chunks[3]);
    }

    [Fact]
    public void AddRangeIfNotExists_ShouldNotDuplicateElements()
    {
        var list = new List<int> { 1, 2, 3 };
        list.AddRangeIfNotExists(new[] { 2, 3, 4, 5 });

        Assert.Equal(new[] { 1, 2, 3, 4, 5 }, list);
    }

    [Fact]
    public void Replace_ShouldReplaceValuesSafely()
    {
        var list = new List<string> { "a", "b", "a" };
        list.Replace("a", "z");

        Assert.Equal(new[] { "z", "b", "z" }, list);
    }
}