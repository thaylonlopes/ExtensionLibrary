# 🚀 TL.UtilExtensions (ExtensionLibrary)

[![.NET](https://img.shields.io/badge/.NET-netstandard2.0%20%7C%20net5.0%20%7C%20net6.0%20%7C%20net8.0-blue.svg)](https://dotnet.microsoft.com/)
[![NuGet Profile](https://img.shields.io/badge/NuGet-ThaylonMALopes-004880.svg?logo=nuget)](https://www.nuget.org/profiles/ThaylonMALopes)
[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://opensource.org/licenses/MIT)
[![Architecture: ADRs](https://img.shields.io/badge/ADRs-10%20Decisões%20Arquiteturais-success.svg)](./docs/adr/)

Bem-vindo ao ecossistema **TL.UtilExtensions** (solução `ExtensionLibrary.sln`)! Esta suíte modular de bibliotecas em C# / .NET disponibiliza métodos de extensão utilitários de alta performance, projetados para simplificar o desenvolvimento diário, eliminar boilerplate, assegurar pureza funcional e manter seu código limpo e idiomático.

Cada módulo é empacotado e distribuído de forma **independente no NuGet** sob o prefixo `TL.*`, permitindo que você importe estritamente as extensões necessárias sem carregar dependências desnecessárias.

---

## 📦 Catálogo de Módulos NuGet & Documentação

| Pacote NuGet | Runtimes Suportados | Dependências | Guia do Pacote | Decisão Arquitetural |
| :--- | :--- | :---: | :---: | :---: |
| **`TL.StringExtensionsLibrary`** | `netstandard2.0`<br/>`net5.0`<br/>`net6.0`<br/>`net8.0` | `Newtonsoft.Json` | [README](./StringExtensionLibrary/README.md) | [ADR-001](./docs/adr/ADR-001-string-extensions-library.md) |
| **`TL.NumericExtensionsLibrary`** | `netstandard2.0`<br/>`net5.0`<br/>`net6.0`<br/>`net8.0` | **Zero (BCL pura)** | [README](./NumericExtensionLibrary/README.md) | [ADR-002](./docs/adr/ADR-002-numeric-extensions-library.md) |
| **`TL.EnumExtensionsLibrary`** | `netstandard2.0`<br/>`net5.0`<br/>`net6.0`<br/>`net8.0` | **Zero (BCL pura)** | [README](./EnumExtensionsLibrary/README.md) | [ADR-003](./docs/adr/ADR-003-enum-extensions-library.md) |
| **`TL.ClaimsPrincipalExtensionsLibrary`** | `net5.0`<br/>`net6.0`<br/>`net8.0` | **Zero (BCL pura)** | [README](./ClaimsPrincipalExtensionsLibrary/README.md) | [ADR-004](./docs/adr/ADR-004-claims-principal-extensions-library.md) |
| **`TL.AssemblyExtensionLibrary`** | `net8.0` | **Zero (BCL pura)** | [README](./AssemblyExtensionLibrary/README.md) | [ADR-005](./docs/adr/ADR-005-assembly-extension-library.md) |
| **`TL.DateTimeExtensionsLibrary`** | `net5.0`<br/>`net6.0`<br/>`net8.0` | **Zero (BCL pura)** | [README](./DateTimeExtensionsLibrary/README.md) | [ADR-006](./docs/adr/ADR-006-date-time-extensions-library.md) |
| **`TL.CollectionExtensionsLibrary`** | `net5.0`<br/>`net6.0`<br/>`net8.0` | **Zero (BCL pura)** | [README](./CollectionExtensionsLibrary/README.md) | [ADR-007](./docs/adr/ADR-007-collection-extensions-library.md) |
| **`TL.HttpClientExtensionsLibrary`** | `net5.0`<br/>`net6.0`<br/>`net8.0` | `Newtonsoft.Json`<br/>`Jwt` | [README](./HttpClientExtensionsLibrary/README.md) | [ADR-008](./docs/adr/ADR-008-http-client-extensions-library.md) |
| **`TL.ObjectExtensionsLibrary`** | `net5.0`<br/>`net6.0`<br/>`net8.0` | **Zero (BCL pura)** | [README](./ObjectExtensionsLibrary/README.md) | [ADR-009](./docs/adr/ADR-009-object-extensions-library.md) |
| **`TL.QueryableExtensionsLibrary`** | `net5.0`<br/>`net6.0`<br/>`net8.0` | **Zero (BCL pura)** | [README](./QueryableExtensionsLibrary/README.md) | [ADR-010](./docs/adr/ADR-010-queryable-extensions-library.md) |

---

## ⚡ Instalação Rápida via CLI

Escolha os módulos desejados e instale via .NET CLI:

```bash
# Manipulação de Strings
dotnet add package TL.StringExtensionsLibrary

# Cálculos Numéricos e Matemáticos
dotnet add package TL.NumericExtensionsLibrary

# Utilitários de Enum e Atributos
dotnet add package TL.EnumExtensionsLibrary

# Autenticação e ClaimsPrincipal
dotnet add package TL.ClaimsPrincipalExtensionsLibrary

# Diagnóstico de Assemblies e Reflexão
dotnet add package TL.AssemblyExtensionLibrary

# Operações de Data e Calendário
dotnet add package TL.DateTimeExtensionsLibrary

# Coleções, Particionamento e LINQ
dotnet add package TL.CollectionExtensionsLibrary

# Cliente HTTP Resiliente e Tokens
dotnet add package TL.HttpClientExtensionsLibrary

# Clonagem e Utilitários Genéricos de Objetos
dotnet add package TL.ObjectExtensionsLibrary

# Filtros e Ordenação Dinâmica em IQueryable
dotnet add package TL.QueryableExtensionsLibrary
```

---

## 💻 Exemplos Práticos de Uso

### 1. `NumericExtensionsLibrary`
```csharp
using NumericExtensionLibrary;

int numero = 17;
Console.WriteLine($"É primo? {numero.IsPrime()}"); // True
Console.WriteLine($"É par? {numero.IsEven()}");    // False
Console.WriteLine($"Fatorial de 5: {5.Factorial()}"); // 120

decimal valor = 250.00m;
decimal taxa = valor.Percentage(15.0m); // Calcula 15% de 250 -> 37.50
```

### 2. `StringExtensionLibrary`
```csharp
using StringExtensionLibrary;

string entrada = "true";
bool ativo = entrada.ToBoolean(); // true

string textoLongo = "Este é um texto corporativo de auditoria de segurança.";
string resumo = textoLongo.TruncateWithEllipsis(25); // "Este é um texto corpor..."

string dataString = "2026-09-03";
bool ehDataValida = dataString.IsDateTime("yyyy-MM-dd"); // true
```

### 3. `EnumExtensionsLibrary`
```csharp
using EnumExtensionsLibrary;

public enum Prioridade
{
    [System.ComponentModel.Description("Alta Urgência")]
    Urgente = 1,
    [System.ComponentModel.Description("Normal")]
    Padrao = 2
}

var descricao = Prioridade.Urgente.GetDescription(); // "Alta Urgência"
Dictionary<int, string> catalogo = Prioridade.Urgente.ToDictionary();
```

### 4. `CollectionExtensionsLibrary`
```csharp
using CollectionExtensionsLibrary;

var lista = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

// Particionamento em lotes (batching)
var lotes = lista.ChunkBy(3); // [1,2,3], [4,5,6], [7,8,9], [10]

// Embaralhamento seguro
var embaralhado = lista.Shuffle();
```

### 5. `QueryableExtensionsLibrary`
```csharp
using QueryableExtensionsLibrary;

IQueryable<Produto> produtos = dbContext.Produtos.AsQueryable();

// Filtro e ordenação dinâmicos a partir de strings da requisição
var resultado = produtos
    .Filter("Nome", "contains", "Notebook")
    .Order("Preco", ascending: false)
    .Page(index: 1, size: 20);
```

---

## 🏛️ Arquitetura e Decisões de Engenharia

Para entender os padrões de design, trade-offs, análise de segurança e matriz de compatibilidade do ecossistema, consulte nossa documentação técnica:
- [Visão Geral da Arquitetura & Diagramas C4](./docs/arquitetura/visao-geral.md)
- [ADR 000: Arquitetura e Convenções](./docs/adr/ADR-000-arquitetura-e-convencoes.md)
- [Catálogo Completo de 10 ADRs](./docs/adr/)

---

## 🤝 Contribuição

Contribuições são muito bem-vindas! Siga estas diretrizes:
1. Abra uma issue para discutir novas funcionalidades ou relatar bugs.
2. Certifique-se de que os métodos de extensão respeitem **Guard Clauses** no primeiro argumento (`this T source`).
3. Mantenha conformidade com todos os runtimes suportados (`dotnet build`).
4. Acompanhe novas implementações com testes unitários no padrão Triple A.

---

## 📄 Licença

Este projeto é distribuído sob a licença [MIT](LICENSE.txt).