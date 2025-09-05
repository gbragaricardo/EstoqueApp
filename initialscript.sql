IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
CREATE TABLE [Category] (
    [Id] int NOT NULL IDENTITY,
    [Name] NVARCHAR(80) NOT NULL,
    CONSTRAINT [PK_Category] PRIMARY KEY ([Id])
);

CREATE TABLE [CostCenter] (
    [Id] int NOT NULL IDENTITY,
    [Name] NVARCHAR(80) NOT NULL,
    [Code] NVARCHAR(8) NOT NULL,
    CONSTRAINT [PK_CostCenter] PRIMARY KEY ([Id])
);

CREATE TABLE [Product] (
    [Id] int NOT NULL IDENTITY,
    [Name] NVARCHAR(80) NOT NULL,
    [UnitPrice] decimal(18,2) NOT NULL DEFAULT 0.0,
    [Description] NVARCHAR(256) NULL,
    [SKU] NVARCHAR(8) NOT NULL,
    [CurrentStock] int NOT NULL DEFAULT 0,
    [IsActive] bit NOT NULL DEFAULT CAST(1 AS bit),
    [CategoryId] int NOT NULL,
    CONSTRAINT [PK_Product] PRIMARY KEY ([Id]),
    CONSTRAINT [CK_Product_CurrentStock_NonNegative] CHECK ([CurrentStock] >= 0),
    CONSTRAINT [CK_Product_UnitPrice_NonNegative] CHECK ([UnitPrice]   >= 0),
    CONSTRAINT [FK_Product_Category] FOREIGN KEY ([CategoryId]) REFERENCES [Category] ([Id]) ON DELETE NO ACTION
);

CREATE TABLE [StockMovements] (
    [Id] int NOT NULL IDENTITY,
    [Type] VARCHAR(10) NOT NULL,
    [UnitCost] decimal(18,2) NOT NULL DEFAULT 0.0,
    [Quantity] int NOT NULL,
    [CreateDate] datetime2 NOT NULL DEFAULT (GETUTCDATE()),
    [Description] NVARCHAR(256) NULL,
    [ProductId] int NOT NULL,
    [CostCenterId] int NULL,
    CONSTRAINT [PK_StockMovements] PRIMARY KEY ([Id]),
    CONSTRAINT [CK_StockMovement_Quantity_Positive] CHECK ([Quantity] > 0),
    CONSTRAINT [CK_StockMovement_Type_Allowed] CHECK ([Type] IN ('In','Out')),
    CONSTRAINT [CK_StockMovement_UnitCost_NonNegative] CHECK ([UnitCost] >= 0),
    CONSTRAINT [FK_Movement_CostCenter] FOREIGN KEY ([CostCenterId]) REFERENCES [CostCenter] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Movement_Product] FOREIGN KEY ([ProductId]) REFERENCES [Product] ([Id]) ON DELETE NO ACTION
);

CREATE UNIQUE INDEX [IX_CostCenter_Code] ON [CostCenter] ([Code]);

CREATE INDEX [IX_Product_CategoryId] ON [Product] ([CategoryId]);

CREATE UNIQUE INDEX [IX_Product_Sku] ON [Product] ([SKU]);

CREATE INDEX [IX_StockMovements_CostCenterId] ON [StockMovements] ([CostCenterId]);

CREATE INDEX [IX_StockMovements_ProductId] ON [StockMovements] ([ProductId]);

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250905184428_InitialCreation', N'9.0.8');

COMMIT;
GO

