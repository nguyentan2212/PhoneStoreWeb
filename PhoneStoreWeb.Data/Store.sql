CREATE TABLE [Categories] (
    [Id] int NOT NULL IDENTITY,
    [IsShowOnHome] bit NOT NULL,
    [Status] int NOT NULL,
    [Created_At] Date NOT NULL,
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


CREATE TABLE [Languages] (
    [Id] nvarchar(450) NOT NULL,
    [Name] nvarchar(max) NOT NULL,
    [IsDefault] bit NOT NULL,
    CONSTRAINT [PK_Languages] PRIMARY KEY ([Id])
);
GO


CREATE TABLE [Promotions] (
    [Id] int NOT NULL IDENTITY,
    [Code] nvarchar(max) NOT NULL,
    [DiscountPercent] int NOT NULL,
    [DiscountAmount] int NOT NULL,
    [FromDate] Date NOT NULL,
    [ToDate] Date NOT NULL,
    CONSTRAINT [PK_Promotions] PRIMARY KEY ([Id])
);
GO


CREATE TABLE [RoleClaims] (
    [Id] int NOT NULL IDENTITY,
    [RoleId] uniqueidentifier NOT NULL,
    [ClaimType] nvarchar(max) NULL,
    [ClaimValue] nvarchar(max) NULL,
    CONSTRAINT [PK_RoleClaims] PRIMARY KEY ([Id])
);
GO


CREATE TABLE [Roles] (
    [Id] uniqueidentifier NOT NULL,
    [Name] nvarchar(max) NULL,
    [NormalizedName] nvarchar(max) NULL,
    [ConcurrencyStamp] nvarchar(max) NULL,
    [Description] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_Roles] PRIMARY KEY ([Id])
);
GO


CREATE TABLE [UserClaims] (
    [Id] int NOT NULL IDENTITY,
    [UserId] uniqueidentifier NOT NULL,
    [ClaimType] nvarchar(max) NULL,
    [ClaimValue] nvarchar(max) NULL,
    CONSTRAINT [PK_UserClaims] PRIMARY KEY ([Id])
);
GO


CREATE TABLE [UserLogins] (
    [UserId] uniqueidentifier NOT NULL,
    [LoginProvider] nvarchar(max) NULL,
    [ProviderKey] nvarchar(max) NULL,
    [ProviderDisplayName] nvarchar(max) NULL,
    CONSTRAINT [PK_UserLogins] PRIMARY KEY ([UserId])
);
GO


CREATE TABLE [UserRoles] (
    [UserId] uniqueidentifier NOT NULL,
    [RoleId] uniqueidentifier NOT NULL
);
GO


CREATE TABLE [UserTokens] (
    [UserId] uniqueidentifier NOT NULL,
    [LoginProvider] nvarchar(max) NULL,
    [Name] nvarchar(max) NULL,
    [Value] nvarchar(max) NULL,
    CONSTRAINT [PK_UserTokens] PRIMARY KEY ([UserId])
);
GO


CREATE TABLE [Products] (
    [Id] int NOT NULL IDENTITY,
    [CategoryId] int NOT NULL,
    [Price] Money NOT NULL,
    [OriginalPrice] Money NOT NULL,
    [Stock] int NOT NULL,
    [Created_At] datetime2 NOT NULL,
    [OS] nvarchar(max) NULL,
    [RAM] int NOT NULL,
    [Storage] int NOT NULL,
    [BatteryCapacity] int NOT NULL,
    [HasQuickCharge] bit NOT NULL,
    CONSTRAINT [PK_Products] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Products_Categories_CategoryId] FOREIGN KEY ([CategoryId]) REFERENCES [Categories] ([Id]) ON DELETE CASCADE
);
GO


CREATE TABLE [CategoryLanguages] (
    [CategoryId] int NOT NULL,
    [LanguageId] nvarchar(450) NOT NULL,
    [Name] nvarchar(max) NULL,
    [CategoryUrl] nvarchar(max) NULL,
    CONSTRAINT [PK_CategoryLanguages] PRIMARY KEY ([CategoryId], [LanguageId]),
    CONSTRAINT [FK_CategoryLanguages_Categories_CategoryId] FOREIGN KEY ([CategoryId]) REFERENCES [Categories] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_CategoryLanguages_Languages_LanguageId] FOREIGN KEY ([LanguageId]) REFERENCES [Languages] ([Id]) ON DELETE CASCADE
);
GO


