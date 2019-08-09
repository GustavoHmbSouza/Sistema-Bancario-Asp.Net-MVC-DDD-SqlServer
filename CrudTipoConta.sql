
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[SelTipoConta]') AND objectproperty(id, N'IsPROCEDURE')=1)
	DROP PROCEDURE [dbo].[SelTipoConta]
GO

CREATE PROCEDURE [dbo].[SelTipoConta]

	AS

	/*
	Documentação
	Arquivo Fonte.....: CrudTipoConta.sql
	Objetivo..........: Retornar a tabela TipoConta
	Autor.............: SMN - Gustavo Matos
 	Data..............: 08/08/2019
	Ex................: EXEC [dbo].[SelTipoConta]

	*/

	BEGIN
	
		SELECT Cod_Conta, Nom_Nome 
		FROM [dbo].[TipoConta] WITH(NOLOCK);

	END
GO
				