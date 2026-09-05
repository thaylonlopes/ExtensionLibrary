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

    [Fact]
    public void ToKeysetPagedList_FirstPage_ShouldReturnPageAndNextCursor()
    {
        var data = Enumerable.Range(1, 50).Select(i => new { Id = i, Name = $"Item {i}" });

        var page = data.ToKeysetPagedList(x => x.Id, pageSize: 10);

        Assert.Equal(10, page.Items.Count);
        Assert.Equal(1, page.Items[0].Id);
        Assert.Equal(10, page.Items[9].Id);
        Assert.True(page.HasNextPage);
        Assert.False(page.HasPreviousPage);
        Assert.Equal(10, page.NextCursor);
    }

    [Fact]
    public void ToKeysetPagedList_SecondPage_ShouldReturnNextItems()
    {
        var data = Enumerable.Range(1, 25).Select(i => new { Id = i, Name = $"Item {i}" });

        var page = data.ToKeysetPagedList(x => x.Id, cursor: 10, pageSize: 10, SeekDirection.Forward);

        Assert.Equal(10, page.Items.Count);
        Assert.Equal(11, page.Items[0].Id);
        Assert.Equal(20, page.Items[9].Id);
        Assert.True(page.HasNextPage);
        Assert.True(page.HasPreviousPage);
        Assert.Equal(20, page.NextCursor);
    }

    [Fact]
    public void ToKeysetPagedList_Backward_ShouldNavigateInReverseChronologicalOrder()
    {
        var data = Enumerable.Range(1, 50).Select(i => new { Id = i, Name = $"Item {i}" });

        var page = data.ToKeysetPagedList(x => x.Id, cursor: 21, pageSize: 10, SeekDirection.Backward);

        Assert.Equal(10, page.Items.Count);
        Assert.Equal(11, page.Items[0].Id);
        Assert.Equal(20, page.Items[9].Id);
    }

    [Fact]
    public void ToKeysetPagedList_InvalidPageSize_ShouldThrowArgumentOutOfRangeException()
    {
        var data = new[] { 1, 2, 3 };

        Assert.Throws<ArgumentOutOfRangeException>(() =>
            data.ToKeysetPagedList(x => x, pageSize: 0));
    }
}