# ADR 000: Arquitetura e Convenções da Suíte TL.UtilExtensions

---

## Contexto

A suíte **TL.UtilExtensions** é uma coleção modular de métodos de extensão utilitários de alta performance em C# / .NET, distribuída através de pacotes NuGet independentes sob o prefixo `TL.*`.

Composta por 10 projetos de bibliotecas de classes, a suíte estende tipos primitivos, estruturas do runtime (.NET BCL), coleções, requisições HTTP e operações LINQ. Para assegurar interoperabilidade, facilidade de uso, previsibilidade e alto desempenho em microsserviços e Web APIs corporativas, este documento formaliza os padrões de design, regras de nomenclatura e convenções arquiteturais que orientam todo o ecossistema.

---

## Decisões Arquiteturais e Convenções

### 1. Estrutura Modular e Granularidade
- A solução é composta por 10 módulos granulares independentes:
  - `TL.StringExtensionsLibrary`
  - `TL.NumericExtensionsLibrary`
  - `TL.EnumExtensionsLibrary`
  - `TL.ClaimsPrincipalExtensionsLibrary`
  - `TL.AssemblyExtensionLibrary`
  - `TL.DateTimeExtensionsLibrary`
  - `TL.CollectionExtensionsLibrary`
  - `TL.HttpClientExtensionsLibrary`
  - `TL.ObjectExtensionsLibrary`
  - `TL.QueryableExtensionsLibrary`
- Cada pacote possui responsabilidade única e coesa, estendendo uma família específica de tipos. Os consumidores instalam estritamente os pacotes de que necessitam, mantendo as aplicações leves e sem dependências transitivas indesejadas.

### 2. Organização de Arquivos e Classes Parciais
- As classes de extensão são declaradas como `public static partial class <Tipo>Extension` ou `<Tipo>Extensions`.
- A separação física dos métodos é estruturada por afinidade de domínio no formato `<Tipo>Extension.<Categoria>.cs` (por exemplo: `StringExtensions.DateTime.cs`, `NumericExtension.Calculate.cs`, `CollectionExtensions.IEnumerable.cs`), facilitando a manutenção e a legibilidade.

### 3. Multi-Targeting Abrangente
- As bibliotecas oferecem compatibilidade com múltiplos runtimes para atender tanto sistemas já em produção quanto novas aplicações em .NET 8+:
  - Runtimes padrão: `netstandard2.0;net5.0;net6.0;net8.0`.
  - Módulos que utilizam recursos avançados e diagnósticos de runtime (.NET 8+) compilam para `net8.0` (ex: `TL.AssemblyExtensionLibrary`).

### 4. Pureza Funcional e Guard Clauses (Fail-Fast)
- Os métodos de extensão são concebidos com foco em previsibilidade e ausência de efeitos colaterais ocultos.
- O primeiro parâmetro (`this T source`) deve conter validação imediata de nulidade via Guard Clauses (`ArgumentNullException.ThrowIfNull(source)` em .NET 8+ ou validação explícita em runtimes legados), exceto métodos expressamente desenhados para avaliação de nulidade (ex: `IsNullOrEmpty()`).

### 5. Tratamento de Erros e APIs Claras
- Métodos utilitários de conversão e parsing disponibilizam sobrecargas claras no padrão `Try*` e `*OrDefault(defaultValue)`, permitindo que o consumidor escolha entre controle de fluxo booleano ou valores de fallback sem sobrecarga desnecessária.

### 6. Documentação Completa de API Pública
- Todos os métodos públicos devem conter documentação XML completa com tags `<summary>`, `<param>`, `<returns>` e, quando aplicável, `<exception>` e `<example>`, garantindo suporte integral a IntelliSense nos ambientes de desenvolvimento.

---

## Consequências e Trade-offs

- **Consistência de API:** Experiência homogênea de desenvolvimento em todos os 10 pacotes da suíte.
- **Portabilidade:** Interoperabilidade transparente com aplicações legadas e modernas.
- **Performance:** Execução determinística com foco em alocação mínima de memória.
