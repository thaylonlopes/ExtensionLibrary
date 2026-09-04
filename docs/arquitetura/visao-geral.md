# 🏛️ Visão Geral da Arquitetura: TL.UtilExtensions (ExtensionLibrary)

## 📌 1. Propósito e Filosofia do Ecossistema

O **TL.UtilExtensions** (solução `ExtensionLibrary.sln`) é uma suíte modular de métodos de extensão em C# / .NET desenvolvida com o objetivo de estender tipos primitivos, estruturas fundamentais do runtime, coleções, requisições HTTP e operações de consulta do ecossistema .NET.

### Princípios Norteadores:
1. **Alta Granularidade & Baixo Acoplamento:** Cada biblioteca é empacotada individualmente no NuGet (`TL.*`), permitindo que aplicações consumidoras importem estritamente as extensões necessárias sem carregar dependências transitivas indesejadas.
2. **Pureza Funcional & Imutabilidade:** Métodos de extensão devem, por padrão, operar sem efeitos colaterais ocultos, priorizando imutabilidade e previsibilidade.
3. **Multi-Targeting Abrangente:** Suporte a múltiplos runtimes e versões de framework (`netstandard2.0`, `net5.0`, `net6.0`, `net8.0`), viabilizando compatibilidade tanto com aplicações legadas quanto com sistemas modernos em .NET 8+.
4. **Proteção de Invariantes (Fail-Fast):** Validação imediata de argumentos de entrada no parâmetro estendido (`this T source`) via Guard Clauses para eliminar `NullReferenceException` não tratadas.

---

## 🗺️ 2. Diagramas de Arquitetura C4

### 2.1. Nível 1: Diagrama de Contexto de Sistema (C4 Context)

O diagrama abaixo ilustra como as diversas aplicações corporativas (Web APIs, Microsserviços, Workers e Camadas de Persistência) consomem os módulos granulares da suíte **TL.UtilExtensions**.

```mermaid
C4Context
    title Diagrama de Contexto de Sistema - Ecossistema TL.UtilExtensions

    Person(dev, "Desenvolvedor / Engenheiro .NET", "Consome métodos de extensão utilitários para acelerar o desenvolvimento e manter código limpo.")

    Enterprise_Boundary(b0, "Aplicações Consumidoras Corporativas") {
        System(webApi, "Web APIs & Microsserviços", "Serviços REST / Minimal APIs (.NET 8/9, TL.ResilientCore, TL.MiddlewareLibrary)")
        System(workers, "Worker Services & Consumidores", "Serviços em background consumindo filas e processando jobs")
        System(dataLayer, "Data Access & Repositórios", "Camadas de persistência com EF Core, Dapper e MongoDB (DataHelpers)")
    }

    System_Boundary(b1, "Ecossistema TL.UtilExtensions") {
        SystemDb_Ext(nugetPackages, "Pacotes NuGet TL.* (10 Módulos)", "Módulos de extensão utilitária compilados em Multi-Targeting")
    }

    System_Ext(bcl, ".NET Base Class Library (BCL)", "Tipos primitivos, System.Collections, System.Net.Http, System.Reflection, LINQ")

    Rel(dev, webApi, "Desenvolve e mantém")
    Rel(dev, workers, "Desenvolve e mantém")
    Rel(dev, dataLayer, "Desenvolve e mantém")

    Rel(webApi, nugetPackages, "Importa conforme granularidade", "NuGet Reference")
    Rel(workers, nugetPackages, "Importa conforme granularidade", "NuGet Reference")
    Rel(dataLayer, nugetPackages, "Importa extensões LINQ e Coleções", "NuGet Reference")

    Rel(nugetPackages, bcl, "Estende tipos nativos", "C# Extension Methods")
```

---

### 2.2. Nível 2: Diagrama de Containers / Módulos (C4 Container)

O diagrama a seguir detalha os **10 módulos granulares** que compõem a solução `ExtensionLibrary.sln`, destacando suas responsabilidades e suas dependências externas.

