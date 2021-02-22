BEGIN TRANSACTION;
GO

DROP TABLE [CategoryLanguages];
GO

DROP TABLE [ProductLanguages];
GO

DROP TABLE [Languages];
GO

ALTER TABLE [Products] ADD [Description] nvarchar(max) NULL;
GO

ALTER TABLE [Products] ADD [Name] nvarchar(max) NULL;
GO

ALTER TABLE [Categories] ADD [Description] nvarchar(max) NULL;
GO

ALTER TABLE [Categories] ADD [Name] nvarchar(max) NULL;
GO

UPDATE [Carts] SET [Created_At] = '2020-11-16T17:01:58.6539792+07:00'
WHERE [Id] = 2;
SELECT @@ROWCOUNT;

GO

UPDATE [Roles] SET [ConcurrencyStamp] = N'cf725855-5205-44b4-95b3-14d26efb2e6a'
WHERE [Id] = '8d04dce2-969a-435d-bba4-df3f325983dc';
SELECT @@ROWCOUNT;

GO

UPDATE [Roles] SET [ConcurrencyStamp] = N'ce3da197-33fd-43e8-bfa6-7b7cd05d52cf'
WHERE [Id] = '8d04dce2-969a-435d-bba4-df3f325983dd';
SELECT @@ROWCOUNT;

GO

UPDATE [Users] SET [ConcurrencyStamp] = N'43a12b2e-619f-4491-8ccd-d87f8dcad3bc', [PasswordHash] = N'AQAAAAEAACcQAAAAEGcnzFw41cwFK4blJvmkcMYJUGtRWWkh/YSBIu7YISw8SLD6mrCwWVz8ChXqMMMQ5w=='
WHERE [Id] = '69bd714f-9576-45ba-b5b7-f00649be00de';
SELECT @@ROWCOUNT;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20201116100159_deleteLanguageClass', N'5.0.0');
GO

COMMIT;
GO

