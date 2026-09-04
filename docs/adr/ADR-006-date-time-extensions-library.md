# ADR 006: Decisões Arquiteturais do Pacote TL.DateTimeExtensionsLibrary

---

## Contexto

O pacote `TL.DateTimeExtensionsLibrary` disponibiliza utilitários para cálculos temporais, manipulação de calendários, operações de fuso horário e contagem de dias úteis sobre `System.DateTime`.

Em serviços de faturamento, agendamento de tarefas e relatórios analíticos, cálculos de prazos e agregações temporais exigem precisão de timezone, imutabilidade e alta performance algorítmica.

---

## Decisões Arquiteturais

### 1. Preservação de Integridade de `DateTimeKind`
- Todas as operações de transformação de datas (como início de mês, adições de períodos e cálculo de idade) preservam o `DateTimeKind` original (`Utc`, `Local` ou `Unspecified`), evitando conversões acidentais de fuso horário em pipelines de dados.

### 2. Eficiência Algorítmica em Dias Úteis ($O(1)$)
- O cálculo de dias úteis em intervalos longos é otimizado matematicamente através do cálculo das semanas completas ($5 \times \text{semanas}$) somado aos dias remanescentes, reduzindo a complexidade de $O(N)$ iterativo para tempo constante $O(1)$ e eliminando loops custosos.

### 3. Fatiamento Temporal para Processamento em Lote (`Chunks`)
- O método `Chunks(endDate, days)` facilita o particionamento de consultas a bancos de dados e chamadas externas em janelas temporais menores e gerenciáveis, prevenindo travamentos por consultas massivas.

### 4. Testabilidade com `TimeProvider` (.NET 8+)
- Para garantir suporte a testes unitários determinísticos em pipelines de CI/CD, operações que consultam o relógio do sistema oferecem suporte opcional à abstração `TimeProvider`, permitindo simular datas futuras e passadas sem alterar o relógio da máquina servidora.

---

## Consequências e Trade-offs

- **Performance Escalável:** Resolução instantânea de prazos úteis para contratos e faturas de longa duração.
- **Confiabilidade Temporal:** Ausência de inconsistências em ambientes de nuvem e contêineres que operam estritamente em UTC.
- **Trade-off de Feriados:** A contagem de dias úteis padrão considera fins de semana (sábado e domingo). Para calendários de feriados municipais ou bancários móveis, a arquitetura provê extensibilidade através de calendários customizados.
