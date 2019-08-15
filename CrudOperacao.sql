IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[InsSaque]') AND objectproperty(id, N'IsPROCEDURE')=1)
	DROP PROCEDURE [dbo].[InsSaque]
GO

CREATE PROCEDURE [dbo].[InsSaque] @Num_idConta1 INT, @Num_Valor DECIMAL, @Num_TipoOperacao TINYINT, @Num_Operacao INT, @Date_DataOperacao DATETIME
	AS

	/*
	Documentação
	Arquivo Fonte.....: CrudOperacao.sql
	Objetivo..........: Sacar um valor
	Autor.............: SMN - Gustavo Matos
 	Data..............: 07/08/2019
	Ex................: EXEC [dbo].[InsSaque]
	Retornos:.........: 0 - Sucesso
						1 - Saldo Insuficiente 
	*/

	BEGIN
	
		DECLARE @SaldoAux DECIMAL

		SELECT @SaldoAux = (Num_Saldo - @Num_Valor) 
			FROM [dbo].[Conta] WITH(NOLOCK)
			WHERE Num_SeqlConta = @Num_idConta1; 

		if (@SaldoAux < 0) 
			return 1

		INSERT INTO [dbo].[Operacoes] (Num_Operacao, Num_Tipooperacao, Num_idConta1, Num_idConta2, Num_Valor, Date_DataOperacao)
				VALUES (@Num_Operacao,  @Num_TipoOperacao, @Num_idconta1, null, @Num_Valor, @Date_DataOperacao);

		return 0
	END
GO


IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[InsDeposito]') AND objectproperty(id, N'IsPROCEDURE')=1)
	DROP PROCEDURE [dbo].[InsDeposito]
GO

CREATE PROCEDURE [dbo].[InsDeposito] @Num_idConta1 INT, @Num_Valor DECIMAL, @Num_TipoOperacao TINYINT, @Num_Operacao INT, @Date_DataOperacao DATETIME

	AS

	/*
	Documentação
	Arquivo Fonte.....: CrudOperacao.sql
	Objetivo..........: Depositar um valor
	Autor.............: SMN - Gustavo Matos
 	Data..............: 07/08/2019
	Ex................: EXEC [dbo].[InsDeposito]
	*/

	BEGIN
	
		INSERT INTO [dbo].[Operacoes] (Num_Operacao, Num_Tipooperacao, Num_idConta1, Num_idConta2, Num_Valor, Date_DataOperacao)
		VALUES (@Num_Operacao,  @Num_TipoOperacao, @Num_idconta1, null, @Num_Valor, @Date_DataOperacao);

	END
GO


IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[InsTransferencia]') AND objectproperty(id, N'IsPROCEDURE')=1)
	DROP PROCEDURE [dbo].[InsTransferencia]
GO

CREATE PROCEDURE [dbo].[InsTransferencia] @Num_idConta1 INT, @Num_Valor DECIMAL, @Num_TipoOperacao TINYINT, @Num_Operacao INT, @Date_DataOperacao DATETIME,  @Num_idConta2 INT 

	AS

	/*
	Documentação
	Arquivo Fonte.....: CrudOperacao.sql
	Objetivo..........: Transferir um valor de uma conta para outra
	Autor.............: SMN - Gustavo Matos
 	Data..............: 07/08/2019
	Ex................: EXEC [dbo].[InsTransferencia]
	Retornos:.........: 0 - Sucesso
						1 - Não existe a conta da transferência
	*/

	BEGIN
		
		DECLARE @qtdRows INT

		SELECT @qtdRows = COUNT(*) FROM [dbo].[Conta] WHERE Num_SeqlConta = @Num_idConta2

		if (@qtdRows = 0) 
			return 1

		INSERT INTO [dbo].[Operacoes] (Num_Operacao, Num_Tipooperacao, Num_idConta1, Num_idConta2, Num_Valor, Date_DataOperacao)
			VALUES (@Num_Operacao,  @Num_TipoOperacao, @Num_idconta1, @Num_idconta2, @Num_Valor, @Date_DataOperacao);
	
		return 0

	END
GO
				
				
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[SelExtrato]') AND objectproperty(id, N'IsPROCEDURE')=1)
	DROP PROCEDURE [dbo].[SelExtrato]
GO

