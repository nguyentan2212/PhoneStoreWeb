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
GO

CREATE TABLE [AspNetRoles] (
    [Id] uniqueidentifier NOT NULL,
    [Description] nvarchar(max) NOT NULL,
    [Name] nvarchar(256) NULL,
    [NormalizedName] nvarchar(256) NULL,
    [ConcurrencyStamp] nvarchar(max) NULL,
    CONSTRAINT [PK_AspNetRoles] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Categories] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(max) NULL,
    [Description] nvarchar(max) NULL,
    [ImagePath] nvarchar(max) NULL,
    CONSTRAINT [PK_Categories] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Contacts] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(max) NULL,
    [Email] nvarchar(50) NOT NULL,
    [Message] nvarchar(max) NULL,
    CONSTRAINT [PK_Contacts] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [AspNetRoleClaims] (
    [Id] int NOT NULL IDENTITY,
    [RoleId] uniqueidentifier NOT NULL,
    [ClaimType] nvarchar(max) NULL,
    [ClaimValue] nvarchar(max) NULL,
    CONSTRAINT [PK_AspNetRoleClaims] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [AspNetRoles] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [AspNetUsers] (
    [Id] uniqueidentifier NOT NULL,
    [BirthDate] Date NOT NULL,
    [FullName] nvarchar(max) NULL,
    [Address] nvarchar(max) NULL,
    [ImagePath] nvarchar(max) NULL,
    [Status] int NOT NULL,
    [CreatedDate] Date NOT NULL,
    [AppRoleId] uniqueidentifier NULL,
    [UserName] nvarchar(256) NULL,
    [NormalizedUserName] nvarchar(256) NULL,
    [Email] nvarchar(256) NULL,
    [NormalizedEmail] nvarchar(256) NULL,
    [EmailConfirmed] bit NOT NULL,
    [PasswordHash] nvarchar(max) NULL,
    [SecurityStamp] nvarchar(max) NULL,
    [ConcurrencyStamp] nvarchar(max) NULL,
    [PhoneNumber] nvarchar(max) NULL,
    [PhoneNumberConfirmed] bit NOT NULL,
    [TwoFactorEnabled] bit NOT NULL,
    [LockoutEnd] datetimeoffset NULL,
    [LockoutEnabled] bit NOT NULL,
    [AccessFailedCount] int NOT NULL,
    CONSTRAINT [PK_AspNetUsers] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_AspNetUsers_AspNetRoles_AppRoleId] FOREIGN KEY ([AppRoleId]) REFERENCES [AspNetRoles] ([Id]) ON DELETE NO ACTION
);
GO

CREATE TABLE [Products] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(max) NULL,
    [Description] nvarchar(max) NULL,
    [Price] Money NOT NULL,
    [Stock] int NOT NULL,
    [CreatedDate] Date NOT NULL,
    [OS] nvarchar(max) NULL,
    [RAM] int NOT NULL,
    [Storage] int NOT NULL,
    [BatteryCapacity] int NOT NULL,
    [Status] int NOT NULL,
    [Image] nvarchar(max) NULL,
    [CategoryId] int NULL,
    CONSTRAINT [PK_Products] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Products_Categories_CategoryId] FOREIGN KEY ([CategoryId]) REFERENCES [Categories] ([Id]) ON DELETE NO ACTION
);
GO