CREATE TABLE [Users] (
    [Id] uniqueidentifier NOT NULL,
    [UserName] nvarchar(max) NULL,
    [NormalizedUserName] nvarchar(max) NULL,
    [Email] nvarchar(max) NULL,
    [NormalizedEmail] nvarchar(max) NULL,
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
    [Dob] Date NOT NULL,
    [FullName] nvarchar(max) NULL,
    [Address] nvarchar(max) NULL,
    [ImagePath] nvarchar(max) NULL,
    [RoleID] uniqueidentifier NOT NULL,
    [AppRoleId] uniqueidentifier NULL,
    CONSTRAINT [PK_Users] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Users_Roles_AppRoleId] FOREIGN KEY ([AppRoleId]) REFERENCES [Roles] ([Id]) ON DELETE NO ACTION
);
GO


CREATE TABLE [ProductImages] (
    [Id] int NOT NULL IDENTITY,
    [ProductId] int NOT NULL,
    [ImagePath] nvarchar(max) NOT NULL,
    [Caption] nvarchar(max) NULL,
    [FileSize] bigint NOT NULL,
    [IsDefault] bit NOT NULL,
    CONSTRAINT [PK_ProductImages] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_ProductImages_Products_ProductId] FOREIGN KEY ([ProductId]) REFERENCES [Products] ([Id]) ON DELETE CASCADE
);
GO


CREATE TABLE [ProductLanguages] (
    [ProductId] int NOT NULL,
    [LanguageId] nvarchar(450) NOT NULL,
    [Name] nvarchar(max) NULL,
    [Description] nvarchar(max) NULL,
    [ProductUrl] nvarchar(max) NULL,
    CONSTRAINT [PK_ProductLanguages] PRIMARY KEY ([ProductId], [LanguageId]),
    CONSTRAINT [FK_ProductLanguages_Languages_LanguageId] FOREIGN KEY ([LanguageId]) REFERENCES [Languages] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_ProductLanguages_Products_ProductId] FOREIGN KEY ([ProductId]) REFERENCES [Products] ([Id]) ON DELETE CASCADE
);
GO


CREATE TABLE [ReViews] (
    [Id] int NOT NULL IDENTITY,
    [ProductId] int NOT NULL,
    [Created_At] Date NOT NULL,
    [Content] nvarchar(max) NOT NULL,
    [Email] nvarchar(max) NOT NULL,
    [Name] nvarchar(max) NOT NULL,
    [Rating] int NOT NULL,
    CONSTRAINT [PK_ReViews] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_ReViews_Products_ProductId] FOREIGN KEY ([ProductId]) REFERENCES [Products] ([Id]) ON DELETE CASCADE
);
GO


CREATE TABLE [Blogs] (
    [Id] int NOT NULL IDENTITY,
    [Content] nvarchar(max) NOT NULL,
    [Title] nvarchar(max) NULL,
    [LikeCount] int NOT NULL,
    [ImagePath] nvarchar(max) NULL,
    [Created_At] Date NOT NULL,
    [UserId] uniqueidentifier NOT NULL,
    [CategoryId] int NOT NULL,
    [AppUserId] uniqueidentifier NULL,
    CONSTRAINT [PK_Blogs] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Blogs_Users_AppUserId] FOREIGN KEY ([AppUserId]) REFERENCES [Users] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Blogs_Categories_CategoryId] FOREIGN KEY ([CategoryId]) REFERENCES [Categories] ([Id]) ON DELETE CASCADE
);
GO


CREATE TABLE [Carts] (
    [Id] int NOT NULL IDENTITY,
    [Price] decimal(18,2) NOT NULL,
    [Created_At] Date NOT NULL,
    [UserId] uniqueidentifier NOT NULL,
    [AppUserId] uniqueidentifier NULL,
    CONSTRAINT [PK_Carts] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Carts_Users_AppUserId] FOREIGN KEY ([AppUserId]) REFERENCES [Users] ([Id]) ON DELETE NO ACTION
);
GO


