
-- DATA
IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;

GO

CREATE TABLE [Hospitais] (
    [Id] nvarchar(450) NOT NULL,
    [CriadoPor] nvarchar(max) NULL,
    [CriadoEm] datetime2 NULL,
    [AtualizadoPor] nvarchar(max) NULL,
    [AtualizadoEm] datetime2 NULL,
    [DeletadoPor] nvarchar(max) NULL,
    [DeletadoEm] datetime2 NULL,
    [Deletado] bit NOT NULL,
    [Nome] varchar(250) NOT NULL,
    [Endereco] varchar(250) NULL,
    [Cep] varchar(250) NULL,
    [Bairro] varchar(250) NULL,
    [Cidade] varchar(250) NULL,
    [Estado] varchar(250) NULL,
    [UserId] nvarchar(max) NULL,
    CONSTRAINT [PK_Hospitais] PRIMARY KEY ([Id])
);

GO

CREATE TABLE [Exames] (
    [Id] nvarchar(450) NOT NULL,
    [CriadoPor] nvarchar(max) NULL,
    [CriadoEm] datetime2 NULL,
    [AtualizadoPor] nvarchar(max) NULL,
    [AtualizadoEm] datetime2 NULL,
    [DeletadoPor] nvarchar(max) NULL,
    [DeletadoEm] datetime2 NULL,
    [Deletado] bit NOT NULL,
    [HospitalId] nvarchar(450) NOT NULL,
    [UsuarioId] varchar(36) NULL,
    [Tipo] varchar(251) NOT NULL,
    [Resultado] varchar(2000) NULL,
    [Url] varchar(500) NULL,
    [ContentType] varchar(50) NULL,
    [NomeArquivo] varchar(100) NULL,
    [Anexo] varbinary(max) NULL,
    CONSTRAINT [PK_Exames] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Exames_Hospitais_HospitalId] FOREIGN KEY ([HospitalId]) REFERENCES [Hospitais] ([Id]) ON DELETE NO ACTION
);

GO

CREATE INDEX [IX_Exames_HospitalId] ON [Exames] ([HospitalId]);

GO

-- Identity

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20200328190929_inicial', N'2.2.6-servicing-10079');

GO



IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;

GO

CREATE TABLE [AspNetRoles] (
    [Id] nvarchar(450) NOT NULL,
    [Name] nvarchar(256) NULL,
    [NormalizedName] nvarchar(256) NULL,
    [ConcurrencyStamp] nvarchar(max) NULL,
    CONSTRAINT [PK_AspNetRoles] PRIMARY KEY ([Id])
);

GO

CREATE TABLE [Contas] (
    [Id] nvarchar(450) NOT NULL,
    [CriadoPor] nvarchar(max) NULL,
    [CriadoEm] datetime2 NULL,
    [AtualizadoPor] nvarchar(max) NULL,
    [AtualizadoEm] datetime2 NULL,
    [DeletadoPor] nvarchar(max) NULL,
    [DeletadoEm] datetime2 NULL,
    [Deletado] bit NOT NULL,
    [Sexo] varchar(20) NOT NULL,
    [Discriminator] nvarchar(max) NOT NULL,
    [CRM] varchar(20) NULL,
    [CPF] varchar(20) NULL,
    [ConvenioMedico] varchar(250) NULL,
    CONSTRAINT [PK_Contas] PRIMARY KEY ([Id])
);

GO

CREATE TABLE [AspNetRoleClaims] (
    [Id] int NOT NULL IDENTITY,
    [RoleId] nvarchar(450) NOT NULL,
    [ClaimType] nvarchar(max) NULL,
    [ClaimValue] nvarchar(max) NULL,
    CONSTRAINT [PK_AspNetRoleClaims] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [AspNetRoles] ([Id]) ON DELETE CASCADE
);

GO

CREATE TABLE [AspNetUsers] (
    [Id] nvarchar(450) NOT NULL,
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
    [Discriminator] nvarchar(max) NOT NULL,
    [ContaId] nvarchar(450) NULL,
    CONSTRAINT [PK_AspNetUsers] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_AspNetUsers_Contas_ContaId] FOREIGN KEY ([ContaId]) REFERENCES [Contas] ([Id]) ON DELETE NO ACTION
);

GO

CREATE TABLE [AspNetUserClaims] (
    [Id] int NOT NULL IDENTITY,
    [UserId] nvarchar(450) NOT NULL,
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
    [UserId] nvarchar(450) NOT NULL,
    CONSTRAINT [PK_AspNetUserLogins] PRIMARY KEY ([LoginProvider], [ProviderKey]),
    CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
);

GO

CREATE TABLE [AspNetUserRoles] (
    [UserId] nvarchar(450) NOT NULL,
    [RoleId] nvarchar(450) NOT NULL,
    CONSTRAINT [PK_AspNetUserRoles] PRIMARY KEY ([UserId], [RoleId]),
    CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [AspNetRoles] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
);

GO

CREATE TABLE [AspNetUserTokens] (
    [UserId] nvarchar(450) NOT NULL,
    [LoginProvider] nvarchar(450) NOT NULL,
    [Name] nvarchar(450) NOT NULL,
    [Value] nvarchar(max) NULL,
    CONSTRAINT [PK_AspNetUserTokens] PRIMARY KEY ([UserId], [LoginProvider], [Name]),
    CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
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

CREATE UNIQUE INDEX [IX_AspNetUsers_ContaId] ON [AspNetUsers] ([ContaId]) WHERE [ContaId] IS NOT NULL;

GO

CREATE INDEX [EmailIndex] ON [AspNetUsers] ([NormalizedEmail]);

GO

CREATE UNIQUE INDEX [UserNameIndex] ON [AspNetUsers] ([NormalizedUserName]) WHERE [NormalizedUserName] IS NOT NULL;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20200328191306_inicial', N'2.2.6-servicing-10079');

GO

