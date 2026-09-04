# ADR 002: Decisões Arquiteturais do Pacote TL.NumericExtensionsLibrary

---

## Contexto

O pacote `TL.NumericExtensionsLibrary` fornece métodos de extensão matemáticos, estatísticos e predicados numéricos para os tipos `int`, `double` e `decimal`. A biblioteca foi concebida com zero dependências externas (BCL pura), suportando `netstandard2.0`, `net5.0`, `net6.0` e `net8.0`.

O objetivo é fornecer uma camada utilitária confiável para regras de negócio, validações de integridade, cálculos de porcentagem, médias e teoria dos números (primos, divisores, fatoriais).

---

## Decisões Arquiteturais

### 1. Independência de Dependências e Zero Alocação
- Manter o pacote operando exclusivamente sobre tipos primitivos da Base Class Library (.NET BCL). As operações ocorrem diretamente na stack, garantindo tempo de execução determinístico e sem sobrecarga no Garbage Collector.

### 2. Rigor Matemático e Validações de Domínio
- **Soma de Algarismos (`DigitSum`):** O método opera sobre a decomposição individual dos dígitos do número em valor absoluto, garantindo precisão matemática para cálculos de checksum e regras de validação.
- **MDC e MMC (`GreatestCommonDivisor` e `GreatestCommonMultiple`):** As operações de máximo divisor comum e mínimo múltiplo comum incluem tratamento defensivo para denominadores e inputs nulos ou iguais a zero, evitando divisões por zero em tempo de execução.
- **Fatorial com Domínio Controlado:** Operações de fatorial validam os limites representáveis do tipo inteiro ($0 \le n \le 12$ para `int`), evitando estouros aritméticos não supervisionados.

### 3. Precisão Financeira em Cálculos Decimais
- Cálculos percentuais e médias ponderadas sobre `decimal` preservam estritamente o tipo de ponto fixo de alta precisão, evitando conversões intermediárias para ponto flutuante binário (`double`) que possam causar pequenas discrepâncias de arredondamento.

### 4. Evolução com Generic Math (`INumber<T>`)
- Para runtimes modernos (.NET 7 e .NET 8+), a suíte evolui para abstrações genéricas baseadas em `System.Numerics.INumber<T>`, reduzindo redundância de código e usufruindo de inlining nativo do JIT compiler.

---

## Consequências e Trade-offs

- **Previsibilidade:** Resultados matemáticos determinísticos com proteção contra exceções de domínio.
- **Eficiência:** Velocidade máxima de execução em processamento numérico de alta frequência.
- **Trade-off de Tipagem:** Métodos em `decimal` priorizam precisão exata para cenários financeiros, enquanto sobrecargas em `double` priorizam performance máxima para cálculos científicos e trigonométricos.
