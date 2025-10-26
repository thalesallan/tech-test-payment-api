# Tech Test Payment API

## Visão geral
API simples para gerenciamento de vendas (cadastro, status, consulta) implementada como teste técnico para processo seletivo. Objetivo: demonstrar design em camadas, boas práticas de Clean Architecture/hexagonal e cobertura de testes unitários.

## Propósito
- Implementar um fluxo mínimo de vendas com regras de negócio e validações.
- Demonstrar domínio isolado (use cases), abstração de repositório, DTOs/entidades, validação e testes.
- Entregar código legível, testável e fácil de evoluir.

## Arquitetura / Estrutura do projeto
Esta solução segue princípios de Clean Architecture / Hexagonal (Ports & Adapters) com separação clara entre domínio, contratos (ports), casos de uso e infraestrutura.

Objetivo: manter regras de negócio (domínio) isoladas, permitir troca fácil de implementações (por exemplo, substituir um repositório em memória por um banco real) e facilitar testes unitários.

Camadas e responsabilidades
- `tech-test-payment-api.Borders` (Contratos / DTOs / Interfaces / Entidades)
  - Aqui ficam os contratos (ports) que descrevem as dependências entre camadas: interfaces dos use cases (ex.: `ICreateSalesUseCase`, `IGetSalesUseCase`, `IUpdateStatusUseCase`) e interfaces de repositório (ex.: `ISaleRepository`).
  - Também contém DTOs de requisição/resposta, entidades de domínio simplificadas e mappers (AutoMapper profiles).
  - Papel: definir contratos estáveis usados por API, UseCases e Repositories; não contém implementações.

- `tech-test-payment-api.UseCases` (Aplicação / Implementação dos Use Cases)
  - Contém as implementações dos casos de uso (a lógica de aplicação) que dependem apenas dos contratos definidos em `Borders`.
  - Exemplos: classes que implementam as interfaces de use case e orquestram validações, regras de negócio e chamadas a portas (repositórios).

- `tech-test-payment-api.Repositories` (Infraestrutura / Implementações de Persistência)
  - Contém as implementações concretas das interfaces de repositório definidas em `Borders` (in-memory, stubs ou adaptadores para DB).
  - Esta camada implementa os ports e é injetada nos UseCases via DI.

- `tech-test-payment-api.Api` (Interface / Adapters - Web)
  - Ponto de entrada da aplicação: Controllers, mapeamentos HTTP <-> DTOs, configuração de DI e ativação do Swagger.
  - Registra as implementações concretas (UseCases, Repositories, Validators, AutoMapper) em `ConfigurationInjection`.
  - Tipos importantes: `Controllers/SalesController.cs`, `Program.cs`, `ConfigurationInjection/*`.

- `tech-test-payment-api.Helpers` (Utilitários e Constantes)
  - Helpers, enums, constantes de erro e exceções customizadas usadas pelos UseCases e Repositories.

- `tech-test-payment-api.UnitTests` (Testes)
  - Contém testes unitários para UseCases e validações (xUnit), usando mocks/stubs para isolar regras de negócio.

Fluxo típico de execução
1. HTTP request chega ao `SalesController` (API).
2. Controller valida/transforma payload em DTO e chama o UseCase (interface definida em `Borders`) — a implementação está em `UseCases`.
3. UseCase executa regras de negócio e usa um `ISaleRepository` (interface de `Borders`) para persistir/recuperar dados; a implementação concreta vem de `Repositories`.
4. Repositório persiste/recupera dados e retorna resultado ao UseCase.
5. UseCase retorna DTO/entidade ao Controller, que responde ao cliente.

Direção de dependência
- Dependências apontam de fora para dentro: API -> UseCases -> Borders (contratos) <- Repositories (implementações).
- Importante: as implementações (UseCases e Repositories) referenciam `Borders` para obedecer aos contratos; `Borders` não referencia implementação alguma.

Validações e erros
- Validações são centralizadas em `Validators/*` e chamadas pelos UseCases antes das operações críticas.
- Exceções customizadas em `Helpers/ExceptionHelper` encapsulam mensagens e códigos usados pelo controlador para mapear respostas HTTP apropriadas.

