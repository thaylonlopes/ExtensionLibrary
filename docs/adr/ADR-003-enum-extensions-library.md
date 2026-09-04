# ADR 003: Decisões Arquiteturais do Pacote TL.EnumExtensionsLibrary

---

## Contexto

O pacote `TL.EnumExtensionsLibrary` é uma biblioteca pura da BCL do .NET que estende o tipo `System.Enum`. Ele fornece métodos para transformar enums em coleções estruturadas para preenchimento de select lists/combos em APIs e front-ends (`ToDictionary`, `ToList`) e extrair metadados descritivos declarados em atributos de anotação (`[Description]`, `[EnumDescription]`, `[Display]`).

O objetivo é eliminar código repetitivo de reflexão nas camadas de apresentação e serviços, mantendo alta eficiência computacional.

---

## Decisões Arquiteturais

### 1. Cache Estático Thread-Safe de Metadados (`EnumCache<T>`)
- A inspeção de atributos declarativos via Reflection em loops ou requisições HTTP de alta frequência é otimizada através de cache estático genérico thread-safe. As informações descritivas são lidas exatamente uma vez por tipo durante o ciclo de vida do processo, garantindo consultas subsequentes em tempo quase instantâneo ($O(1)$).

### 2. Aproveitamento de APIs Modernas do .NET 8+
- Em runtimes .NET 8+, a biblioteca adota `Enum.GetValues<T>()` genérico, eliminando operações de *boxing* e *unboxing* na iteração de valores enumerados.

### 3. Resolução Graciosa de Descrições
- Ao solicitar a descrição de um enum que não contenha atributo anotado, o método realiza fallback automático para o nome literal do identificador (`enumValue.ToString()`), assegurando que a chamada sempre retorne um texto utilizável sem quebras de execução.

### 4. Suporte a Padrões de Atributos do Ecossistema
- Reconhecimento automático dos atributos padrão `System.ComponentModel.DescriptionAttribute`, `System.ComponentModel.DataAnnotations.DisplayAttribute` e o atributo embutido `EnumDescriptionAttribute`.

---

## Consequências e Trade-offs

- **Performance Acelerada:** Redução expressiva do custo computacional de Reflection em endpoints que retornam catálogos de enums.
- **Produtividade:** Geração ágil de dicionários `(ID -> Descrição)` com uma única linha de código.
- **Trade-off de Memória:** O uso de cache estático retém metadados em memória de forma proporcional à quantidade de tipos enum utilizados, o que representa um consumo negligenciável frente aos ganhos de CPU.
