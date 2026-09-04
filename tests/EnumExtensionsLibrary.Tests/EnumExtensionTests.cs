using System.ComponentModel;
using EnumExtensionsLibrary;
using Xunit;

namespace EnumExtensionsLibrary.Tests;

public enum OrderState
{
    [Description("Pedido Criado")]
    Created = 1,
    [Description("Pedido Pago")]
    Paid = 2
}

public class EnumExtensionTests
{
    [Fact]
    public void GetDescription_ShouldReturnAttributeValue()
    {
        Assert.Equal("Pedido Criado", OrderState.Created.GetDescription());
        Assert.Equal("Pedido Pago", OrderState.Paid.GetDescription());
    }
}