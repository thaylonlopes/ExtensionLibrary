# ADR 001: Decisões Arquiteturais do Pacote TL.StringExtensionsLibrary

---

## Contexto

O pacote `TL.StringExtensionsLibrary` provê métodos utilitários de extensão para o tipo fundamental `System.String`. Por lidar diretamente com dados de entrada de usuários e integração com sistemas externos (APIs, mensageria, arquivos CSV), o pacote desempenha um papel central na garantia de integridade, facilidade de uso e alta performance para microsserviços e bibliotecas em C# / .NET.

A biblioteca é compatível com múltiplos runtimes (`netstandard2.0`, `net5.0`, `net6.0` e `net8.0`).

---

## Decisões Arquiteturais

### 1. Pureza Funcional e Imutabilidade
- Operações de extensão sobre strings produzem sempre novas instâncias ou estruturas imutáveis, garantindo ausência de efeitos colaterais e segurança para concorrência multithread.
- Validação defensiva de nulidade no primeiro parâmetro (`this string source`), exceto em métodos cujo objetivo expresso seja checar estados nulos ou em branco (como `IsNullOrEmpty()` ou `IsNullOrWhiteSpace()`).

### 2. Conversões e Parsing com Padrão Try/Default
- Para fluxos de dados com formato imprevisível, o pacote prioriza sobrecargas seguras:
  - `TryToInt(out int result)`: Permite controle de fluxo idiomático em checagens booleanas.
  - `ToIntOrDefault(int defaultValue = 0)`: Permite extração rápida com valor de fallback sem interrupção por exceções de formato.

### 3. Otimização de Memória e Zero-Allocation via `ReadOnlySpan<char>`
- Métodos de fatiamento e truncamento (`Left`, `Right`, `Trim`, `TruncateWithEllipsis`) são estruturados para priorizar o uso de `ReadOnlySpan<char>`, minimizando alocações intermediárias na Heap e melhorando o throughput da aplicação consumidora.

### 4. Resiliência e Prevenção de DoS em Expressões Regulares
- Operações que utilizam expressões regulares (`Regex`) definem `TimeSpan matchTimeout` explícito, protegendo aplicações contra travamentos de CPU decorrentes de padrões de texto patológicos.

---

## Consequências e Trade-offs

- **Robustez:** Alta tolerância a variações de payloads e formatos de entrada sem risco de falhas não tratadas.
- **Performance:** Eficiência de memória ao processar grandes volumes de texto através de spans.
- **Compatibilidade:** O suporte a `netstandard2.0` em conjunto com APIs modernas do .NET 8+ é viabilizado de forma transparente por diretivas de compilação condicional, preservando a interoperabilidade da biblioteca.
