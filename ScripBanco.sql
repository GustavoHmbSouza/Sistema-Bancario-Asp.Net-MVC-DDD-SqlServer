CREATE DATABASE ProjTesteBd;

USE ProjTesteBd;

CREATE TABLE TipoConta
(
	Cod_Conta			TINYINT IDENTITY,
	Nom_Nome			VARCHAR(80) NOT NULL,
	CONSTRAINT PK_TipoConta PRIMARY KEY (Cod_Conta)
);

CREATE TABLE TipoOperacao
(
	Cod_TipoOperacao	TINYINT IDENTITY,
	Nom_Nome			VARCHAR(80) NOT NULL,
	Num_Sinal			SMALLINT NOT NULL,

	CONSTRAINT PK_TipoOperacao PRIMARY KEY (Cod_TipoOperacao)
);

CREATE TABLE Conta
(
	Num_SeqlConta		INT IDENTITY,
	Num_Conta			TINYINT NOT NULL,
	Nom_Nome			VARCHAR(80) NOT NULL,
	Num_Saldo			DECIMAL(10,2) NOT NULL,		
	Date_DataCriacao	DATETIME NOT NULL,
	Num_TipoConta		TINYINT NOT NULL,

	CONSTRAINT PK_Conta PRIMARY KEY (Num_SeqlConta),
	CONSTRAINT Fk_TipoConta_Conta FOREIGN KEY (Num_TipoConta) REFERENCES TipoConta (Cod_Conta)
);

CREATE TABLE Operacoes
(
	Num_SeqlOperacao	INT IDENTITY,
	Num_Operacao		INT NOT NULL,
	Num_TipoOperacao	TINYINT NOT NULL,
	Num_idConta1		INT,
	Num_idConta2		INT,
	Num_idOperacoes		INT,
	Num_Valor			DECIMAL(10,2),
	Date_DataOperacao	DATETIME NOT NULL,

	CONSTRAINT PK_Operacoes PRIMARY KEY (Num_SeqlOperacao),
	CONSTRAINT Fk_TipoOperacao_Operacoes FOREIGN KEY (Num_TipoOperacao) REFERENCES TipoOperacao (Cod_TipoOperacao),
	CONSTRAINT Fk_Conta_Operacoes1 FOREIGN KEY (Num_idConta1) REFERENCES Conta (Num_SeqlConta),
	CONSTRAINT Fk_Conta_Operacoes2 FOREIGN KEY (Num_idConta2) REFERENCES Conta (Num_SeqlConta),
	CONSTRAINT Fk_Operacoes_Operacoes FOREIGN KEY (Num_idOperacoes) REFERENCES Operacoes (Num_SeqlOperacao)
);