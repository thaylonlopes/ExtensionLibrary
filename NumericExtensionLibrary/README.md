# 🔢 TL.NumericExtensionsLibrary

[![NuGet](https://img.shields.io/nuget/v/TL.NumericExtensionsLibrary.svg?style=flat-square&label=TL.NumericExtensionsLibrary)](https://www.nuget.org/packages/TL.NumericExtensionsLibrary/)
[![.NET](https://img.shields.io/badge/.NET-netstandard2.0%20%7C%20net8.0-blue.svg)](https://dotnet.microsoft.com/)
[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](../LICENSE.txt)
> **Lightweight numeric helpers for C#: value clamping, percentage calculations, and fluent math utilities for domain models and business logic.**  
> *Utilitários numéricos leves para C#: clamping (limites) de valores, cálculo de porcentagens e métodos matemáticos fluentes para regras de negócio.*

O **`TL.NumericExtensionsLibrary`** fornece uma suíte completa de métodos de extensão matemáticos, estatísticos e de conversão para tipos primitivos (`int`, `double` e `decimal`), com foco em zero alocação, previsibilidade de domínio e alta precisão para cenários financeiros e computação numérica.

---

## 📦 Instalação

Adicione o pacote ao seu projeto através do .NET CLI:

```bash
dotnet add package TL.NumericExtensionsLibrary
```

---

## 🚀 Funcionalidades Principais

### Extensões para `int`
| Método | Retorno | Descrição |
| :--- | :---: | :--- |
| `IsPrime()` | `bool` | Valida se o número inteiro é primo. |
| `IsEven()` / `IsOdd()` | `bool` | Determina se o número é par ou ímpar. |
| `Factorial()` | `int` | Calcula o fatorial com validação de limites de representação. |
| `IsPerfectSquare()` | `bool` | Verifica se o número é um quadrado perfeito. |
| `IsMultipleOf(divisor)` | `bool` | Verifica se o número é múltiplo do divisor informado. |
| `DigitSum()` | `int` | Calcula a soma absoluta de todos os algarismos. |
| `ReverseDigits()` | `int` | Inverte a ordem dos algarismos do número. |
| `GreatestCommonDivisor(b)` | `int` | Calcula o Máximo Divisor Comum (MDC / GCD). |
| `GreatestCommonMultiple(b)` | `int` | Calcula o Mínimo Múltiplo Comum (MMC / LCM). |
| `ToBinaryString()` | `string` | Converte o número para representação binária textual. |
| `ToHexString()` | `string` | Converte o número para representação hexadecimal. |

### Extensões para `double`
| Método | Retorno | Descrição |
| :--- | :---: | :--- |
| `ToRadians()` | `double` | Converte valores em graus para radianos. |
| `ToDegrees()` | `double` | Converte valores em radianos para graus. |
| `IsEven()` / `IsOdd()` | `bool` | Validação de paridade sobre a porção inteira do número. |

### Extensões para `decimal`
| Método | Retorno | Descrição |
| :--- | :---: | :--- |
| `Percentage(percentage)` | `decimal` | Calcula percentual mantendo exatidão de ponto fixo financeiro. |
| `WeightedAverage(numbers, weights)` | `decimal` | Calcula a média ponderada entre valores e pesos correspondentes. |
| `DigitSum()` | `int` | Soma absoluta dos algarismos presentes no valor decimal. |
| `ReverseDigits()` | `decimal` | Inverte os algarismos da porção numérica. |
| `Subtract(subtrahend)` | `decimal` | Subtração fluente com suporte a encadeamento de chamadas. |

---

## 💡 Exemplos de Uso

```csharp
using NumericExtensionLibrary;

public class CalculadoraFinanceira
{
    public void ExecutarExemplos()
    {
        // 1. Cálculos de Teoria dos Números
        int numero = 17;
        bool primo = numero.IsPrime(); // true
        int somaDigitos = 12345.DigitSum(); // 15
        int mdc = 24.GreatestCommonDivisor(36); // 12

        // 2. Cálculos Financeiros em Ponto Fixo
        decimal valorBase = 1500.00m;
        decimal desconto = valorBase.Percentage(10); // 150.00m
        decimal totalLiquido = valorBase.Subtract(desconto); // 1350.00m
    }
}
```

---

## 🏛️ Decisões Arquiteturais

Para detalhes sobre o rigor matemático, prevenção contra estouros aritméticos e generic math, consulte:
- 📄 [ADR-002: Decisões Arquiteturais do TL.NumericExtensionsLibrary](../docs/adr/ADR-002-numeric-extensions-library.md)

---

## 📄 Licença

Distribuído sob a licença [MIT](../LICENSE.txt).