CREATE TABLE [AspNetUserClaims] (
    [Id] int NOT NULL IDENTITY,
    [UserId] uniqueidentifier NOT NULL,
    [ClaimType] nvarchar(max) NULL,
    [ClaimValue] nvarchar(max) NULL,
    CONSTRAINT [PK_AspNetUserClaims] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [AspNetUserLogins] (
    [LoginProvider] nvarchar(450) NOT NULL,
    [ProviderKey] nvarchar(450) NOT NULL,
    [ProviderDisplayName] nvarchar(max) NULL,
    [UserId] uniqueidentifier NOT NULL,
    CONSTRAINT [PK_AspNetUserLogins] PRIMARY KEY ([LoginProvider], [ProviderKey]),
    CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [AspNetUserRoles] (
    [UserId] uniqueidentifier NOT NULL,
    [RoleId] uniqueidentifier NOT NULL,
    CONSTRAINT [PK_AspNetUserRoles] PRIMARY KEY ([UserId], [RoleId]),
    CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [AspNetRoles] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [AspNetUserTokens] (
    [UserId] uniqueidentifier NOT NULL,
    [LoginProvider] nvarchar(450) NOT NULL,
    [Name] nvarchar(450) NOT NULL,
    [Value] nvarchar(max) NULL,
    CONSTRAINT [PK_AspNetUserTokens] PRIMARY KEY ([UserId], [LoginProvider], [Name]),
    CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [Discounts] (
    [Id] int NOT NULL IDENTITY,
    [Code] nvarchar(max) NOT NULL,
    [DiscountPercent] int NOT NULL,
    [DiscountAmount] int NOT NULL,
    [FromDate] Date NOT NULL,
    [ToDate] Date NOT NULL,
    [AppUserId] uniqueidentifier NULL,
    CONSTRAINT [PK_Discounts] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Discounts_AspNetUsers_AppUserId] FOREIGN KEY ([AppUserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE NO ACTION
);
GO

CREATE TABLE [CartItems] (
    [Id] int NOT NULL IDENTITY,
    [Quantity] int NOT NULL,
    [TotalPrice] Money NOT NULL,
    [ProductId] int NULL,
    [AppUserId] uniqueidentifier NULL,
    CONSTRAINT [PK_CartItems] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_CartItems_AspNetUsers_AppUserId] FOREIGN KEY ([AppUserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_CartItems_Products_ProductId] FOREIGN KEY ([ProductId]) REFERENCES [Products] ([Id]) ON DELETE NO ACTION
);
GO

CREATE TABLE [Orders] (
    [Id] int NOT NULL IDENTITY,
    [ShipName] nvarchar(max) NOT NULL,
    [ShipEmail] nvarchar(50) NOT NULL,
    [ShipPhoneNumber] nvarchar(max) NOT NULL,
    [ShipAddress] nvarchar(max) NOT NULL,
    [OrderNotes] nvarchar(max) NULL,
    [TransactionId] nvarchar(max) NULL,
    [Total] Money NOT NULL,
    [Status] int NOT NULL,
    [CreatedDate] Date NOT NULL,
    [AppUserId] uniqueidentifier NULL,
    [DiscountId] int NULL,
    CONSTRAINT [PK_Orders] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Orders_AspNetUsers_AppUserId] FOREIGN KEY ([AppUserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Orders_Discounts_DiscountId] FOREIGN KEY ([DiscountId]) REFERENCES [Discounts] ([Id]) ON DELETE NO ACTION
);
GO

CREATE TABLE [ProductItems] (
    [Id] int NOT NULL IDENTITY,
    [SerialNumber] nvarchar(max) NOT NULL,
    [ReceivedPrice] Money NOT NULL,
    [ReceivedDate] Date NOT NULL,
    [SoldPrice] Money NOT NULL,
    [Status] int NOT NULL,
    [AppUsersId] uniqueidentifier NULL,
    [ProductId] int NULL,
    [OrderId] int NULL,
    CONSTRAINT [PK_ProductItems] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_ProductItems_AspNetUsers_AppUsersId] FOREIGN KEY ([AppUsersId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_ProductItems_Orders_OrderId] FOREIGN KEY ([OrderId]) REFERENCES [Orders] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_ProductItems_Products_ProductId] FOREIGN KEY ([ProductId]) REFERENCES [Products] ([Id]) ON DELETE NO ACTION
);
GO

CREATE TABLE [WarrantyCards] (
    [Id] int NOT NULL IDENTITY,
    [Price] Money NOT NULL,
    [Notes] nvarchar(max) NULL,
    [ReceivedDate] Date NOT NULL,
    [DeliveriedDate] Date NOT NULL,
    [Status] int NOT NULL,
    [StaffID] uniqueidentifier NOT NULL,
    [CustomerID] uniqueidentifier NOT NULL,
    [ProductItemsId] int NULL,
    CONSTRAINT [PK_WarrantyCards] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_WarrantyCards_AspNetUsers_CustomerID] FOREIGN KEY ([CustomerID]) REFERENCES [AspNetUsers] ([Id]),
    CONSTRAINT [FK_WarrantyCards_AspNetUsers_StaffID] FOREIGN KEY ([StaffID]) REFERENCES [AspNetUsers] ([Id]),
    CONSTRAINT [FK_WarrantyCards_ProductItems_ProductItemsId] FOREIGN KEY ([ProductItemsId]) REFERENCES [ProductItems] ([Id]) ON DELETE NO ACTION
);
GO

CREATE INDEX [IX_AspNetRoleClaims_RoleId] ON [AspNetRoleClaims] ([RoleId]);
GO

CREATE UNIQUE INDEX [RoleNameIndex] ON [AspNetRoles] ([NormalizedName]) WHERE [NormalizedName] IS NOT NULL;
GO

CREATE INDEX [IX_AspNetUserClaims_UserId] ON [AspNetUserClaims] ([UserId]);
GO

CREATE INDEX [IX_AspNetUserLogins_UserId] ON [AspNetUserLogins] ([UserId]);
GO

CREATE INDEX [IX_AspNetUserRoles_RoleId] ON [AspNetUserRoles] ([RoleId]);
GO

CREATE INDEX [EmailIndex] ON [AspNetUsers] ([NormalizedEmail]);
GO

CREATE INDEX [IX_AspNetUsers_AppRoleId] ON [AspNetUsers] ([AppRoleId]);
GO

CREATE UNIQUE INDEX [UserNameIndex] ON [AspNetUsers] ([NormalizedUserName]) WHERE [NormalizedUserName] IS NOT NULL;
GO

CREATE INDEX [IX_CartItems_AppUserId] ON [CartItems] ([AppUserId]);
GO

CREATE INDEX [IX_CartItems_ProductId] ON [CartItems] ([ProductId]);
GO

CREATE INDEX [IX_Discounts_AppUserId] ON [Discounts] ([AppUserId]);
GO

CREATE INDEX [IX_Orders_AppUserId] ON [Orders] ([AppUserId]);
GO

CREATE INDEX [IX_Orders_DiscountId] ON [Orders] ([DiscountId]);
GO

CREATE INDEX [IX_ProductItems_AppUsersId] ON [ProductItems] ([AppUsersId]);
GO

CREATE INDEX [IX_ProductItems_OrderId] ON [ProductItems] ([OrderId]);
GO

CREATE INDEX [IX_ProductItems_ProductId] ON [ProductItems] ([ProductId]);
GO

CREATE INDEX [IX_Products_CategoryId] ON [Products] ([CategoryId]);
GO

CREATE INDEX [IX_WarrantyCards_CustomerID] ON [WarrantyCards] ([CustomerID]);
GO

CREATE INDEX [IX_WarrantyCards_ProductItemsId] ON [WarrantyCards] ([ProductItemsId]);
GO

CREATE INDEX [IX_WarrantyCards_StaffID] ON [WarrantyCards] ([StaffID]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20201201083041_InitDb', N'5.0.0');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20201201085132_UpdateDb', N'5.0.0');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20201202053855_updataAppUser', N'5.0.0');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20201202065422_updataEnums', N'5.0.0');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [Products] ADD [WarrantyPeriod] int NOT NULL DEFAULT 0;
GO

ALTER TABLE [ProductItems] ADD [WarrantyPeriod] int NOT NULL DEFAULT 0;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20201204054927_addWarrantyPeriod', N'5.0.0');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20201204062522_addOrder2ProductItem', N'5.0.0');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

DROP TABLE [Contacts];
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20201207063150_deleteContactEntity', N'5.0.0');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

DROP TABLE [CartItems];
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20201207073822_deleteCartItemEntity', N'5.0.0');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [WarrantyCards] DROP CONSTRAINT [FK_WarrantyCards_AspNetUsers_CustomerID];
GO

ALTER TABLE [WarrantyCards] DROP CONSTRAINT [FK_WarrantyCards_AspNetUsers_StaffID];
GO

DROP INDEX [IX_WarrantyCards_CustomerID] ON [WarrantyCards];
GO

DECLARE @var0 sysname;
SELECT @var0 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[WarrantyCards]') AND [c].[name] = N'CustomerID');
IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [WarrantyCards] DROP CONSTRAINT [' + @var0 + '];');
ALTER TABLE [WarrantyCards] DROP COLUMN [CustomerID];
GO

DECLARE @var1 sysname;
SELECT @var1 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Orders]') AND [c].[name] = N'ShipEmail');
IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [Orders] DROP CONSTRAINT [' + @var1 + '];');
ALTER TABLE [Orders] DROP COLUMN [ShipEmail];
GO