CREATE PROCEDURE [dbo].[SelExtrato] @Num_idConta1  INT

	AS

	/*
	Documentação
	Arquivo Fonte.....: CrudOperacao.sql
	Objetivo..........: Consultar extrato
	Autor.............: SMN - Gustavo Matos
 	Data..............: 07/08/2019
	Ex................: EXEC [dbo].[SelExtrato]
	*/

	BEGIN
	
		SELECT Num_Operacao,
		Date_DataOperacao,
		Num_Valor,
		(	
			SELECT Nom_Nome FROM [dbo].[TipoOperacao] WITH(NOLOCK) WHERE Cod_TipoOperacao = Num_TipoOperacao
		) 
		AS Nom_TipoOperacao, 
		COALESCE
		(
			(
				SELECT Nom_Nome
					FROM [dbo].[Conta] WITH(NOLOCK) 
					WHERE Num_SeqlConta = Num_idConta1
			)
			, '--'
		) 
		AS 'Nom_TransferenciaDe',
		COALESCE
		(
			(
				SELECT Nom_Nome
					FROM [dbo].[Conta] WITH(NOLOCK) 
					WHERE Num_SeqlConta = Num_idConta2
			)
			, '--'
		) 
		AS 'Nom_TransferenciaPara'
			FROM [dbo].[Operacoes] WITH(NOLOCK)
			WHERE Num_idConta1 = @Num_idConta1 
				OR Num_idConta2 = @Num_idConta1 

	END
GO


IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[InsExtorno]') AND objectproperty(id, N'IsPROCEDURE')=1)
	DROP PROCEDURE [dbo].[InsExtorno]
GO

CREATE PROCEDURE [dbo].[InsExtorno] @Num_SeqlOperacao INT, @Num_Operacao INT,  @Num_TipoOperacao TINYINT, @Num_idOperacoes INT, @Date_DataOperacao DATETIME

	AS

	/*
	Documentação
	Arquivo Fonte.....: CrudOperacao.sql
	Objetivo..........: Extornar operação
	Autor.............: SMN - Gustavo Matos
 	Data..............: 13/08//2019
	Ex................: EXEC [dbo].[InsExtorno]

	*/

	BEGIN
	
		INSERT INTO [dbo].[Operacoes] (Num_Operacao, Num_TipoOperacao, Num_idOperacoes, Date_DataOperacao)
			VALUES (@Num_Operacao,  @Num_TipoOperacao, @Num_idOperacoes, @Date_DataOperacao);

		return 0

	END
GO


IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[VerificaEstornoExistente]') AND objectproperty(id, N'IsPROCEDURE')=1)
	DROP PROCEDURE [dbo].[VerificaEstornoExistente]
GO

CREATE PROCEDURE [dbo].[VerificaEstornoExistente] @Num_SeqlOperacao INT

	AS

	/*
	Documentação
	Arquivo Fonte.....: CrudOperacao.sql
	Objetivo..........: Verifica se já existe um extorno para aquela conta
	Autor.............: SMN - Gustavo Souza
 	Data..............: 13/08/2019
	Ex................: EXEC [dbo].[VerificaEstornoExistente]
	Retornos:.........: 0 - Sucesso
						1 - Já existe um extorno para aquela operação
	*/

	BEGIN
	
		DECLARE @qtdRows INT

		SELECT @qtdRows = COUNT(*) FROM [dbo].[Operacoes] WHERE Num_idOperacoes =  @Num_SeqlOperacao AND Num_TipoOperacao = 4

		if (@qtdRows = 0)
			return 1

		return 0

	END
GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[VerificaSaldoNaConta]') AND objectproperty(id, N'IsPROCEDURE')=1)
	DROP PROCEDURE [dbo].[VerificaSaldoNaConta]
GO

CREATE PROCEDURE [dbo].[VerificaSaldoNaConta] @Num_idConta1 INT, @Num_Valor DECIMAL

	AS

	/*
	Documentação
	Arquivo Fonte.....: CrudOperacao.sql
	Objetivo..........: Verifica se existe saldo na conta
	Autor.............: SMN - Gustavo Souza
 	Data..............: 13/08/2019
	Ex................: EXEC [dbo].[VerificaSaldoNaConta]
	Retornos:.........: 0 - Sucesso
						1 - Caso não exista saldo na conta para efetuar 'x' operação
	*/

	BEGIN
	
			
		DECLARE @SaldoAux DECIMAL

		SELECT @SaldoAux = (Num_Saldo - @Num_Valor) 
			FROM [dbo].[Conta] WITH(NOLOCK)
			WHERE Num_SeqlConta = @Num_idConta1; 

		if (@SaldoAux < 0) 
			return 1

		return 0

	END
GO


	SELECT Num_Operacao,
		Date_DataOperacao,
		Num_Valor,
		(	
			SELECT Nom_Nome FROM [dbo].[TipoOperacao] WITH(NOLOCK) WHERE Cod_TipoOperacao = Num_TipoOperacao
		) 
		AS Nom_TipoOperacao, 
		COALESCE
		(
			(
				SELECT Nom_Nome
					FROM [dbo].[Conta] WITH(NOLOCK) 
					WHERE Num_SeqlConta = Num_idConta1
			)
			, '--'
		) 
		AS 'Nom_TransferenciaDe',
		COALESCE
		(
			(
				SELECT Nom_Nome
					FROM [dbo].[Conta] WITH(NOLOCK) 
					WHERE Num_SeqlConta = Num_idConta2
			)
			, '--'
		) 
		AS 'Nom_TransferenciaPara'
			FROM [dbo].[Operacoes] WITH(NOLOCK)
			WHERE Num_idConta1 = 2
				OR Num_idConta2 = 2