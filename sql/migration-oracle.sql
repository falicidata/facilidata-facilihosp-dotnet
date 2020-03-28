-- DATA

BEGIN
BEGIN 
EXECUTE IMMEDIATE 'CREATE TABLE 
"__EFMigrationsHistory" (
    "MigrationId" NVARCHAR2(150) NOT NULL,
    "ProductVersion" NVARCHAR2(32) NOT NULL,
    CONSTRAINT "PK___EFMigrationsHistory" PRIMARY KEY ("MigrationId")
)';
END;
EXCEPTION
WHEN OTHERS THEN
    IF(SQLCODE != -942)THEN
        RAISE;
    END IF;
END;
/

BEGIN 
EXECUTE IMMEDIATE 'CREATE TABLE 
"Hospitais" (
    "Id" NVARCHAR2(450) NOT NULL,
    "CriadoPor" NVARCHAR2(2000),
    "CriadoEm" TIMESTAMP(7),
    "AtualizadoPor" NVARCHAR2(2000),
    "AtualizadoEm" TIMESTAMP(7),
    "DeletadoPor" NVARCHAR2(2000),
    "DeletadoEm" TIMESTAMP(7),
    "Deletado" NUMBER(1) NOT NULL,
    "Nome" varchar(250) NOT NULL,
    "Endereco" varchar(250),
    "Cep" varchar(250),
    "Bairro" varchar(250),
    "Cidade" varchar(250),
    "Estado" varchar(250),
    "UserId" NVARCHAR2(2000),
    CONSTRAINT "PK_Hospitais" PRIMARY KEY ("Id")
)';
END;
/

BEGIN 
EXECUTE IMMEDIATE 'CREATE TABLE 
"Exames" (
    "Id" NVARCHAR2(450) NOT NULL,
    "CriadoPor" NVARCHAR2(2000),
    "CriadoEm" TIMESTAMP(7),
    "AtualizadoPor" NVARCHAR2(2000),
    "AtualizadoEm" TIMESTAMP(7),
    "DeletadoPor" NVARCHAR2(2000),
    "DeletadoEm" TIMESTAMP(7),
    "Deletado" NUMBER(1) NOT NULL,
    "HospitalId" NVARCHAR2(450) NOT NULL,
    "UsuarioId" varchar(36),
    "Tipo" varchar(251) NOT NULL,
    "Resultado" varchar(2000),
    "Url" varchar(500),
    "ContentType" varchar(50),
    "NomeArquivo" varchar(100),
    "Anexo" RAW(2000),
    CONSTRAINT "PK_Exames" PRIMARY KEY ("Id"),
    CONSTRAINT "FK_Exames_Hospitais_HospitalId" FOREIGN KEY ("HospitalId") REFERENCES "Hospitais" ("Id")
)';
END;
/

CREATE INDEX "IX_Exames_HospitalId" ON "Exames" ("HospitalId")
/

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES (N'20200328191742_inicial', N'2.2.6-servicing-10079')
/



-- IDENTITY
BEGIN
BEGIN 
EXECUTE IMMEDIATE 'CREATE TABLE 
"__EFMigrationsHistory" (
    "MigrationId" NVARCHAR2(150) NOT NULL,
    "ProductVersion" NVARCHAR2(32) NOT NULL,
    CONSTRAINT "PK___EFMigrationsHistory" PRIMARY KEY ("MigrationId")
)';
END;
EXCEPTION
WHEN OTHERS THEN
    IF(SQLCODE != -942)THEN
        RAISE;
    END IF;
END;
/

BEGIN 
EXECUTE IMMEDIATE 'CREATE TABLE 
"AspNetRoles" (
    "Id" NVARCHAR2(450) NOT NULL,
    "Name" NVARCHAR2(256),
    "NormalizedName" NVARCHAR2(256),
    "ConcurrencyStamp" NVARCHAR2(2000),
    CONSTRAINT "PK_AspNetRoles" PRIMARY KEY ("Id")
)';
END;
/

BEGIN 
EXECUTE IMMEDIATE 'CREATE TABLE 
"Contas" (
    "Id" NVARCHAR2(450) NOT NULL,
    "CriadoPor" NVARCHAR2(2000),
    "CriadoEm" TIMESTAMP(7),
    "AtualizadoPor" NVARCHAR2(2000),
    "AtualizadoEm" TIMESTAMP(7),
    "DeletadoPor" NVARCHAR2(2000),
    "DeletadoEm" TIMESTAMP(7),
    "Deletado" NUMBER(1) NOT NULL,
    "Sexo" varchar(20) NOT NULL,
    "Discriminator" NVARCHAR2(2000) NOT NULL,
    "CRM" varchar(20),
    "CPF" varchar(20),
    "ConvenioMedico" varchar(250),
    CONSTRAINT "PK_Contas" PRIMARY KEY ("Id")
)';
END;
/

BEGIN 
EXECUTE IMMEDIATE 'CREATE TABLE 
"AspNetRoleClaims" (
    "Id" NUMBER(10) GENERATED BY DEFAULT ON NULL AS IDENTITY NOT NULL,
    "RoleId" NVARCHAR2(450) NOT NULL,
    "ClaimType" NVARCHAR2(2000),
    "ClaimValue" NVARCHAR2(2000),
    CONSTRAINT "PK_AspNetRoleClaims" PRIMARY KEY ("Id"),
    CONSTRAINT "FK_AspNetRoleClaims_AspNetRoles_RoleId" FOREIGN KEY ("RoleId") REFERENCES "AspNetRoles" ("Id") ON DELETE CASCADE
)';
END;
/