EXEC sp_rename N'[WarrantyCards].[StaffID]', N'AppUserId', N'COLUMN';
GO

EXEC sp_rename N'[WarrantyCards].[IX_WarrantyCards_StaffID]', N'IX_WarrantyCards_AppUserId', N'INDEX';
GO

EXEC sp_rename N'[Orders].[TransactionId]', N'Email', N'COLUMN';
GO

EXEC sp_rename N'[Orders].[ShipPhoneNumber]', N'PhoneNumber', N'COLUMN';
GO

EXEC sp_rename N'[Orders].[ShipName]', N'Name', N'COLUMN';
GO

EXEC sp_rename N'[Orders].[ShipAddress]', N'Address', N'COLUMN';
GO

ALTER TABLE [WarrantyCards] ADD CONSTRAINT [FK_WarrantyCards_AspNetUsers_AppUserId] FOREIGN KEY ([AppUserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20201209024609_removeCustomerWarranty', N'5.0.0');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

EXEC sp_rename N'[Orders].[Total]', N'TotalPrice', N'COLUMN';
GO

ALTER TABLE [Orders] ADD [FinalPrice] Money NOT NULL DEFAULT 0.0;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20201209050843_updateOrder', N'5.0.0');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

DROP TABLE [WarrantyCards];
GO

DECLARE @var2 sysname;
SELECT @var2 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[ProductItems]') AND [c].[name] = N'WarrantyPeriod');
IF @var2 IS NOT NULL EXEC(N'ALTER TABLE [ProductItems] DROP CONSTRAINT [' + @var2 + '];');
ALTER TABLE [ProductItems] DROP COLUMN [WarrantyPeriod];
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20201209072128_removeWarrantyCard', N'5.0.0');
GO

COMMIT;
GO

