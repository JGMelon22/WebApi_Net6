-- Cria o banco de dados
CREATE DATABASE MpNet6Api;

-- Muda para a base recem criada
USE MpNet6Api;

-- Criação das tabelas e colunas
BEGIN TRANSACTION
CREATE TABLE [Pessoa]
(
    [IdPessoa] [INT] IDENTITY(1, 1),
    [Nome] [NVARCHAR](100) NOT NULL,
    [Documento] [NVARCHAR](20) NOT NULL,
    [Celular] [VARCHAR](20) NOT NULL
)
	GO

ALTER TABLE [Pessoa]
		ADD CONSTRAINT [PK_IdPessoa]
			PRIMARY KEY ([IdPessoa])
	GO
ROLLBACK -- COMMIT

BEGIN TRANSACTION
CREATE TABLE [Produto]
(
    [IdProduto] [INT] IDENTITY(1, 1),
    [Nome] [NVARCHAR](100) NOT NULL,
    [CodigoErp] [VARCHAR](10) NOT NULL,
    [Preco] [DECIMAL](10,2) NOT NULL
)
	GO

ALTER TABLE [Produto]
		ADD CONSTRAINT [PK_IdProduto]
			PRIMARY KEY ([IdProduto])
	GO
ROLLBACK -- COMMIT

BEGIN TRANSACTION
CREATE TABLE [Compra]
(
    [IdCompra] [INT] IDENTITY(1, 1),
    [IdProduto] [INT] NOT NULL,
    [IdPessoa] [INT] NOT NULL,
    [DataCompra] [DATE] NOT NULL,

)
	GO

ALTER TABLE [Compra]
		ADD CONSTRAINT [PK_IdCompra]
			PRIMARY KEY ([IdCompra])
	GO

ALTER TABLE [Compra]
		ADD CONSTRAINT [FK_IdProduto]
			FOREIGN KEY ([IdProduto])
            REFERENCES [Produto]
	GO

ALTER TABLE [Compra]
		ADD CONSTRAINT [FK_IdPessoa]
			FOREIGN KEY ([IdPessoa])
            REFERENCES [Pessoa]
	GO
ROLLBACK -- COMMIT

-- Cria a trigger para resetar o contador do identity
CREATE Trigger [dbo].[ResetarContadorPessoa]
ON [dbo].[Pessoa]
INSTEAD OF DELETE
AS 
BEGIN
	DBCC CHECKIDENT ('[Pessoa]', RESEED, 0);
END

CREATE Trigger [dbo].[ResetarContadorProduto]
ON [dbo].[Produto]
INSTEAD OF DELETE
AS 
BEGIN
	DBCC CHECKIDENT ('[Produto]', RESEED, 0);
END

CREATE Trigger [dbo].[ResetarContadorCompra]
ON [dbo].[Compra]
INSTEAD OF DELETE
AS 
BEGIN
	DBCC CHECKIDENT ('[Compra]', RESEED, 0);
END