# 🏛️ TL.ExtensionLibrary.Domain

[![.NET](https://img.shields.io/badge/.NET-netstandard2.0%20%7C%20net8.0-blue.svg)](https://dotnet.microsoft.com/)
[![NuGet Version](https://img.shields.io/nuget/v/TL.ExtensionLibrary.Domain.svg?style=flat&logo=nuget)](https://www.nuget.org/packages/TL.ExtensionLibrary.Domain)
[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://opensource.org/licenses/MIT)

**`TL.ExtensionLibrary.Domain`** é o metapacote oficial para a camada de **Domínio** em aplicações baseadas em **Clean Architecture** e **Domain-Driven Design (DDD)**.

Ele agrega com zero overhead binário as extensões essenciais para tipos puros de domínio, garantindo **zero dependência de infraestrutura, bancos de dados, JSON ou ASP.NET Core**.

---

## 📦 Módulos Incluídos

Este metapacote agrega transitivamente 5 módulos fundamentais:

| Pacote | Descrição |
| :--- | :--- |
| **`TL.StringExtensionsLibrary`** | Manipulação de texto, truncamento seguro com reticências, parsing numérico/data e helpers de string. |
| **`TL.NumericExtensionsLibrary`** | Operações matemáticas, verificação de números primos, paridade, cálculo percentual e fatoriais. |
| **`TL.DateTimeExtensionsLibrary`** | Cálculos de dias úteis, verificação de finais de semana e aritmética fluente de datas. |
| **`TL.EnumExtensionsLibrary`** | Obtenção rápida de descrições anotadas via cache de alta performance e mapeamento de chaves. |
| **`TL.CollectionExtensionsLibrary`** | Particionamento em lote (`ChunkBy`), paginação em memória e embaralhamento seguro. |

---

## 🚀 Instalação

Instale diretamente no seu projeto de Domínio (`Domain` / `Core`):

```bash
dotnet add package TL.ExtensionLibrary.Domain
```

---

## 🛡️ Garantia Arquitetural

Ao instalar este pacote em sua camada de domínio:
- ✅ **Acesso Total:** Tipos primitivos, coleções e enums ganham utilitários de alta performance.
- ❌ **Isolamento Rígido:** Nenhuma classe de `HttpClient`, `IQueryable`, `ClaimsPrincipal` ou Entity Framework Core é exposta no IntelliSense ou adicionada às referências.

---

## 📄 Licença

Distribuído sob a licença [MIT](https://opensource.org/licenses/MIT). Autor: **Thaylon Lopes**.

