# ADR 005: Decisões Arquiteturais do Pacote TL.AssemblyExtensionLibrary

---

## Contexto

O pacote `TL.AssemblyExtensionLibrary` provê extensões para a classe `System.Reflection.Assembly`. Ele atende a necessidades frequentes em arquiteturas corporativas, como diagnóstico de compilação em tempo de execução (Debug vs Release), inspeção de metadados para injeção de dependência e carregamento prático de recursos embutidos (*manifest resources*).

Compilado exclusivamente para `.NET 8.0`, usufrui das otimizações de Reflection do runtime moderno.

---

## Decisões Arquiteturais

### 1. Foco em Métodos de Alto Valor Agregado
- Priorizar métodos de real conveniência e produtividade para os desenvolvedores, como leitura direta de recursos embutidos para texto e bytes (`GetManifestResourceString`, `GetManifestResourceBytes`) e verificação de compilação JIT (`IsDebugBuild`).

### 2. Resiliência no Escaneamento de Tipos (`GetLoadableTypes`)
- Para contornar cenários onde certas dependências transitivas não estão presentes no AppDomain em tempo de execução, o escaneamento de tipos utiliza captura controlada de `ReflectionTypeLoadException`, retornando os tipos carregados com sucesso (`ex.Types.Where(t => t != null)`). Isso assegura estabilidade em rotinas de registro automático de DI e plugins.

### 3. Compatibilidade com Ambientes Single-File e Containers
- Aplicações compiladas no modo *Single-File Publish* do .NET 8+ possuem localização de assembly vazia (`assembly.Location == ""`). A biblioteca adota tratamento defensivo, evitando o disparo de exceções em operações de `FileInfo` quando executada nesse tipo de ambiente conteinerizado.

### 4. Gestão Segura de Streams I/O
- A leitura de manifest streams utiliza descarte automático de recursos com escopo `using`, garantindo liberação imediata de descritores de arquivo e memória do runtime.

---

## Consequências e Trade-offs

- **Robustez Operacional:** Rotinas de bootstrapping e injeção de dependência estáveis mesmo diante de dependências parciais.
- **Produtividade:** Acesso direto a recursos embutidos (scripts SQL, templates HTML, schemas JSON) sem repetição de boilerplate de streams.
- **Trade-off de Runtime:** O alinhamento exclusivo com `net8.0` foca na modernidade e segurança de execução das versões LTS atuais do ecossistema .NET.
