# EstoqueApp — Documentação Geral

## Sumário
* [Project overview](#project-overview)
* [Tech stack & requirements](#tech-stack--requirements)
* [Setup (development)](#setup-rápido-development)
* [Database & migrations](#database--migrations)
* [Arquitetura e rotas principais (Razor Pages)](#arquitetura-e-razor-pages)
* [Modelos / Classes (detalhado)](#models--classes-detalhamento-completo)
* [Regras de negócio importantes (validações / constraints)](#regras-de-negócio-essenciais-resumo)
* [Layouts e assets (UI)](#ui--layouts--assets)
* [Lista de pendências / notas do projeto](#pendências--lembretes-extraídos-de-lembretestxt)
* [Contribuição, testes e deployment](#desenvolvimento-boas-práticas-e-sugestões)
* [Checklist para publicar no GitHub](#checklist-para-publicar-no-github)

---

## Project overview
EstoqueApp é um sistema simples de controle de estoque baseado em Razor Pages (.NET 9). Suporta cadastro de produtos, categorias, unidades de medida, centros de custo, estoques por centro e movimentações de entrada/saída/transferência. Projeto organizado para ser usado em ambiente empresarial leve, com atenção para constraints de banco de dados e rastreabilidade de movimentos.

### Propósito
Gerenciar estoques consolidados e por centros de custo, registrar movimentações e manter histórico.

---

## Tech stack & requirements
* **.NET 9** (target framework do projeto)
* **C# 13**
* **ASP.NET Core Razor Pages**
* **Entity Framework Core** (SQL Server provider)
* **Visual Studio 2022** (recomendado)
* **SQL Server** (LocalDB / SQL Server)
* **Optional**: TailwindCSS (CDN atualmente), Chart.js (CDN)
* **Ferramentas recomendadas**: `dotnet-ef`, EF Core Tools

### Ambiente mínimo de desenvolvimento:
* VS2022 com workload “ASP.NET and web development”
* .NET 9 SDK instalado
* SQL Server / LocalDB acessível
* Connection string configurada em `appsettings.json` (DefaultConnection)

### Exemplo de connection string (`appsettings.json`):
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost,1433;Database=Estoque;User ID=sa;Password=Projeta24862;Encrypt=True;TrustServerCertificate=True"
  }
}
```

---

## Setup rápido (development)
1.  Clonar repositório.
2.  Configurar connection string em `appsettings.json`.
3.  `dotnet restore`
4.  `dotnet tool install --global dotnet-ef` (se não tiver instalado)
5.  `dotnet ef database update` (aplica migrations; a migration inicial já existente cria o schema)
6.  Abrir no Visual Studio 2022 e executar, ou `dotnet run`

> **Observações:**
> * A cultura padrão é `pt-BR` definida em `Program.cs`.
> * `Program.cs` adiciona header UTF-8 e mapeia Razor Pages e assets estáticos.

---

## Database & migrations
A migration inicial (`Migrations/20250919191800_InitialCreate.cs`) cria as seguintes tabelas:
* `Category`
* `CostCenter`
* `MeasureUnit`
* `Product`
* `StockByCostCenter`
* `StockMovement`

### Índices e constraints notáveis (implementados na migration):
* Unique index em `CostCenter.Code`
* Unique index em `MeasureUnit.Abbreviation`
* Unique index em `Product.SKU` e em `Product.Name`
* **Check constraints:**
    * `Product.CurrentStock >= 0`
    * `Product.UnitPrice >= 0`
    * `StockMovement.Quantity > 0`
    * `StockMovement.Type IN ('Entry','Exit','Transfer')`
* **Foreign keys com comportamentos:** alguns com `Restrict`, alguns com `SetNull` (`StockByCostCenter`)

---

## Arquitetura e Razor Pages
* **Razor Pages** é o padrão; priorizar páginas do diretório `Pages` (index, produtos, categorias etc).
* **Layouts fornecidos:**
    * `Pages/Shared/_Layout.cshtml` (baseado em Bootstrap)
    * `Pages/Shared/_LayoutNovo.cshtml` (alternativo, baseado em Tailwind)
* **`Program.cs`:**
    * Configura cultura `pt-BR`
    * Registra `RazorPages` e `DbContext` (`AppDataContext` usando SQL Server)
    * Garante resposta como UTF-8
    * Mapeia páginas e assets estáticos

---

## Models / Classes (detalhamento completo)
> **Observação:** as assinaturas abaixo refletem os arquivos do projeto e os tipos referenciados.

### 1. `Product` (`Models/Product.cs`)
* **Propriedades:**
    * `int Id` — PK
    * `required string Name` — nome do produto (unique index)
    * `decimal UnitPrice` — preço unitário (default 0, check >= 0)
    * `string? Description` — descrição opcional
    * `required string Sku` — SKU (unique)
    * `decimal MinStock` — estoque mínimo para alerta (default 0)
    * `decimal CurrentStock` — estoque consolidado atual (default 0, check >= 0)
    * `bool IsActive` — flag para ativar/desativar produto (default true)
    * `int CategoryId` / `Category? Category` — FK para `Category` (Restrict)
    * `int MeasureUnitId` / `MeasureUnit? MeasureUnit` — FK para `MeasureUnit` (Restrict)
    * `List<StockByCostCenter> StocksByCostCenter` — estoques por centro
    * `List<ProductByMovement> ProductsByMovement` — relacionamento com movimentos
    * `List<StockMovement> Movements` — movimentos associados (navegação)
* **Papel:** representa o item físico/virtual gerenciado no sistema.
* **Relações:** 1 `Product` -> N `StockByCostCenter`, N `ProductByMovement`.

### 2. `StockMovement` (`Models/StockMovement.cs`)
* **Propriedades:**
    * `int Id` — PK
    * `MovementType Type` — enum `Entry`/`Exit`/`Transfer` (stored as `varchar(16)` no migration)
    * `DateTime SystemDate` — data de criação registrada automaticamente (UTC, `default DateTime.UtcNow`)
    * `DateTime Date` — data informada pelo usuário (quando ocorreu)
    * `string? Description` — comentário/observação
    * `List<ProductByMovement> ProductsByMovement` — lista de produtos e quantidades no movimento
    * `int? OriginCostCenterId` / `CostCenter? OriginCostCenter` — origem (null em entrada)
    * `int? DestinationCostCenterId` / `CostCenter? DestinationCostCenter` — destino (null em saída)
* **`MovementType` enum:**
    * `Entry` (Entrada)
    * `Exit` (Saída)
    * `Transfer` (Transferência)
* **Regras / constraints (migration):**
    * `[Quantity] > 0` (na migration assoc. ao movimento)
    * `Type` somente `Entry`/`Exit`/`Transfer`
* **Papel:** registra um evento de movimentação que pode alterar estoques por centro e consolidado.

### 3. `ProductByMovement`
* **Propriedades (assinatura referenciada):**
    * `int Id`
    * `int? MovementId` / `StockMovement? Movement`
    * `int? ProductId` / `Product? Product`
    * `decimal Quantity`
* **Papel:** junction (many-to-many com payload) entre `StockMovement` e `Product`, armazenando a quantidade por produto no movimento.

### 4. `StockByCostCenter`
* **Propriedades:**
    * `int Id`
    * `int? ProductId` / `Product? Product`
    * `int? CostCenterId` / `CostCenter? CostCenter`
    * `decimal Quantity` — quantidade atual daquele produto naquele centro
    * `DateTime LastUpdate` — ultima atualização (migration usa `default GETUTCDATE()`)
* **Papel:** guarda o estoque por produto dentro de cada centro de custo. Único por `(ProductId, CostCenterId)` com índice único.

### 5. `CostCenter` (`Models/CostCenter.cs`)
* **Propriedades:**
    * `int Id`
    * `required string Name`
    * `required string Code` — código único (índice único)
    * `List<StockByCostCenter> StocksByCostCenter`
    * `List<StockMovement> Movements`
* **Papel:** unidade organizacional ou local de estoque (filial, armazém, setor).

### 6. `Category`
* **Propriedades (conforme assinaturas):**
    * `int Id`
    * `required string Name`
    * `required string Abbreviation`
    * `List<Product> Products`
* **Papel:** categorização de produtos (para relatórios, filtros).

### 7. `MeasureUnit`
* **Propriedades:**
    * `int Id`
    * `required string Name`
    * `required string Abbreviation` (unique)
    * `List<Product> Products`
* **Papel:** unidade de medida para o produto (kg, un, l, etc).

---

## Regras de negócio essenciais (resumo)
* Produtos têm `UnitPrice >= 0` e `CurrentStock >= 0`.
* Movimentações têm `Quantity > 0`.
* `MovementType` só aceita `Entry`/`Exit`/`Transfer`.
* Em **transferências**, `origin` e `destination` devem ser ambos preenchidos.
* Em **entry**: `DestinationCostCenterId` deve existir; `OriginCostCenterId == null`.
* Em **exit**: `OriginCostCenterId` deve existir; `DestinationCostCenterId == null`.
* `StockByCostCenter` é único por `(ProductId, CostCenterId)` — gerenciar criação/atualização atômica.
* **Ao aplicar um movimento:**
    * **Entry**: incrementar `StockByCostCenter.Destination` e `CurrentStock` do `Product`.
    * **Exit**: decrementar `StockByCostCenter.Origin` e `CurrentStock` do `Product` (garantir que não ficará negativo).
    * **Transfer**: decrementar `origin`, incrementar `destination`; `CurrentStock` do `Product` fica inalterado.
    * Sempre persistir `ProductByMovement` para histórico e para facilitar auditoria.

> A implementação concreta das operações deve ser feita em serviços/transactions no backend para garantir a consistência dos dados.

---

## UI / Layouts / Assets
* **Dois layouts:**
    * `_Layout.cshtml` — Bootstrap (principal)
    * `_LayoutNovo.cshtml` — Alternativo com Tailwind, sidebar e JS para toggling
* **Assets:**
    * `/lib/bootstrap` (local)
    * jQuery & Bootstrap bundle (local)
    * Tailwind e Chart.js atualmente via CDN (nota: migrar para local)
* **Scripts:**
    * `~/js/site.js`
    * `~/js/confirmModal.js`
> **Observação:** `Program.cs` usa `MapStaticAssets` e `WithStaticAssets`. Verificar se estes métodos de extensão estão corretamente definidos no projeto ao publicar.

---

## Pendências / Lembretes (extraídos de `Lembretes.txt`)
* [ ] Remover comentários em propriedades de `Product` e nos mapeamentos de `Unit`/`Measure`.
* [ ] Revisar os índices de todos os mapeamentos.
* [ ] Revisar o comportamento de delete (cascade/restrict) de todos os mapeamentos.
* [ ] Baixar o Tailwind e remover a referência do CDN.
* [ ] Remover o CDN do Chart.js; incluir o bundle localmente.
* [ ] Remover código de teste para gráficos e alertas da página `Index` principal.
* [ ] Revisar comentários de `ILogger` na página `Index`.

---

## Desenvolvimento: boas práticas e sugestões
* **Implementar uma camada de serviço/transação** para aplicar movimentações, garantindo atomicidade e rollback.
* **Usar DTOs/ViewModels** para formulários de cadastro e movimento.
* **Validar entradas do usuário** no server-side (`DataAnnotations` + validadores customizados) e no client-side.
* **Adicionar testes unitários** para regras de negócio: entradas, saídas, transferências, prevenção de estoque negativo.
* **Registrar auditoria** (quem fez a movimentação) — adicionar ASP.NET Identity se o sistema for multiusuário.
* **Fazer um seed básico** para `MeasureUnit`, `Category` e alguns `CostCenters` para facilitar os testes.

---

## Testes e CI/CD
* **Pipeline GitHub Actions recomendado:**
    1.  `dotnet restore`
    2.  `dotnet build --configuration Release`
    3.  `dotnet test` (se houver testes)
    4.  `dotnet ef migrations script` (opcional)
    5.  Build de imagem Docker e publicação (opcional)
* Incluir um arquivo `.github/workflows/ci.yml` com uma matriz para versões do .NET.

---

## Deployment
* Publicar para Azure App Service ou Docker.
* Garantir que a connection string esteja configurada em variáveis de ambiente.
* Executar as migrations (`dotnet ef database update`) durante o deploy ou usar um script de migração automatizado.
