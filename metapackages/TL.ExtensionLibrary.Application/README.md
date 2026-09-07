# 🏢 TL.ExtensionLibrary.Application

[![.NET](https://img.shields.io/badge/.NET-netstandard2.0%20%7C%20net8.0-blue.svg)](https://dotnet.microsoft.com/)
[![NuGet Version](https://img.shields.io/nuget/v/TL.ExtensionLibrary.Application.svg?style=flat&logo=nuget)](https://www.nuget.org/packages/TL.ExtensionLibrary.Application)
[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://opensource.org/licenses/MIT)

**`TL.ExtensionLibrary.Application`** é o metapacote oficial para a camada de **Aplicação** (Use Cases, Serviços de Aplicação, DTOs e CQRS) em arquiteturas limpas.

Ele agrega utilitários para manipulação de objetos, serialização e identidade autenticada, além de herdar transitivamente todo o pacote **`TL.ExtensionLibrary.Domain`**.

---

## 📦 Módulos Incluídos

Este metapacote inclui diretamente:

| Pacote | Descrição |
| :--- | :--- |
| **`TL.ObjectExtensionsLibrary`** | Clonagem profunda, serialização/desserialização JSON otimizada, conversão para dicionários e reflexão de propriedades. |
| **`TL.ClaimsPrincipalExtensionsLibrary`** | Extração tipada de dados de usuários autenticados (Id, Email, Roles, Claims específicas) em fluxos de orquestração. |
| **`TL.ExtensionLibrary.Domain`** | Agregador transitivo contendo `String`, `Numeric`, `DateTime`, `Enum` e `Collection`. |

---

## 🚀 Instalação

Instale diretamente no seu projeto de Aplicação (`Application` / `UseCases`):

```bash
dotnet add package TL.ExtensionLibrary.Application
```

---

## 🛡️ Garantia Arquitetural

- ✅ **Casos de Uso Equipados:** Domínio completo disponível + serialização de DTOs e segurança.
- ❌ **Isolamento Técnico Preservado:** Nenhuma dependência direta de banco de dados (`IQueryable`), clientes HTTP ou reflexão de assemblies.

---

## 📄 Licença

Distribuído sob a licença [MIT](https://opensource.org/licenses/MIT). Autor: **Thaylon Lopes**.

