using System;
using System.ComponentModel;
using EnumExtensionsLibrary;
using Xunit;

namespace EnumExtensionsLibrary.Tests;

public enum OrderState
{
    [Description("Pedido Criado")]
    [EnumDescription(101, "Código 101 - Pedido Criado")]
    Created = 1,

    [Description("Pedido Pago")]
    [EnumDescription(102, "Código 102 - Pedido Pago")]
    Paid = 2,

    // Sem atributo de descrição
    Shipped = 3
}

public class EnumExtensionTests
{
    [Fact]
    public void GetDescription_ShouldReturnAttributeValue()
    {
        Assert.Equal("Pedido Criado", OrderState.Created.GetDescription());
        Assert.Equal("Pedido Pago", OrderState.Paid.GetDescription());
    }

    [Fact]
    public void GetDescription_WithoutAttribute_ShouldFallbackToEnumName()
    {
        Assert.Equal("Shipped", OrderState.Shipped.GetDescription());
    }

    [Fact]
    public void GetDescription_ByKey_ShouldReturnKeyedDescription()
    {
        Assert.Equal("Código 101 - Pedido Criado", OrderState.Created.GetDescription(101));
        Assert.Equal("Shipped", OrderState.Shipped.GetDescription(999));
    }

    [Fact]
    public void GetDescriptionByKeyOrDefault_ShouldReturnDefaultWhenNotFound()
    {
        var result = OrderState.Created.GetDescriptionByKeyOrDefault(999, "Padrão");
        Assert.Equal("Padrão", result);
    }

    [Fact]
    public void HasKey_ShouldReturnTrueForExistingKey()
    {
        Assert.True(OrderState.Created.HasKey(101));
        Assert.False(OrderState.Created.HasKey(999));
    }

    [Fact]
    public void ToDictionary_ShouldContainAllValues()
    {
        var dict = EnumExtension.ToDictionary<OrderState>();
        Assert.Equal(3, dict.Count);
        Assert.Equal("Created", dict[1]);
        Assert.Equal("Paid", dict[2]);
        Assert.Equal("Shipped", dict[3]);
    }

    [Fact]
    public void GetEnumByDescription_ShouldReturnCorrectEnumOrThrow()
    {
        var item = EnumExtension.GetEnumByDescription<OrderState>("Pedido Criado");
        Assert.Equal(OrderState.Created, item);

        Assert.Throws<ArgumentException>(() =>
            EnumExtension.GetEnumByDescription<OrderState>("Descrição Inexistente"));
    }

    [Fact]
    public void TryParse_ShouldHandleValidAndInvalidValues()
    {
        Assert.True(EnumExtension.TryParse<OrderState>("Created", out var valid));
        Assert.Equal(OrderState.Created, valid);

        Assert.False(EnumExtension.TryParse<OrderState>("InvalidValue", out _));
    }
}