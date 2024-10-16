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

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240829134619_Create Libook DB'
)
BEGIN
    CREATE TABLE [Authors] (
        [Id] uniqueidentifier NOT NULL,
        [Name] nvarchar(max) NOT NULL,
        CONSTRAINT [PK_Authors] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240829134619_Create Libook DB'
)
BEGIN
    CREATE TABLE [Categories] (
        [Id] uniqueidentifier NOT NULL,
        [Name] nvarchar(max) NOT NULL,
        CONSTRAINT [PK_Categories] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240829134619_Create Libook DB'
)
BEGIN
    CREATE TABLE [Conversations] (
        [Id] uniqueidentifier NOT NULL,
        [Name] nvarchar(max) NULL,
        [CreatedAt] datetime2 NOT NULL,
        CONSTRAINT [PK_Conversations] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240829134619_Create Libook DB'
)
BEGIN
    CREATE TABLE [Suppliers] (
        [Id] uniqueidentifier NOT NULL,
        [Name] nvarchar(max) NOT NULL,
        CONSTRAINT [PK_Suppliers] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240829134619_Create Libook DB'
)
BEGIN
    CREATE TABLE [Vouchers] (
        [VoucherId] uniqueidentifier NOT NULL,
        [Title] nvarchar(max) NOT NULL,
        [StartDate] datetime2 NOT NULL,
        [EndDate] datetime2 NOT NULL,
        [Discount] float NOT NULL,
        [Remain] int NOT NULL,
        CONSTRAINT [PK_Vouchers] PRIMARY KEY ([VoucherId])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240829134619_Create Libook DB'
)
BEGIN
    CREATE TABLE [Messages] (
        [Id] uniqueidentifier NOT NULL,
        [Content] nvarchar(max) NOT NULL,
        [SendAt] datetime2 NOT NULL,
        [ConversationId] uniqueidentifier NOT NULL,
        [UserId] uniqueidentifier NOT NULL,
        CONSTRAINT [PK_Messages] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Messages_Conversations_ConversationId] FOREIGN KEY ([ConversationId]) REFERENCES [Conversations] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240829134619_Create Libook DB'
)
BEGIN
    CREATE TABLE [Participants] (
        [Id] uniqueidentifier NOT NULL,
        [JoinedAt] datetime2 NOT NULL,
        [ConversationId] uniqueidentifier NOT NULL,
        [UserId] uniqueidentifier NOT NULL,
        CONSTRAINT [PK_Participants] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Participants_Conversations_ConversationId] FOREIGN KEY ([ConversationId]) REFERENCES [Conversations] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240829134619_Create Libook DB'
)
BEGIN
    CREATE TABLE [Books] (
        [Id] uniqueidentifier NOT NULL,
        [Name] nvarchar(max) NOT NULL,
        [Description] nvarchar(max) NOT NULL,
        [Price] float NOT NULL,
        [PrecentDiscount] real NOT NULL,
        [Remain] int NOT NULL,
        [isActive] bit NOT NULL,
        [AuthorId] uniqueidentifier NOT NULL,
        [CategoryId] uniqueidentifier NOT NULL,
        [SupplierId] uniqueidentifier NOT NULL,
        CONSTRAINT [PK_Books] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Books_Authors_AuthorId] FOREIGN KEY ([AuthorId]) REFERENCES [Authors] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_Books_Categories_CategoryId] FOREIGN KEY ([CategoryId]) REFERENCES [Categories] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_Books_Suppliers_SupplierId] FOREIGN KEY ([SupplierId]) REFERENCES [Suppliers] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240829134619_Create Libook DB'
)
BEGIN
    CREATE TABLE [Orders] (
        [OrderId] uniqueidentifier NOT NULL,
        [DateCreate] datetime2 NOT NULL,
        [Amount] float NOT NULL,
        [UserId] uniqueidentifier NOT NULL,
        [VoucherId] uniqueidentifier NULL,
        CONSTRAINT [PK_Orders] PRIMARY KEY ([OrderId]),
        CONSTRAINT [FK_Orders_Vouchers_VoucherId] FOREIGN KEY ([VoucherId]) REFERENCES [Vouchers] ([VoucherId])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240829134619_Create Libook DB'
)
BEGIN
    CREATE TABLE [VoucherActiveds] (
        [VoucherId] uniqueidentifier NOT NULL,
        [UserId] uniqueidentifier NOT NULL,
        CONSTRAINT [PK_VoucherActiveds] PRIMARY KEY ([VoucherId], [UserId]),
        CONSTRAINT [FK_VoucherActiveds_Vouchers_VoucherId] FOREIGN KEY ([VoucherId]) REFERENCES [Vouchers] ([VoucherId]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240829134619_Create Libook DB'
)
BEGIN
    CREATE TABLE [BookImages] (
        [Id] uniqueidentifier NOT NULL,
        [BookImageUrl] nvarchar(max) NULL,
        [BookId] uniqueidentifier NOT NULL,
        CONSTRAINT [PK_BookImages] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_BookImages_Books_BookId] FOREIGN KEY ([BookId]) REFERENCES [Books] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240829134619_Create Libook DB'
)
BEGIN
    CREATE TABLE [Comments] (
        [Id] uniqueidentifier NOT NULL,
        [DateCreate] datetime2 NOT NULL,
        [Content] nvarchar(max) NOT NULL,
        [UserId] uniqueidentifier NOT NULL,
        [BookId] uniqueidentifier NULL,
        CONSTRAINT [PK_Comments] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Comments_Books_BookId] FOREIGN KEY ([BookId]) REFERENCES [Books] ([Id])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240829134619_Create Libook DB'
)
BEGIN
    CREATE TABLE [OrderDetails] (
        [Id] uniqueidentifier NOT NULL,
        [UnitPrice] float NOT NULL,
        [Quantity] int NOT NULL,
        [OrderId] uniqueidentifier NOT NULL,
        [BookId] uniqueidentifier NOT NULL,
        CONSTRAINT [PK_OrderDetails] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_OrderDetails_Books_BookId] FOREIGN KEY ([BookId]) REFERENCES [Books] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_OrderDetails_Orders_OrderId] FOREIGN KEY ([OrderId]) REFERENCES [Orders] ([OrderId]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240829134619_Create Libook DB'
)
BEGIN
    CREATE TABLE [OrderStatuses] (
        [Id] uniqueidentifier NOT NULL,
        [Status] nvarchar(max) NOT NULL,
        [DateCreate] datetime2 NOT NULL,
        [OrderId] uniqueidentifier NOT NULL,
        CONSTRAINT [PK_OrderStatuses] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_OrderStatuses_Orders_OrderId] FOREIGN KEY ([OrderId]) REFERENCES [Orders] ([OrderId]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240829134619_Create Libook DB'
)
BEGIN
    CREATE TABLE [CommentImages] (
        [Id] uniqueidentifier NOT NULL,
        [CommentImageUrl] nvarchar(max) NULL,
        [CommentId] uniqueidentifier NOT NULL,
        CONSTRAINT [PK_CommentImages] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_CommentImages_Comments_CommentId] FOREIGN KEY ([CommentId]) REFERENCES [Comments] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240829134619_Create Libook DB'
)
BEGIN
    CREATE INDEX [IX_BookImages_BookId] ON [BookImages] ([BookId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240829134619_Create Libook DB'
)
BEGIN
    CREATE INDEX [IX_Books_AuthorId] ON [Books] ([AuthorId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240829134619_Create Libook DB'
)
BEGIN
    CREATE INDEX [IX_Books_CategoryId] ON [Books] ([CategoryId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240829134619_Create Libook DB'
)
BEGIN
    CREATE INDEX [IX_Books_SupplierId] ON [Books] ([SupplierId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240829134619_Create Libook DB'
)
BEGIN
    CREATE INDEX [IX_CommentImages_CommentId] ON [CommentImages] ([CommentId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240829134619_Create Libook DB'
)
BEGIN
    CREATE INDEX [IX_Comments_BookId] ON [Comments] ([BookId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240829134619_Create Libook DB'
)
BEGIN
    CREATE INDEX [IX_Messages_ConversationId] ON [Messages] ([ConversationId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240829134619_Create Libook DB'
)
BEGIN
    CREATE INDEX [IX_OrderDetails_BookId] ON [OrderDetails] ([BookId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240829134619_Create Libook DB'
)
BEGIN
    CREATE INDEX [IX_OrderDetails_OrderId] ON [OrderDetails] ([OrderId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240829134619_Create Libook DB'
)
BEGIN
    CREATE INDEX [IX_Orders_VoucherId] ON [Orders] ([VoucherId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240829134619_Create Libook DB'
)
BEGIN
    CREATE INDEX [IX_OrderStatuses_OrderId] ON [OrderStatuses] ([OrderId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240829134619_Create Libook DB'
)
BEGIN
    CREATE INDEX [IX_Participants_ConversationId] ON [Participants] ([ConversationId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240829134619_Create Libook DB'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20240829134619_Create Libook DB', N'8.0.8');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240830025433_Add OrderInfo table'
)
BEGIN
    ALTER TABLE [Orders] ADD [OrderInfoId] uniqueidentifier NOT NULL DEFAULT '00000000-0000-0000-0000-000000000000';
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240830025433_Add OrderInfo table'
)
BEGIN
    CREATE TABLE [OrderInfo] (
        [Id] uniqueidentifier NOT NULL,
        [Name] nvarchar(max) NOT NULL,
        [Phone] int NOT NULL,
        [ProvinceId] nvarchar(20) NOT NULL,
        [DistrictId] nvarchar(20) NOT NULL,
        [WardId] nvarchar(20) NOT NULL,
        [Address] nvarchar(max) NOT NULL,
        [UserId] uniqueidentifier NOT NULL,
        CONSTRAINT [PK_OrderInfo] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_OrderInfo_districts_DistrictId] FOREIGN KEY ([DistrictId]) REFERENCES [districts] ([code]) ON DELETE CASCADE,
        CONSTRAINT [FK_OrderInfo_provinces_ProvinceId] FOREIGN KEY ([ProvinceId]) REFERENCES [provinces] ([code]) ON DELETE CASCADE,
        CONSTRAINT [FK_OrderInfo_wards_WardId] FOREIGN KEY ([WardId]) REFERENCES [wards] ([code]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240830025433_Add OrderInfo table'
)
BEGIN
    CREATE INDEX [IX_Orders_OrderInfoId] ON [Orders] ([OrderInfoId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240830025433_Add OrderInfo table'
)
BEGIN
    CREATE INDEX [IX_OrderInfo_DistrictId] ON [OrderInfo] ([DistrictId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240830025433_Add OrderInfo table'
)
BEGIN
    CREATE INDEX [IX_OrderInfo_ProvinceId] ON [OrderInfo] ([ProvinceId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240830025433_Add OrderInfo table'
)
BEGIN
    CREATE INDEX [IX_OrderInfo_WardId] ON [OrderInfo] ([WardId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240830025433_Add OrderInfo table'
)
BEGIN
    ALTER TABLE [Orders] ADD CONSTRAINT [FK_Orders_OrderInfo_OrderInfoId] FOREIGN KEY ([OrderInfoId]) REFERENCES [OrderInfo] ([Id]) ON DELETE CASCADE;
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240830025433_Add OrderInfo table'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20240830025433_Add OrderInfo table', N'8.0.8');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240830030927_Add OrderInfo table2'
)
BEGIN
    ALTER TABLE [OrderInfo] DROP CONSTRAINT [FK_OrderInfo_districts_DistrictId];
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240830030927_Add OrderInfo table2'
)
BEGIN
    ALTER TABLE [OrderInfo] DROP CONSTRAINT [FK_OrderInfo_provinces_ProvinceId];
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240830030927_Add OrderInfo table2'
)
BEGIN
    ALTER TABLE [OrderInfo] DROP CONSTRAINT [FK_OrderInfo_wards_WardId];
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240830030927_Add OrderInfo table2'
)
BEGIN
    ALTER TABLE [Orders] DROP CONSTRAINT [FK_Orders_OrderInfo_OrderInfoId];
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240830030927_Add OrderInfo table2'
)
BEGIN
    ALTER TABLE [OrderInfo] DROP CONSTRAINT [PK_OrderInfo];
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240830030927_Add OrderInfo table2'
)
BEGIN
    EXEC sp_rename N'[OrderInfo]', N'OrderInfos';
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240830030927_Add OrderInfo table2'
)
BEGIN
    EXEC sp_rename N'[OrderInfos].[IX_OrderInfo_WardId]', N'IX_OrderInfos_WardId', N'INDEX';
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240830030927_Add OrderInfo table2'
)
BEGIN
    EXEC sp_rename N'[OrderInfos].[IX_OrderInfo_ProvinceId]', N'IX_OrderInfos_ProvinceId', N'INDEX';
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240830030927_Add OrderInfo table2'
)
BEGIN
    EXEC sp_rename N'[OrderInfos].[IX_OrderInfo_DistrictId]', N'IX_OrderInfos_DistrictId', N'INDEX';
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240830030927_Add OrderInfo table2'
)
BEGIN
    ALTER TABLE [OrderInfos] ADD CONSTRAINT [PK_OrderInfos] PRIMARY KEY ([Id]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240830030927_Add OrderInfo table2'
)
BEGIN
    ALTER TABLE [OrderInfos] ADD CONSTRAINT [FK_OrderInfos_districts_DistrictId] FOREIGN KEY ([DistrictId]) REFERENCES [districts] ([code]) ON DELETE CASCADE;
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240830030927_Add OrderInfo table2'
)
BEGIN
    ALTER TABLE [OrderInfos] ADD CONSTRAINT [FK_OrderInfos_provinces_ProvinceId] FOREIGN KEY ([ProvinceId]) REFERENCES [provinces] ([code]) ON DELETE CASCADE;
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240830030927_Add OrderInfo table2'
)
BEGIN
    ALTER TABLE [OrderInfos] ADD CONSTRAINT [FK_OrderInfos_wards_WardId] FOREIGN KEY ([WardId]) REFERENCES [wards] ([code]) ON DELETE CASCADE;
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240830030927_Add OrderInfo table2'
)
BEGIN
    ALTER TABLE [Orders] ADD CONSTRAINT [FK_Orders_OrderInfos_OrderInfoId] FOREIGN KEY ([OrderInfoId]) REFERENCES [OrderInfos] ([Id]) ON DELETE CASCADE;
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240830030927_Add OrderInfo table2'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20240830030927_Add OrderInfo table2', N'8.0.8');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240830051929_Update Order'
)
BEGIN
    ALTER TABLE [Orders] ADD [Address] nvarchar(max) NOT NULL DEFAULT N'';
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240830051929_Update Order'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20240830051929_Update Order', N'8.0.8');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240904054102_Update Book table'
)
BEGIN
    ALTER TABLE [Comments] DROP CONSTRAINT [FK_Comments_Books_BookId];
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240904054102_Update Book table'
)
BEGIN
    DROP INDEX [IX_Comments_BookId] ON [Comments];
    DECLARE @var0 sysname;
    SELECT @var0 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Comments]') AND [c].[name] = N'BookId');
    IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [Comments] DROP CONSTRAINT [' + @var0 + '];');
    EXEC(N'UPDATE [Comments] SET [BookId] = ''00000000-0000-0000-0000-000000000000'' WHERE [BookId] IS NULL');
    ALTER TABLE [Comments] ALTER COLUMN [BookId] uniqueidentifier NOT NULL;
    ALTER TABLE [Comments] ADD DEFAULT '00000000-0000-0000-0000-000000000000' FOR [BookId];
    CREATE INDEX [IX_Comments_BookId] ON [Comments] ([BookId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240904054102_Update Book table'
)
BEGIN
    ALTER TABLE [Books] ADD [ImageUrl] nvarchar(max) NOT NULL DEFAULT N'';
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240904054102_Update Book table'
)
BEGIN
    ALTER TABLE [Comments] ADD CONSTRAINT [FK_Comments_Books_BookId] FOREIGN KEY ([BookId]) REFERENCES [Books] ([Id]) ON DELETE CASCADE;
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240904054102_Update Book table'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20240904054102_Update Book table', N'8.0.8');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240905015451_Update order table'
)
BEGIN
    ALTER TABLE [Orders] ADD [PaymentMethod] nvarchar(max) NOT NULL DEFAULT N'';
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240905015451_Update order table'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20240905015451_Update order table', N'8.0.8');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240906133815_Update string numberphone'
)
BEGIN
    DECLARE @var1 sysname;
    SELECT @var1 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[OrderInfos]') AND [c].[name] = N'Phone');
    IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [OrderInfos] DROP CONSTRAINT [' + @var1 + '];');
    ALTER TABLE [OrderInfos] ALTER COLUMN [Phone] nvarchar(max) NOT NULL;
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240906133815_Update string numberphone'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20240906133815_Update string numberphone', N'8.0.8');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240913084304_CreatePaymentOrder'
)
BEGIN
    CREATE TABLE [PaymentOrders] (
        [PaymentID] bigint NOT NULL IDENTITY,
        [Amount] decimal(18,4) NOT NULL,
        [CreatedDate] datetime2 NOT NULL,
        [OrderId] uniqueidentifier NOT NULL,
        CONSTRAINT [PK_PaymentOrders] PRIMARY KEY ([PaymentID]),
        CONSTRAINT [FK_PaymentOrders_Orders_OrderId] FOREIGN KEY ([OrderId]) REFERENCES [Orders] ([OrderId]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240913084304_CreatePaymentOrder'
)
BEGIN
    CREATE INDEX [IX_PaymentOrders_OrderId] ON [PaymentOrders] ([OrderId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240913084304_CreatePaymentOrder'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20240913084304_CreatePaymentOrder', N'8.0.8');
END;
GO

COMMIT;
GO

