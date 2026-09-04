using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Claims;
using AssemblyExtensionLibrary;
using ClaimsPrincipalExtensionsLibrary;
using CollectionExtensionsLibrary;
using DateTimeExtensionsLibrary;
using EnumExtensionsLibrary;
using NumericExtensionLibrary;
using ObjectExtensionsLibrary;
using StringExtensionLibrary;

namespace ExtensionLibrary.Showcase;

public enum OrderPriority
{
    [Description("Baixa Prioridade")]
    [EnumDescription(1, "Prioridade Nível 1 - SLA 72h")]
    Low = 1,

    [Description("Prioridade Padrão")]
    [EnumDescription(2, "Prioridade Nível 2 - SLA 24h")]
    Standard = 2,

    [Description("Alta Prioridade")]
    [EnumDescription(3, "Prioridade Nível 3 - SLA 4h")]
    High = 3
}

public class OrderModel
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public decimal Amount { get; set; }
}

public class Program
{
    public static void Main(string[] args)
    {
        DemonstrateKeysetPagination();
        DemonstrateClaimsPrincipal();
        DemonstrateDateTimeExtensions();
        DemonstrateStringExtensions();
        DemonstrateNumericExtensions();
        DemonstrateEnumExtensions();
        DemonstrateObjectExtensions();
    }

    private static void DemonstrateKeysetPagination()
    {
        var orders = Enumerable.Range(1, 25).Select(i => new OrderModel
        {
            Id = i,
            Title = $"Pedido #{i}",
            Amount = i * 15.50m
        }).ToList();

        var firstPage = orders.ToKeysetPagedList(o => o.Id, pageSize: 5);
        Console.WriteLine($"Keyset Page 1: {firstPage.Items.Count} itens, HasNext: {firstPage.HasNextPage}, NextCursor: {firstPage.NextCursor}");

        var secondPage = orders.ToKeysetPagedList(o => o.Id, cursor: firstPage.NextCursor, pageSize: 5, SeekDirection.Forward);
        Console.WriteLine($"Keyset Page 2: {secondPage.Items.Count} itens, FirstId: {secondPage.Items.First().Id}, LastId: {secondPage.Items.Last().Id}");
    }

    private static void DemonstrateClaimsPrincipal()
    {
        var identity = new ClaimsIdentity(new[]
        {
            new Claim("sub", "usr_1001"),
            new Claim("email", "dev@empresa.com"),
            new Claim("role", "Administrator"),
            new Claim("role", "Developer")
        });
        var principal = new ClaimsPrincipal(identity);

        Console.WriteLine($"Principal Sub: {principal.ClaimSub()}, Email: {principal.Email()}, Roles: {string.Join(", ", principal.ClaimRoles())}");
    }

    private static void DemonstrateDateTimeExtensions()
    {
        var startDate = new DateTime(2026, 9, 1);
        var targetDate = startDate.AddBusinessDays(5);
        int businessDays = startDate.BusinessDaysBetween(targetDate);

        Console.WriteLine($"DateTime: Início {startDate:yyyy-MM-dd}, +5 dias úteis: {targetDate:yyyy-MM-dd} (Total: {businessDays} dias úteis)");
    }

    private static void DemonstrateStringExtensions()
    {
        string text = "ExtensionLibrary fornece utilitarios para .NET";
        string truncated = text.Truncate(20);
        string leftPart = text.Left(16);
        int parsedNumber = "1024".ToIntOrDefault(defaultValue: 0);

        Console.WriteLine($"String: Truncate='{truncated}', Left='{leftPart}', ToIntOrDefault={parsedNumber}");
    }

    private static void DemonstrateNumericExtensions()
    {
        int number = 29;
        int digitSum = 9876.DigitSum();
        double percentage = 500.Percentage(15);

        Console.WriteLine($"Numeric: {number} é primo? {number.IsPrime()}, Soma dos dígitos de 9876: {digitSum}, 15% de 500: {percentage}");
    }

    private static void DemonstrateEnumExtensions()
    {
        var priority = OrderPriority.High;
        string defaultDescription = priority.GetDescription();
        string contextualDescription = priority.GetDescription(3);

        Console.WriteLine($"Enum: Descrição='{defaultDescription}', DescriçãoContextual='{contextualDescription}'");
    }

    private static void DemonstrateObjectExtensions()
    {
        var original = new OrderModel { Id = 42, Title = "Original", Amount = 199.90m };
        var clone = original.Clone();
        clone.Title = "Clonado";

        Console.WriteLine($"Object: Original='{original.Title}', Clone='{clone.Title}'");
    }
}

