# ADR 011: Metapacotes Agregadores por Camadas da Clean Architecture

---

## Contexto

A suíte `TL.ExtensionLibrary` disponibiliza 10 pacotes modulares individuais e altamente desacoplados. Embora essa granularidade confira total controle sobre dependências, ela introduz atrito na experiência de desenvolvimento (DX) em projetos corporativos baseados em **Clean Architecture** e **Domain-Driven Design (DDD)**:

1. **Atrito de Instalação:** Uma camada de Domínio típica necessita gerenciar múltiplos pacotes (`String`, `Numeric`, `DateTime`, `Enum`, `Collection`), exigindo repetidas instalações individuais.
2. **Risco de Poluição Arquitetural:** Desenvolvedores menos experientes podem acidentalmente instalar extensões com acoplamento a bancos de dados (`TL.QueryableExtensionsLibrary`), protocolos de rede (`TL.HttpClientExtensionsLibrary`) ou contextos web (`TL.ClaimsPrincipalExtensionsLibrary`) no núcleo do Domínio, violando a regra de dependência da Clean Architecture.

Surge a necessidade de disponibilizar pacotes agregadores que agrupem as extensões de acordo com as fronteiras arquiteturais correspondentes.

---

## Decisões Arquiteturais

### 1. Segregação Estrita em 3 Metapacotes de Camada

Optou-se por criar exatamente 3 metapacotes com hierarquia unidirecional estrita:

- **`TL.ExtensionLibrary.Domain`:** Reúne apenas extensões de tipos puros e fundamentais (`String`, `Numeric`, `DateTime`, `Enum`, `Collection`). Proíbe terminantemente qualquer dependência de JSON, ASP.NET Core, EF Core ou HTTP.
- **`TL.ExtensionLibrary.Application`:** Reúne extensões para orquestração de casos de uso, manipulação de DTOs e segurança (`Object`, `ClaimsPrincipal`), incorporando transitivamente o pacote `Domain`.
- **`TL.ExtensionLibrary.Infrastructure`:** Reúne extensões de integração externa, reflexão de assemblies e persistência (`Queryable`, `HttpClient`, `Assembly`), incorporando transitivamente `Application` e `Domain`.

### 2. Exclusão Deliberada do Pacote All-in-One

Decidiu-se explicitamente **não disponibilizar** um metapacote genérico "All-in-One" (`TL.ExtensionLibrary`):
- Um pacote "All-in-One" viola a governança de fronteiras, estimulando o vazamento de tecnologias externas para camadas puras.
- Para aplicações monolíticas, protótipos rápidos ou utilitários de console que necessitem de todas as extensões em um único `<PackageReference>`, a instalação de `TL.ExtensionLibrary.Infrastructure` atende integralmente ao requisito através da herança transitiva natural.

### 3. Zero Overhead Binário (`<IncludeBuildOutput>false</IncludeBuildOutput>`)

Metapacotes não devem conter código C# ou assemblies `.dll` empacotados na pasta `lib/` do pacote NuGet.
- A propriedade `<IncludeBuildOutput>false</IncludeBuildOutput>` é configurada em cada metapacote.
- As dependências são declaradas como `<ProjectReference ... PrivateAssets="none" />`, assegurando que o NuGet propague as dependências com `include="All"` nos manifestos `.nuspec`.
- Isso garante que o download do metapacote tenha tamanho insignificante (~18 KB, incluindo metadados e ícone) e não adicione assemblies intermediários à pasta de build da aplicação consumidora.

### 4. Cobertura de Testes Automatizados de Arquitetura

Para assegurar que o isolamento de camadas não seja degradado ao longo de novas versões, foi criado o projeto de teste `ExtensionLibrary.Architecture.Tests`, que valida em tempo de execução e compilação que o pacote de Domínio não carrega nem vaza tipos de infraestrutura ou web.

---

## Consequências e Trade-offs

- **Developer Experience (DX) Elevada:** Um único comando `dotnet add package TL.ExtensionLibrary.Domain` equipa toda a camada de domínio da aplicação consumidora.
- **Isolamento Garantido por Design:** Prevenção em tempo de compilação contra importações indevidas de APIs técnicas em regras de negócio.
- **Manutenibilidade Simplificada:** A versão dos metapacotes acompanha de forma unificada a versão dos módulos individuais (SemVer), sem divergências.
- **Trade-off de Atualização:** Uma atualização em qualquer módulo do Domínio pode motivar a publicação de nova versão do respectivo metapacote.

