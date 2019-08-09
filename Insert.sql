USE ProjTesteBd;

BEGIN TRANSACTION

	INSERT INTO  [dbo].[TipoOperacao] (Nom_Nome, Num_Sinal) 
		VALUES ('Saque', -1), ('Deposito', 1), ('Transferencia', -1), ('Estorno', -1);  

	IF @@ERROR <> 0
		ROLLBACK
	
	INSERT INTO [dbo].[TipoConta] (Nom_Nome)
		VALUES ('Corrente'), ('Poupança'), ('Corrente Especial'), ('Poupança Especial');
	
	IF @@ERROR <> 0
		ROLLBACK
	
COMMIT