```mermaid
C4Container
    title Diagrama de Containers / Módulos - Suíte TL.UtilExtensions

    Container_Boundary(c1, "Solução ExtensionLibrary.sln (10 Módulos)") {
        Container(str, "TL.StringExtensionsLibrary", "C# / netstandard2.0;net5;net6;net8", "Manipulação de strings, casing, parsing, truncamento seguro, CSV e regex. Depende de Newtonsoft.Json.")
        Container(num, "TL.NumericExtensionsLibrary", "C# / netstandard2.0;net5;net6;net8", "Cálculos matemáticos, números primos, fatorial, percentual, GCD/LCM e conversões de ponto flutuante.")
        Container(enm, "TL.EnumExtensionsLibrary", "C# / netstandard2.0;net5;net6;net8", "Extração de descrições, conversão em dicionários e listas tipadas a partir de enums.")
        Container(clm, "TL.ClaimsPrincipalExtensionsLibrary", "C# / net5.0;net6.0;net8.0", "Extração tipada de Claims, Roles, User IDs e propriedades de ClaimsPrincipal.")
        Container(asm, "TL.AssemblyExtensionLibrary", "C# / net8.0", "Inspeção de assemblies, verificação de build debug, leitura de tipos e recursos embutidos.")
        Container(dt, "TL.DateTimeExtensionsLibrary", "C# / net5.0;net6.0;net8.0", "Operações de calendário, contagem de dias úteis, checagens de intervalos e conversões de datas.")
        Container(col, "TL.CollectionExtensionsLibrary", "C# / net5.0;net6.0;net8.0", "Operações sobre IEnumerable, List, Dictionary, Queue, Stack e particionamento (ChunkBy, Shuffle, WhereIf).")
        Container(http, "TL.HttpClientExtensionsLibrary", "C# / net5.0;net6.0;net8.0", "Políticas de resiliência, retry, rate limit, enriquecimento de headers e tokens JWT. Depende de Newtonsoft e Jwt.")
        Container(obj, "TL.ObjectExtensionsLibrary", "C# / net5.0;net6.0;net8.0", "Clonagem profunda, conversão para ExpandoObject, serialização em bytes e inspeção de propriedades.")
        Container(qry, "TL.QueryableExtensionsLibrary", "C# / net5.0;net6.0;net8.0", "Filtros dinâmicos em IQueryable, ordenação via Expression Trees, paginação e agrupamento.")
    }

    System_Ext(newtonsoft, "Newtonsoft.Json (v13.0.3)", "Serialização/deserialização legada")
    System_Ext(jwt, "System.IdentityModel.Tokens.Jwt (v8.1.2)", "Manipulação e parsing de tokens JWT")

    Rel(str, newtonsoft, "Usa para serialização")
    Rel(http, newtonsoft, "Usa para deserialização HTTP")
    Rel(http, jwt, "Usa para parsing de JWT")
```

---

## 📦 3. Catálogo de Projetos e Matriz de Multi-Targeting

| Projeto / Pacote NuGet | Runtimes Alvo (`TargetFrameworks`) | Dependências Externas | Responsabilidade Principal |
| :--- | :--- | :---: | :--- |
| **`TL.StringExtensionsLibrary`** | `netstandard2.0`, `net5.0`, `net6.0`, `net8.0` | `Newtonsoft.Json` (13.0.3) | Métodos utilitários de strings, regex, checagens de formato, truncamento e parsing. |
| **`TL.NumericExtensionsLibrary`** | `netstandard2.0`, `net5.0`, `net6.0`, `net8.0` | **Zero (BCL Pura)** | Operações matemáticas, percentuais, paridade, primalidade e trigonometria. |
| **`TL.EnumExtensionsLibrary`** | `netstandard2.0`, `net5.0`, `net6.0`, `net8.0` | **Zero (BCL Pura)** | Leitura de atributos customizados, descrições e transformações para Dicionários/Listas. |
| **`TL.ClaimsPrincipalExtensionsLibrary`** | `net5.0`, `net6.0`, `net8.0` | **Zero (BCL Pura)** | Extração simplificada e segura de Claims, Roles, IDs inteiros/longos e booleanos. |
| **`TL.AssemblyExtensionLibrary`** | `net8.0` | **Zero (BCL Pura)** | Diagnóstico de compilação (Debug/Release), enumeração de tipos e metadados de Assembly. |
| **`TL.DateTimeExtensionsLibrary`** | `net5.0`, `net6.0`, `net8.0` | **Zero (BCL Pura)** | Cálculos de dias úteis, idades, manipulação de fusos e conversões de formatos temporais. |
| **`TL.CollectionExtensionsLibrary`** | `net5.0`, `net6.0`, `net8.0` | **Zero (BCL Pura)** | Métodos funcionais para coleções (`DistinctBy`, `WhereIf`, `Shuffle`, `Paginate`, `ChunkBy`). |
| **`TL.HttpClientExtensionsLibrary`** | `net5.0`, `net6.0`, `net8.0` | `Newtonsoft.Json`<br/>`System.IdentityModel.Tokens.Jwt` | Enriquecimento de chamadas HTTP, retentativas com backoff, rate limit e extração de claims JWT. |
| **`TL.ObjectExtensionsLibrary`** | `net5.0`, `net6.0`, `net8.0` | **Zero (BCL Pura)** | Clonagem em memória, conversão para dicionários e `ExpandoObject`, conversão para byte arrays. |
| **`TL.QueryableExtensionsLibrary`** | `net5.0`, `net6.0`, `net8.0` | **Zero (BCL Pura)** | Filtros dinâmicos baseados em strings, ordenação parametrizada e paginação em `IQueryable`. |

---

## ⚙️ 4. Diretrizes de Engenharia e Boas Práticas

1. **Parâmetro Estendido não Nulo:** Todo método `this T source` deve validar a não-nulidade imediata de `source` (exceto métodos expressamente desenhados para testar nulidade, como `IsNullOrEmpty()`).
2. **Eliminação de Falhas Silenciosas:** Proibição estrita de blocos `catch` vazios que engolem exceções ou retornam coleções vazias ocultando erros de sintaxe ou de banco de dados.
3. **Modernização com Zero-Allocation:** Migração progressiva de operações com strings e coleções para `ReadOnlySpan<char>` e `Memory<T>`, reduzindo pressão sobre o Garbage Collector.
4. **Desacoplamento de Dependências Pesadas:** Substituição gradual do `Newtonsoft.Json` por `System.Text.Json` com Source Generators no .NET 8+.

