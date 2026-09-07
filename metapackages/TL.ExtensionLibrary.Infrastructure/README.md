# ⚙️ TL.ExtensionLibrary.Infrastructure

[![.NET](https://img.shields.io/badge/.NET-netstandard2.0%20%7C%20net8.0-blue.svg)](https://dotnet.microsoft.com/)
[![NuGet Version](https://img.shields.io/nuget/v/TL.ExtensionLibrary.Infrastructure.svg?style=flat&logo=nuget)](https://www.nuget.org/packages/TL.ExtensionLibrary.Infrastructure)
[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://opensource.org/licenses/MIT)

**`TL.ExtensionLibrary.Infrastructure`** é o metapacote oficial para a camada de **Infraestrutura** (Repositórios, Acesso a Dados, Gateways de API e Integração Externa) em aplicações baseadas em Clean Architecture.

Ele provê ferramentas para montagem dinâmica de consultas LINQ/EF Core, clientes HTTP resilientes e escaneamento de assemblies, além de agregar transitivamente as camadas **`Application`** e **`Domain`**.

> 💡 **Nota para Projetos Monolíticos:** Para aplicações menores, monolitos ou MVPs que desejam utilizar todas as 10 extensões da suíte em um único pacote, a instalação de `TL.ExtensionLibrary.Infrastructure` atende a esse cenário com eficiência máxima e sem duplicação.

---

## 📦 Módulos Incluídos

Este metapacote inclui diretamente:

| Pacote | Descrição |
| :--- | :--- |
| **`TL.QueryableExtensionsLibrary`** | Filtros dinâmicos em `IQueryable`, ordenação condicional e paginação de banco de dados compatíveis com EF Core. |
| **`TL.HttpClientExtensionsLibrary`** | Políticas de retry automáticas, deserialização JSON integrada e comunicação HTTP resiliente. |
| **`TL.AssemblyExtensionLibrary`** | Descoberta dinâmica de tipos, injeção de dependência automática e escaneamento de assemblies. |
| **`TL.ExtensionLibrary.Application`** | Agregador transitivo contendo `ObjectExtensionsLibrary`, `ClaimsPrincipalExtensionsLibrary` e todo o `TL.ExtensionLibrary.Domain`. |

---

## 🚀 Instalação

Instale diretamente no seu projeto de Infraestrutura (`Infrastructure` / `Data` / `ExternalServices`):

```bash
dotnet add package TL.ExtensionLibrary.Infrastructure
```

---

## 🛡️ Camadas Transitivas Resumidas

Ao instalar `TL.ExtensionLibrary.Infrastructure`, sua camada tem acesso a todos os 10 módulos da biblioteca de forma estruturada:

```text
TL.ExtensionLibrary.Infrastructure
 ├── QueryableExtensionsLibrary
 ├── HttpClientExtensionsLibrary
 ├── AssemblyExtensionLibrary
 └── TL.ExtensionLibrary.Application
      ├── ObjectExtensionsLibrary
      ├── ClaimsPrincipalExtensionsLibrary
      └── TL.ExtensionLibrary.Domain
           ├── StringExtensionLibrary
           ├── NumericExtensionLibrary
           ├── DateTimeExtensionsLibrary
           ├── EnumExtensionsLibrary
           └── CollectionExtensionsLibrary
```

---

## 📄 Licença

Distribuído sob a licença [MIT](https://opensource.org/licenses/MIT). Autor: **Thaylon Lopes**.

