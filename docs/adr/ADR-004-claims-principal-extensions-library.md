# ADR 004: Decisões Arquiteturais do Pacote TL.ClaimsPrincipalExtensionsLibrary

---

## Contexto

O pacote `TL.ClaimsPrincipalExtensionsLibrary` simplifica a extração de dados de autenticação e permissões a partir de instâncias de `System.Security.Claims.ClaimsPrincipal` (`HttpContext.User`).

Em arquiteturas de microsserviços e Web APIs com autenticação via tokens JWT, diferentes Identity Providers (Keycloak, Auth0, Microsoft Entra ID) e middlewares do ASP.NET Core utilizam convenções distintas de nomenclatura para claims de identificação (como nomes curtos RFC 7519 vs namespaces XML de `ClaimTypes`). O pacote atua como uma camada unificada e resiliente de abstração.

---

## Decisões Arquiteturais

### 1. Suporte Híbrido e Resiliente a Esquemas de Claims
- As extensões inspecionam de forma transparente tanto o padrão curto de tokens JWT (`sub`, `role`, `email`, `name`) quanto os esquemas formais de `ClaimTypes` (`ClaimTypes.NameIdentifier`, `ClaimTypes.Role`, `ClaimTypes.Email`, `ClaimTypes.Name`), garantindo funcionamento imediato independente da configuração do provedor de identidade.

### 2. Extração Flexível e Tipada de Identificadores (`GetUserId<T>`)
- Além de acessores diretos para identificadores numéricos, o pacote disponibiliza getters tipados capazes de converter o Subject ID para `Guid`, `string`, `long` ou `int`, atendendo às diversas convenções de chave primária adotadas pelas aplicações consumidoras.

### 3. Validação de Estado de Autenticação
- Operações de leitura contam com validação defensiva do estado `claimsPrincipal?.Identity?.IsAuthenticated`, retornando valores anuláveis (`null`) de forma previsível caso a requisição não esteja autenticada.

### 4. Mapeamento de Roles para Enums de Domínio
- O método `Roles<T>()` permite que o consumidor converta as roles de texto do usuário em um enum tipado de autorização da sua própria regra de negócio, reduzindo a proliferação de strings mágicas no código de segurança.

---

## Consequências e Trade-offs

- **Interoperabilidade:** Facilidade de integração com qualquer IdP moderno sem necessidade de remapeamento manual de claims no middleware.
- **Segurança de Código:** Redução de código boilerplate defensivo nos controllers, Minimal APIs e handlers MediatR.
- **Trade-off de Resolução:** A busca em esquemas duplos (JWT e WS-Fed) introduz uma verificação sequencial simples que não gera impacto perceptível na performance das requisições.