BEGIN 
EXECUTE IMMEDIATE 'CREATE TABLE 
"AspNetUsers" (
    "Id" NVARCHAR2(450) NOT NULL,
    "UserName" NVARCHAR2(256),
    "NormalizedUserName" NVARCHAR2(256),
    "Email" NVARCHAR2(256),
    "NormalizedEmail" NVARCHAR2(256),
    "EmailConfirmed" NUMBER(1) NOT NULL,
    "PasswordHash" NVARCHAR2(2000),
    "SecurityStamp" NVARCHAR2(2000),
    "ConcurrencyStamp" NVARCHAR2(2000),
    "PhoneNumber" NVARCHAR2(2000),
    "PhoneNumberConfirmed" NUMBER(1) NOT NULL,
    "TwoFactorEnabled" NUMBER(1) NOT NULL,
    "LockoutEnd" TIMESTAMP(3) WITH TIME ZONE,
    "LockoutEnabled" NUMBER(1) NOT NULL,
    "AccessFailedCount" NUMBER(10) NOT NULL,
    "Discriminator" NVARCHAR2(2000) NOT NULL,
    "ContaId" NVARCHAR2(450),
    CONSTRAINT "PK_AspNetUsers" PRIMARY KEY ("Id"),
    CONSTRAINT "FK_AspNetUsers_Contas_ContaId" FOREIGN KEY ("ContaId") REFERENCES "Contas" ("Id")
)';
END;
/

BEGIN 
EXECUTE IMMEDIATE 'CREATE TABLE 
"AspNetUserClaims" (
    "Id" NUMBER(10) GENERATED BY DEFAULT ON NULL AS IDENTITY NOT NULL,
    "UserId" NVARCHAR2(450) NOT NULL,
    "ClaimType" NVARCHAR2(2000),
    "ClaimValue" NVARCHAR2(2000),
    CONSTRAINT "PK_AspNetUserClaims" PRIMARY KEY ("Id"),
    CONSTRAINT "FK_AspNetUserClaims_AspNetUsers_UserId" FOREIGN KEY ("UserId") REFERENCES "AspNetUsers" ("Id") ON DELETE CASCADE
)';
END;
/

BEGIN 
EXECUTE IMMEDIATE 'CREATE TABLE 
"AspNetUserLogins" (
    "LoginProvider" NVARCHAR2(450) NOT NULL,
    "ProviderKey" NVARCHAR2(450) NOT NULL,
    "ProviderDisplayName" NVARCHAR2(2000),
    "UserId" NVARCHAR2(450) NOT NULL,
    CONSTRAINT "PK_AspNetUserLogins" PRIMARY KEY ("LoginProvider", "ProviderKey"),
    CONSTRAINT "FK_AspNetUserLogins_AspNetUsers_UserId" FOREIGN KEY ("UserId") REFERENCES "AspNetUsers" ("Id") ON DELETE CASCADE
)';
END;
/

BEGIN 
EXECUTE IMMEDIATE 'CREATE TABLE 
"AspNetUserRoles" (
    "UserId" NVARCHAR2(450) NOT NULL,
    "RoleId" NVARCHAR2(450) NOT NULL,
    CONSTRAINT "PK_AspNetUserRoles" PRIMARY KEY ("UserId", "RoleId"),
    CONSTRAINT "FK_AspNetUserRoles_AspNetRoles_RoleId" FOREIGN KEY ("RoleId") REFERENCES "AspNetRoles" ("Id") ON DELETE CASCADE,
    CONSTRAINT "FK_AspNetUserRoles_AspNetUsers_UserId" FOREIGN KEY ("UserId") REFERENCES "AspNetUsers" ("Id") ON DELETE CASCADE
)';
END;
/

BEGIN 
EXECUTE IMMEDIATE 'CREATE TABLE 
"AspNetUserTokens" (
    "UserId" NVARCHAR2(450) NOT NULL,
    "LoginProvider" NVARCHAR2(450) NOT NULL,
    "Name" NVARCHAR2(450) NOT NULL,
    "Value" NVARCHAR2(2000),
    CONSTRAINT "PK_AspNetUserTokens" PRIMARY KEY ("UserId", "LoginProvider", "Name"),
    CONSTRAINT "FK_AspNetUserTokens_AspNetUsers_UserId" FOREIGN KEY ("UserId") REFERENCES "AspNetUsers" ("Id") ON DELETE CASCADE
)';
END;
/

CREATE INDEX "IX_AspNetRoleClaims_RoleId" ON "AspNetRoleClaims" ("RoleId")
/

CREATE UNIQUE INDEX "RoleNameIndex" ON "AspNetRoles" ("NormalizedName")
/

CREATE INDEX "IX_AspNetUserClaims_UserId" ON "AspNetUserClaims" ("UserId")
/

CREATE INDEX "IX_AspNetUserLogins_UserId" ON "AspNetUserLogins" ("UserId")
/

CREATE INDEX "IX_AspNetUserRoles_RoleId" ON "AspNetUserRoles" ("RoleId")
/

CREATE UNIQUE INDEX "IX_AspNetUsers_ContaId" ON "AspNetUsers" ("ContaId")
/

CREATE INDEX "EmailIndex" ON "AspNetUsers" ("NormalizedEmail")
/

CREATE UNIQUE INDEX "UserNameIndex" ON "AspNetUsers" ("NormalizedUserName")
/

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES (N'20200328191654_inicial', N'2.2.6-servicing-10079')
/