Mapeamento
- `AutoMapper` é usado para traduzir entre DTOs (`Borders/Dtos`) e entidades de domínio (`Borders/Entities`). Profile(s) estão em `ConfigurationInjection/AutoMapperConfig.cs` e `Borders/Mapper`.

Testabilidade
- Estrutura modular permite testes unitários dos UseCases sem dependência de infraestrutura.
- `UnitTests` usa mocks/stubs para validar cenários (ex.: mudança de status de venda, regras de negócio e mensagens de erro esperadas).

Como trocar/estender implementações
- Para trocar a persistência por um banco real: implementar `ISaleRepository` com EF Core/Postgres e registrar essa implementação em `ConfigurationInjection/RepositoryConfig.cs`.
- Para adicionar um novo caso de uso: criar a interface e implementação em `UseCases`, expor via DI em `UseCaseConfig` e adicionar rota/controller na API.

Boas práticas adotadas
- Controllers finos: orquestram entrada/saída e delegam lógica aos UseCases.
- UseCases coesos: responsabilidades limitadas a um fluxo de negócio.
- Interfaces para portas (repositories) permitindo Test Doubles.
- Centralização de mensagens/constantes para facilitar assertions em testes.

Sugestões rápidas de evolução
- Introduzir camada de integração (Integration Tests) com banco em memória (SQLite) para testes end-to-end.
- Implementar políticas de retry/timeout nas chamadas externas (se houver integrações).
- Adicionar documentação de API mais detalhada (ex.: exemplos de request/response no Swagger).

## Principais conceitos aplicados
- Use Cases (interactors): regras de negócio centralizadas fora de controllers.
- DTOs vs Entities: contratos de entrada/saída isolados de modelos de domínio.
- Dependency Injection: injeção centralizada em `ConfigurationInjection` para desacoplar implementações.
- Validators: validações de payload/IDs antes de executar regras.
- AutoMapper: mapeamento entre DTOs e entidades.
- Tratamento de erros centralizado com exceções customizadas e constantes para mensagens.
- Testes unitários cobrindo cenários críticos das regras de status.

## Tecnologias
- Plataforma: .NET 8 (C#)
- Web API: ASP.NET Core
- Documentação: Swashbuckle / Swagger (Swagger UI)
- Mapeamento: AutoMapper
- Testes: xUnit
- Validações: validators customizados (ou FluentValidation, se presente)
- Ferramentas: dotnet CLI

## Como rodar localmente
(Exemplos em PowerShell)

- Restaurar pacotes:

  dotnet restore tech-test-payment.sln

- Rodar testes:

  dotnet test --no-build

- Rodar a API (porta definida em `launchSettings.json`):

  dotnet run --project .\tech-test-payment-api.Api\tech-test-payment.Api.csproj

- Endpoints locais (quando iniciado):
  - HTTPS: https://localhost:7062
  - HTTP:  http://localhost:5066

- Swagger UI:
  - https://localhost:7062/swagger

## Endpoints principais
Consulte o Swagger para rotas completas. Exemplos comuns:
- `POST /sales` — criar venda
- `PUT /sales/{id}/status` — atualizar status
- `GET /sales/{id}` — obter venda

(As rotas e verbos exatos estão definidos em `Controllers/SalesController.cs`.)

## Observações e decisões de design
- Organização em módulos facilita testes e manutenção.
- UseCases isolam regras, deixando controllers finos (apenas tradução de HTTP <-> DTO).
- Repositório pode ser trocado por um banco real alterando a implementação registrada em `RepositoryConfig`.

## Como estender / próximos passos sugeridos
- Adicionar persistência real (EF Core / PostgreSQL) com migrations.
- Adicionar integrações (ex.: gateway de pagamento simulado).
- Melhorar cobertura de testes (integração e e2e).
- Adicionar políticas de logging estruturado e traces distribuídos.
- Criar exemplo de requests (cURL/Insomnia/Postman) para os casos principais.

## Notas finais
Este repositório foi implementado como teste técnico para candidatura. O código prioriza clareza e separação de responsabilidades para facilitar revisão e evolução.

---

Se quiser, posso também:
- Extrair e listar endpoints com exemplos de payload;
- Criar templates de solicitações cURL/Insomnia;
- Adicionar um `README` mais técnico por pasta ou gerar um `docker-compose` com banco.
