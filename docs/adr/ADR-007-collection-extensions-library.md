# ADR 007: Decisões Arquiteturais do Pacote TL.CollectionExtensionsLibrary

---

## Contexto

O pacote `TL.CollectionExtensionsLibrary` provê extensões para coleções do .NET (`IEnumerable<T>`, `IList<T>`, `Dictionary<TKey, TValue>`, `HashSet<T>`, `Queue<T>`, `Stack<T>`). É uma das bibliotecas mais consumidas para operações de particionamento de lotes (batching), paginação em memória, filtros condicionais e manipulação de listas.

Por atuar no núcleo de pipelines de dados, a previsibilidade da complexidade assintótica e o consumo eficiente de memória são fatores mandatórios.

---

## Decisões Arquiteturais

### 1. Particionamento Linear $O(N)$ em `ChunkBy`
- O método de particionamento `ChunkBy` opera em passagem única (*single-pass streaming*). Ele avança o enumerador de forma linear alocando lotes de tamanho pré-definido via `yield return`, eliminando re-enumerações repetitivas da fonte de dados e garantindo processamento contínuo em coleções volumosas.

### 2. Embaralhamento Estatisticamente Uniforme (`Shuffle`)
- A operação de embaralhamento adota o algoritmo de Fisher-Yates (Knuth Shuffle), garantindo distribuição uniforme com complexidade $O(N)$ e aproveitando instâncias thread-safe (`Random.Shared` no .NET 6+) para máxima eficiência em cenários concorrentes.

### 3. Prevenção de Duplicatas com Tempo Constante $O(1)$
- O método `AddRangeIfNotExists` utiliza tabelas hash (`HashSet<T>`) para indexação intermediária, garantindo verificações de existência em tempo $O(1)$ por elemento adicionado, otimizando inserções em lote em listas grandes.

### 4. Tratamento Seguro de Nulidade em Comparações
- Operações de substituição e busca (`Replace`, `DistinctBy`) utilizam `EqualityComparer<T>.Default.Equals`, suportando coleções que contenham valores nulos com total estabilidade.

---

## Consequências e Trade-offs

- **Alto Throughput:** Capacidade de particionar e processar milhões de registros em streams sem degradação exponencial de CPU.
- **Previsibilidade:** Eliminação de travamentos por consumo descontrolado de memória em pipelines de dados.
- **Trade-off Funcional:** Os métodos de `IEnumerable<T>` operam de forma puramente funcional retornando novas sequências (sem mutação), enquanto utilitários sobre `IList<T>` (como `Move` e `SortBy`) oferecem mutações *in-place* de alto desempenho quando expressamente desejado.
