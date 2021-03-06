-- Cria o banco
CREATE DATABASE DOTNET6_API;

-- Cria as tabelas
CREATE TABLE DOTNET6_API;.dbo.Pessoa (
	IdPessoa INT IDENTITY(1,1) NOT NULL,
	Nome NVARCHAR(100) NOT NULL,
	Documento NVARCHAR(20) NOT NULL,
	Celular VARCHAR(20) NOT NULL,
	CONSTRAINT PK_IdPessoa PRIMARY KEY (IdPessoa)
);

CREATE TABLE Produto (
	IdProduto INT IDENTITY(1,1) NOT NULL,
	Nome nVARCHAR(100)  NOT NULL,
	CodigoErp VARCHAR(10)  NOT NULL,
	Preco DECIMAL(10,2) NOT NULL,
	CONSTRAINT PK_IdProduto PRIMARY KEY (IdProduto)
);

CREATE TABLE Usuario (
	IdUsuario INT IDENTITY(1,1) NOT NULL,
	Email VARCHAR(50)  NOT NULL,
	Senha VARCHAR(100)  NULL
);

CREATE TABLE Compra (
	IdCompra INT IDENTITY(1,1) NOT NULL,
	IdProduto INT NOT NULL,
	IdPessoa INT NOT NULL,
	DataCompra DATE NOT NULL,
	CONSTRAINT PK_IdCompra PRIMARY KEY (IdCompra),
	CONSTRAINT FK_IdPessoa FOREIGN KEY (IdPessoa) REFERENCES Pessoa(IdPessoa),
	CONSTRAINT FK_IdProduto FOREIGN KEY (IdProduto) REFERENCES Produto(IdProduto)
);