CREATE TABLE [Orders] (
    [Id] int NOT NULL IDENTITY,
    [UserId] uniqueidentifier NULL,
    [AppUserId] uniqueidentifier NULL,
    [PromotionId] int NULL,
    [ShipName] nvarchar(max) NOT NULL,
    [ShipEmail] nvarchar(50) NOT NULL,
    [ShipPhone] nvarchar(max) NOT NULL,
    [ShipAddress] nvarchar(max) NOT NULL,
    [OrderNotes] nvarchar(max) NULL,
    [TransactionId] nvarchar(max) NULL,
    [Total] decimal(18,2) NOT NULL,
    [Status] int NOT NULL,
    [Created_At] Date NOT NULL,
    CONSTRAINT [PK_Orders] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Orders_Users_AppUserId] FOREIGN KEY ([AppUserId]) REFERENCES [Users] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Orders_Promotions_PromotionId] FOREIGN KEY ([PromotionId]) REFERENCES [Promotions] ([Id]) ON DELETE NO ACTION
);
GO


CREATE TABLE [Comments] (
    [Id] int NOT NULL IDENTITY,
    [UserId] uniqueidentifier NOT NULL,
    [AppUserId] uniqueidentifier NULL,
    [BlogId] int NOT NULL,
    [Created_At] datetime2 NOT NULL,
    [Content] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_Comments] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Comments_Users_AppUserId] FOREIGN KEY ([AppUserId]) REFERENCES [Users] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Comments_Blogs_BlogId] FOREIGN KEY ([BlogId]) REFERENCES [Blogs] ([Id]) ON DELETE CASCADE
);
GO


CREATE TABLE [Likes] (
    [Id] int NOT NULL IDENTITY,
    [UserId] uniqueidentifier NOT NULL,
    [AppUserId] uniqueidentifier NULL,
    [BlogId] int NOT NULL,
    CONSTRAINT [PK_Likes] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Likes_Users_AppUserId] FOREIGN KEY ([AppUserId]) REFERENCES [Users] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Likes_Blogs_BlogId] FOREIGN KEY ([BlogId]) REFERENCES [Blogs] ([Id]) ON DELETE CASCADE
);
GO


CREATE TABLE [CartProducts] (
    [ProductID] int NOT NULL,
    [CartID] int NOT NULL,
    [Quantity] int NOT NULL,
    CONSTRAINT [PK_CartProducts] PRIMARY KEY ([CartID], [ProductID]),
    CONSTRAINT [FK_CartProducts_Carts_CartID] FOREIGN KEY ([CartID]) REFERENCES [Carts] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_CartProducts_Products_ProductID] FOREIGN KEY ([ProductID]) REFERENCES [Products] ([Id]) ON DELETE CASCADE
);
GO


CREATE TABLE [OrderDetails] (
    [OrderId] int NOT NULL,
    [ProductId] int NOT NULL,
    [Price] decimal(18,2) NOT NULL,
    [Quantity] int NOT NULL,
    CONSTRAINT [PK_OrderDetails] PRIMARY KEY ([OrderId], [ProductId]),
    CONSTRAINT [FK_OrderDetails_Orders_OrderId] FOREIGN KEY ([OrderId]) REFERENCES [Orders] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_OrderDetails_Products_ProductId] FOREIGN KEY ([ProductId]) REFERENCES [Products] ([Id]) ON DELETE CASCADE
);
GO


IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'AppUserId', N'Created_At', N'Price', N'UserId') AND [object_id] = OBJECT_ID(N'[Carts]'))
    SET IDENTITY_INSERT [Carts] ON;
INSERT INTO [Carts] ([Id], [AppUserId], [Created_At], [Price], [UserId])
VALUES (1, NULL, '0001-01-01T00:00:00.0000000', 20000.0, '00000000-0000-0000-0000-000000000000'),
(2, NULL, '2020-11-12T13:29:25.9739784+07:00', 0.0, '69bd714f-9576-45ba-b5b7-f00649be00de');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'AppUserId', N'Created_At', N'Price', N'UserId') AND [object_id] = OBJECT_ID(N'[Carts]'))
    SET IDENTITY_INSERT [Carts] OFF;
GO


IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Created_At', N'ImagePath', N'IsShowOnHome', N'Status') AND [object_id] = OBJECT_ID(N'[Categories]'))
    SET IDENTITY_INSERT [Categories] ON;
INSERT INTO [Categories] ([Id], [Created_At], [ImagePath], [IsShowOnHome], [Status])
VALUES (1, '0001-01-01T00:00:00.0000000', NULL, CAST(1 AS bit), 1);
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Created_At', N'ImagePath', N'IsShowOnHome', N'Status') AND [object_id] = OBJECT_ID(N'[Categories]'))
    SET IDENTITY_INSERT [Categories] OFF;
GO


IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Email', N'Message', N'Name') AND [object_id] = OBJECT_ID(N'[Contacts]'))
    SET IDENTITY_INSERT [Contacts] ON;
INSERT INTO [Contacts] ([Id], [Email], [Message], [Name])
VALUES (1, N'hieuvo044@gmail.com', N'Very good', N'Võ Trung Hiếu'),
(2, N'hieuvo044@gmail.com', N'Very good', N'Phuong Quyen'),
(3, N'hieuvo044@gmail.com', N'Very good', N'Võ Trung Hiếu');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Email', N'Message', N'Name') AND [object_id] = OBJECT_ID(N'[Contacts]'))
    SET IDENTITY_INSERT [Contacts] OFF;
GO


IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'IsDefault', N'Name') AND [object_id] = OBJECT_ID(N'[Languages]'))
    SET IDENTITY_INSERT [Languages] ON;
INSERT INTO [Languages] ([Id], [IsDefault], [Name])
VALUES (N'vn', CAST(0 AS bit), N'VIETNAM'),
(N'en', CAST(0 AS bit), N'ENGLISH');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'IsDefault', N'Name') AND [object_id] = OBJECT_ID(N'[Languages]'))
    SET IDENTITY_INSERT [Languages] OFF;
GO


IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Code', N'DiscountAmount', N'DiscountPercent', N'FromDate', N'ToDate') AND [object_id] = OBJECT_ID(N'[Promotions]'))
    SET IDENTITY_INSERT [Promotions] ON;
INSERT INTO [Promotions] ([Id], [Code], [DiscountAmount], [DiscountPercent], [FromDate], [ToDate])
VALUES (1, N'123', 10000, 0, '0001-01-01T00:00:00.0000000', '0001-01-01T00:00:00.0000000'),
(2, N'1234', 20000, 0, '0001-01-01T00:00:00.0000000', '0001-01-01T00:00:00.0000000');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Code', N'DiscountAmount', N'DiscountPercent', N'FromDate', N'ToDate') AND [object_id] = OBJECT_ID(N'[Promotions]'))
    SET IDENTITY_INSERT [Promotions] OFF;
GO


IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'ConcurrencyStamp', N'Description', N'Name', N'NormalizedName') AND [object_id] = OBJECT_ID(N'[Roles]'))
    SET IDENTITY_INSERT [Roles] ON;
INSERT INTO [Roles] ([Id], [ConcurrencyStamp], [Description], [Name], [NormalizedName])
VALUES ('8d04dce2-969a-435d-bba4-df3f325983dc', N'358727b0-7227-49f7-ad5a-45ba4ebc76f6', N'Administrator role', N'admin', N'admin'),
('8d04dce2-969a-435d-bba4-df3f325983dd', N'eb7197b3-7815-4a56-b579-f00bdbd83f61', N'Client role', N'client', N'client');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'ConcurrencyStamp', N'Description', N'Name', N'NormalizedName') AND [object_id] = OBJECT_ID(N'[Roles]'))
    SET IDENTITY_INSERT [Roles] OFF;
GO


IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'AccessFailedCount', N'Address', N'AppRoleId', N'ConcurrencyStamp', N'Dob', N'Email', N'EmailConfirmed', N'FullName', N'ImagePath', N'LockoutEnabled', N'LockoutEnd', N'NormalizedEmail', N'NormalizedUserName', N'PasswordHash', N'PhoneNumber', N'PhoneNumberConfirmed', N'RoleID', N'SecurityStamp', N'TwoFactorEnabled', N'UserName') AND [object_id] = OBJECT_ID(N'[Users]'))
    SET IDENTITY_INSERT [Users] ON;
