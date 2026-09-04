# ADR 009: Decisões Arquiteturais do Pacote TL.ObjectExtensionsLibrary

---

## Contexto

O pacote `TL.ObjectExtensionsLibrary` oferece métodos utilitários genéricos para qualquer instância derivada de `System.Object`, abrangendo clonagem profunda (*deep cloning*), conversão em `ExpandoObject` e dicionários dinâmicos, conversão para byte arrays e invocação dinâmica via reflexão.

A proposta é acelerar o desenvolvimento de DTOs, testes automatizados e manipulação de estruturas flexíveis mantendo alocação controlada de memória.

---

## Decisões Arquiteturais

### 1. Clonagem Profunda com Otimização de Buffers
- A clonagem profunda de grafos de objetos utiliza serialização binária UTF-8 em memória (`System.Text.Json` ou `Utf8JsonWriter`), garantindo cópia completa de propriedades e referências aninhadas com geração mínima de strings intermediárias.

### 2. Conversão Dinâmica para `ExpandoObject` com Cache
- Métodos que mapeiam objetos POCO para `ExpandoObject` ou `Dictionary<string, object>` utilizam cache estático das propriedades públicas do tipo, eliminando a sobrecarga de Reflection em transformações repetidas de dados.

### 3. Preservação de Causa Raiz em Invocação Dinâmica (`InvokeMethod`)
- Em rotinas de invocação de métodos por reflexão, exceções disparadas dentro do método invocado são desembrulhadas de `TargetInvocationException` e relançadas via `ExceptionDispatchInfo.Capture(ex.InnerException).Throw()`, preservando integralmente o stack trace e a mensagem de erro original para depuração.

### 4. Encadeamento Funcional Fluente (`Pipe`, `As<T>`)
- Métodos de apoio como `obj.As<T>()` e `obj.Pipe(transform)` facilitam construções expressivas em estilo funcional sem poluir o código com variáveis temporárias descartáveis.

---

## Consequências e Trade-offs

- **Flexibilidade Máxima:** Capacidade de clonar e inspecionar objetos complexos com uma sintaxe simples e direta.
- **Rastreabilidade de Falhas:** Diagnóstico claro de erros em invocações dinâmicas sem mascaramento de exceções.
- **Trade-off de Tipagem:** Métodos de clonagem profunda baseados em dados focam em propriedades e campos serializáveis; delegates, manipuladores de eventos e ponteiros não são duplicados por design.
