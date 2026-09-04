using System;
using System.Collections.Generic;
using System.Linq;
using QueryableExtensionsLibrary;
using Xunit;

namespace QueryableExtensionsLibrary.Tests;

public class Product
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
}

public class QueryableExtensionTests
{
    [Fact]
    public void Filter_ShouldFilterByValidProperty()
    {
        var query = new List<Product>
        {
            new() { Id = 1, Name = "Notebook", Price = 3000 },
            new() { Id = 2, Name = "Mouse", Price = 100 }
        }.AsQueryable();

        var result = query.Filter("Price", ">=", 1000).ToList();

        Assert.Single(result);
        Assert.Equal("Notebook", result[0].Name);
    }

    [Fact]
    public void Filter_ShouldThrowOnInvalidProperty()
    {
        var query = new List<Product> { new() { Id = 1, Name = "Item" } }.AsQueryable();

        Assert.Throws<ArgumentException>(() => query.Filter("PropriedadeInexistente", "Valor"));
    }
}