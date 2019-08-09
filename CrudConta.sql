IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[AltSaldo]') AND objectproperty(id, N'IsPROCEDURE')=1)
	DROP PROCEDURE [dbo].[AltSaldo]
GO

CREATE PROCEDURE [dbo].[AltSaldo] @Num_TipoOperacao TINYINT, @Num_Valor DECIMAL, @Num_idConta1 INT, @Num_idConta2 INT

	AS

	/*
	Documentação
	Arquivo Fonte.....: CrudOperacao.sql
	Objetivo..........: Atualizar o saldo de uma conta
	Autor.............: SMN - Gustavo Matos
 	Data..............: 01/08/2019
	Ex................: EXEC [dbo].[AltSaldo]
	*/

	/* 
	Cod 
	1 - Saque
	2 - Deposito
	3 - Transferencia
	4 - Estorno
	*/

	BEGIN

		if (@Num_TipoOperacao = 3)
			BEGIN
				/*Adiciona em uma conta e subtrai o mesmo valor em outra*/
				UPDATE [dbo].[Conta] 
					SET Num_Saldo = (Num_Saldo - @Num_Valor) 
					WHERE Num_SeqlConta = @Num_idConta1; 

				UPDATE [dbo].[Conta]  
					SET Num_Saldo = (Num_Saldo + @Num_Valor) 
					WHERE Num_SeqlConta = @Num_idConta2; 
			END
		ELSE if (@Num_TipoOperacao = 1 OR @Num_TipoOperacao = 2 OR @Num_TipoOperacao = 4)
			BEGIN
				DECLARE @Sinal SMALLINT;

				/*Busca o sinal da operacao*/
				SELECT @Sinal = Num_Sinal 
					FROM [dbo].[TipoOperacao]  WITH(NOLOCK) 
					WHERE Cod_TipoOperacao = @Num_TipoOperacao;

				/*Atualiza o saldo da conta*/
				UPDATE [dbo].[Conta] 
					SET Num_Saldo = (Num_Saldo + (@Num_Valor * @Sinal)) 
					WHERE Num_SeqlConta = @Num_idConta1; 	
			END
	END
	
GO


IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[SelConta]') AND objectproperty(id, N'IsPROCEDURE')=1)
	DROP PROCEDURE [dbo].[SelConta]
GO

CREATE PROCEDURE [dbo].[SelConta]

	AS

	/*
	Documentação
	Arquivo Fonte.....: CrudConta.sql
	Objetivo..........: Retornar todos os dados de conta
	Autor.............: SMN - Gustavo Henrique
 	Data..............: 02/08/2019
	Ex................: EXEC [dbo].[SelConta]

	*/

	BEGIN
	
		SELECT Num_SeqlConta, Num_Conta, Nom_Nome, Num_Saldo, Date_DataCriacao, Num_TipoConta
			FROM [dbo].[Conta]  WITH(NOLOCK);	

	END
GO
				

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[SelContaId]') AND objectproperty(id, N'IsPROCEDURE')=1)
	DROP PROCEDURE [dbo].[SelContaId]
GO

CREATE PROCEDURE [dbo].[SelContaId] @Num_SeqlConta INT

AS

	/*
	Documentação
	Arquivo Fonte.....: CrudConta.sql
	Objetivo..........: Retorna uma conta de acordo com seu id
	Autor.............: SMN - Gustavo Matos
 	Data..............: 01/08/2018
	Ex................: EXEC [dbo].[SelContaId]

	*/
	
	BEGIN

	 SELECT Num_SeqlConta, Num_Conta, Nom_Nome, Num_Saldo, Date_DataCriacao, Num_TipoConta
		FROM [dbo].[Conta] WITH(NOLOCK)
		WHERE Num_SeqlConta = @Num_SeqlConta
	
	END
GO



IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[DelConta]') AND objectproperty(id, N'IsPROCEDURE')=1)
	DROP PROCEDURE [dbo].[DelConta]
GO

CREATE PROCEDURE [dbo].[DelConta] @Num_SeqlConta INT

	AS

	/*
	Documentação
	Arquivo Fonte.....: CrudConta.sql
	Objetivo..........: Deletar uma conta
	Autor.............: SMN - Gustavo Matos
 	Data..............: 01/08/2019
	Ex................: EXEC [dbo].[DelConta]

	*/

	BEGIN
	
		DELETE FROM [dbo].[Conta] 
			WHERE Num_SeqlConta = @Num_SeqlConta;

	END
GO

					
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[InsConta]') AND objectproperty(id, N'IsPROCEDURE')=1)
	DROP PROCEDURE [dbo].[InsConta]
GO

CREATE PROCEDURE [dbo].[InsConta]  @Num_Conta INT, @Nom_Nome VARCHAR(80), @Num_Saldo DECIMAL, @Date_DataCriacao DATETIME, @Num_TipoConta TINYINT

	AS

	/*
	Documentação
	Arquivo Fonte.....: CrudConta.sql
	Objetivo..........: Inserir uma conta nova
	Autor.............: SMN - Gustavo Matos
 	Data..............: 02/08/2019
	Ex................: EXEC [dbo].[InsConta]

	*/

	BEGIN

		INSERT INTO [dbo].[Conta]  (Num_Conta, Nom_Nome, Num_Saldo, Date_DataCriacao, Num_TipoConta) 
			VALUES(@Num_Conta, @Nom_Nome, @Num_Saldo, @Date_DataCriacao, @Num_TipoConta);

	END

GO	


IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[UpdConta]') AND objectproperty(id, N'IsPROCEDURE')=1)
	DROP PROCEDURE [dbo].[UpdConta]
GO

CREATE PROCEDURE [dbo].[UpdConta] @Num_SeqlConta INT, @Num_Conta INT, @Nom_Nome VARCHAR(80), @Num_Saldo DECIMAL, @Date_DataCriacao DATETIME, @Num_TipoConta TINYINT

	AS

	/*
	Documentação
	Arquivo Fonte.....: CrudConta.sql
	Objetivo..........: Alterar os dados de uma conta
	Autor.............: SMN - Gustavo Matos
 	Data..............: 02/08/2019
	Ex................: EXEC [dbo].[UpdConta]

	*/

	BEGIN

		UPDATE [dbo].[Conta]  
			SET Num_Conta = @Num_Conta, Nom_Nome = @Nom_Nome, Num_Saldo = @Num_Saldo, Date_DataCriacao = @Date_DataCriacao, Num_TipoConta = @Num_TipoConta
			WHERE Num_SeqlConta = @Num_SeqlConta;

	END

GO
	