INSERT INTO [Users] ([Id], [AccessFailedCount], [Address], [AppRoleId], [ConcurrencyStamp], [Dob], [Email], [EmailConfirmed], [FullName], [ImagePath], [LockoutEnabled], [LockoutEnd], [NormalizedEmail], [NormalizedUserName], [PasswordHash], [PhoneNumber], [PhoneNumberConfirmed], [RoleID], [SecurityStamp], [TwoFactorEnabled], [UserName])
VALUES ('69bd714f-9576-45ba-b5b7-f00649be00de', 0, NULL, NULL, N'a894cb16-cfae-4163-aae8-aad7003c8206', '2020-01-31T00:00:00.0000000', N'hieuvo044@gmail.com', CAST(1 AS bit), NULL, NULL, CAST(0 AS bit), NULL, N'hieuvo044@gmail.com', N'admin', N'AQAAAAEAACcQAAAAEJNBSwhxWXtdjou3J3CttyZPu+huqLQYcXtSUH4z91Z98/FikwPDXTl9YYqWu0Cc7g==', NULL, CAST(0 AS bit), '8d04dce2-969a-435d-bba4-df3f325983dc', N'', CAST(0 AS bit), N'admin');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'AccessFailedCount', N'Address', N'AppRoleId', N'ConcurrencyStamp', N'Dob', N'Email', N'EmailConfirmed', N'FullName', N'ImagePath', N'LockoutEnabled', N'LockoutEnd', N'NormalizedEmail', N'NormalizedUserName', N'PasswordHash', N'PhoneNumber', N'PhoneNumberConfirmed', N'RoleID', N'SecurityStamp', N'TwoFactorEnabled', N'UserName') AND [object_id] = OBJECT_ID(N'[Users]'))
    SET IDENTITY_INSERT [Users] OFF;
GO


IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'CategoryId', N'LanguageId', N'CategoryUrl', N'Name') AND [object_id] = OBJECT_ID(N'[CategoryLanguages]'))
    SET IDENTITY_INSERT [CategoryLanguages] ON;
INSERT INTO [CategoryLanguages] ([CategoryId], [LanguageId], [CategoryUrl], [Name])
VALUES (1, N'vn', N'banh-ngot', N'Bánh ngọt');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'CategoryId', N'LanguageId', N'CategoryUrl', N'Name') AND [object_id] = OBJECT_ID(N'[CategoryLanguages]'))
    SET IDENTITY_INSERT [CategoryLanguages] OFF;
GO


IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'CategoryId', N'LanguageId', N'CategoryUrl', N'Name') AND [object_id] = OBJECT_ID(N'[CategoryLanguages]'))
    SET IDENTITY_INSERT [CategoryLanguages] ON;
INSERT INTO [CategoryLanguages] ([CategoryId], [LanguageId], [CategoryUrl], [Name])
VALUES (1, N'en', N'cake', N'Cake');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'CategoryId', N'LanguageId', N'CategoryUrl', N'Name') AND [object_id] = OBJECT_ID(N'[CategoryLanguages]'))
    SET IDENTITY_INSERT [CategoryLanguages] OFF;
GO


IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'BatteryCapacity', N'CategoryId', N'Created_At', N'HasQuickCharge', N'OS', N'OriginalPrice', N'Price', N'RAM', N'Stock', N'Storage') AND [object_id] = OBJECT_ID(N'[Products]'))
    SET IDENTITY_INSERT [Products] ON;
INSERT INTO [Products] ([Id], [BatteryCapacity], [CategoryId], [Created_At], [HasQuickCharge], [OS], [OriginalPrice], [Price], [RAM], [Stock], [Storage])
VALUES (1, 0, 1, '0001-01-01T00:00:00.0000000', CAST(0 AS bit), NULL, 17000.0, 20000.0, 0, 0, 0);
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'BatteryCapacity', N'CategoryId', N'Created_At', N'HasQuickCharge', N'OS', N'OriginalPrice', N'Price', N'RAM', N'Stock', N'Storage') AND [object_id] = OBJECT_ID(N'[Products]'))
    SET IDENTITY_INSERT [Products] OFF;
GO


IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'CartID', N'ProductID', N'Quantity') AND [object_id] = OBJECT_ID(N'[CartProducts]'))
    SET IDENTITY_INSERT [CartProducts] ON;
INSERT INTO [CartProducts] ([CartID], [ProductID], [Quantity])
VALUES (1, 1, 2);
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'CartID', N'ProductID', N'Quantity') AND [object_id] = OBJECT_ID(N'[CartProducts]'))
    SET IDENTITY_INSERT [CartProducts] OFF;
GO


IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Caption', N'FileSize', N'ImagePath', N'IsDefault', N'ProductId') AND [object_id] = OBJECT_ID(N'[ProductImages]'))
    SET IDENTITY_INSERT [ProductImages] ON;
INSERT INTO [ProductImages] ([Id], [Caption], [FileSize], [ImagePath], [IsDefault], [ProductId])
VALUES (1, N'!23', CAST(0 AS bigint), N'http://product.hstatic.net/1000026716/product/81ax00mcvn_bd76b8bf0aed4307bc9714e4dc5830f0_large.jpg', CAST(1 AS bit), 1),
(2, N'!23', CAST(0 AS bigint), N'http://product.hstatic.net/1000026716/product/81ax00mcvn_bd76b8bf0aed4307bc9714e4dc5830f0_large.jpg', CAST(0 AS bit), 1);
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Caption', N'FileSize', N'ImagePath', N'IsDefault', N'ProductId') AND [object_id] = OBJECT_ID(N'[ProductImages]'))
    SET IDENTITY_INSERT [ProductImages] OFF;
GO


IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'ProductId', N'LanguageId', N'Description', N'Name', N'ProductUrl') AND [object_id] = OBJECT_ID(N'[ProductLanguages]'))
    SET IDENTITY_INSERT [ProductLanguages] ON;
INSERT INTO [ProductLanguages] ([ProductId], [LanguageId], [Description], [Name], [ProductUrl])
VALUES (1, N'vn', N'This is banh ngot 1', N'Bánh ngọt1', N'banh-ngot1'),
(1, N'en', N'This is cake1', N'Cake1', N'cake1');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'ProductId', N'LanguageId', N'Description', N'Name', N'ProductUrl') AND [object_id] = OBJECT_ID(N'[ProductLanguages]'))
    SET IDENTITY_INSERT [ProductLanguages] OFF;
GO


CREATE INDEX [IX_Blogs_AppUserId] ON [Blogs] ([AppUserId]);
GO


CREATE INDEX [IX_Blogs_CategoryId] ON [Blogs] ([CategoryId]);
GO


CREATE INDEX [IX_CartProducts_ProductID] ON [CartProducts] ([ProductID]);
GO


CREATE INDEX [IX_Carts_AppUserId] ON [Carts] ([AppUserId]);
GO


CREATE INDEX [IX_CategoryLanguages_LanguageId] ON [CategoryLanguages] ([LanguageId]);
GO


CREATE INDEX [IX_Comments_AppUserId] ON [Comments] ([AppUserId]);
GO


CREATE INDEX [IX_Comments_BlogId] ON [Comments] ([BlogId]);
GO


CREATE INDEX [IX_Likes_AppUserId] ON [Likes] ([AppUserId]);
GO


CREATE INDEX [IX_Likes_BlogId] ON [Likes] ([BlogId]);
GO


CREATE UNIQUE INDEX [IX_OrderDetails_ProductId] ON [OrderDetails] ([ProductId]);
GO


CREATE INDEX [IX_Orders_AppUserId] ON [Orders] ([AppUserId]);
GO


CREATE UNIQUE INDEX [IX_Orders_PromotionId] ON [Orders] ([PromotionId]) WHERE [PromotionId] IS NOT NULL;
GO


CREATE INDEX [IX_ProductImages_ProductId] ON [ProductImages] ([ProductId]);
GO


CREATE INDEX [IX_ProductLanguages_LanguageId] ON [ProductLanguages] ([LanguageId]);
GO


CREATE INDEX [IX_Products_CategoryId] ON [Products] ([CategoryId]);
GO


CREATE INDEX [IX_ReViews_ProductId] ON [ReViews] ([ProductId]);
GO


CREATE INDEX [IX_Users_AppRoleId] ON [Users] ([AppRoleId]);
GO


