using ProjTeste.Domain.ConexaoBancoDados;
using ProjTeste.Domain.Entities;
using ProjTeste.Domain.Notification;
using ProjTeste.Domain.Operacoes;
using System;
using System.Collections.Generic;

namespace ProjTeste.Repository.Repositories
{
    public class OperacaoRepository : IOperacaoRepository
    {
        public readonly IConexaoBancoDados _conexaoBancoDados;
        private readonly INotification _notification;

        public OperacaoRepository(IConexaoBancoDados conexaoBancoDados, INotification notification)
        {
            _conexaoBancoDados = conexaoBancoDados;
            _notification = notification;
        }

        public IEnumerable<OperacaoDTO> GetExtrato(int id)
        {
            _conexaoBancoDados.ExecutarProcedure("SelExtrato");
            _conexaoBancoDados.AddParametro("@Num_idConta1", id);

            var contas = new List<OperacaoDTO>();
            using (var leitor = _conexaoBancoDados.ExecuteReader())
            {
                while (leitor.Read())
                {
                    contas.Add(new OperacaoDTO
                    {
                        Num_Operacao = leitor.GetInt32(leitor.GetOrdinal("Num_Operacao")),
                        Date_DataOperacao = leitor.GetDateTime(leitor.GetOrdinal("Date_DataOperacao")),
                        Num_Valor = leitor.GetDecimal(leitor.GetOrdinal("Num_Valor")),
                        Nom_TipoOperacao = leitor.GetString(leitor.GetOrdinal("Nom_TipoOperacao")),
                        Nom_TransferenciaPara = leitor.GetString(leitor.GetOrdinal("Nom_TransferenciaPara")),
                        Nom_TransferenciaDe = leitor.GetString(leitor.GetOrdinal("Nom_TransferenciaDe"))
                    });
                }
            }
            return contas;
        }

        public void Deposito(OperacaoDTO operacaoDTO)
        {
            _conexaoBancoDados.ExecutarProcedure("InsDeposito");
            _conexaoBancoDados.AddParametro("@Num_idConta1", operacaoDTO.Num_idConta1);
            _conexaoBancoDados.AddParametro("@Num_Valor", operacaoDTO.Num_Valor);
            _conexaoBancoDados.AddParametro("@Num_TipoOperacao", operacaoDTO.Num_TipoOperacao);
            _conexaoBancoDados.AddParametro("@Num_Operacao", operacaoDTO.Num_Operacao);
            _conexaoBancoDados.AddParametro("@Date_DataOperacao", operacaoDTO.Date_DataOperacao);

            _conexaoBancoDados.ExecutarSemRetorno();
        }

        public void Estorno(OperacaoDTO operacaoDTO)
        {
            throw new NotImplementedException();
        }

        public void Saque(OperacaoDTO operacaoDTO)
        {
            _conexaoBancoDados.ExecutarProcedure("InsSaque");
            _conexaoBancoDados.AddParametro("@Num_idConta1", operacaoDTO.Num_idConta1);
            _conexaoBancoDados.AddParametro("@Num_Valor", operacaoDTO.Num_Valor);
            _conexaoBancoDados.AddParametro("@Num_TipoOperacao", operacaoDTO.Num_TipoOperacao);
            _conexaoBancoDados.AddParametro("@Num_Operacao", operacaoDTO.Num_Operacao);
            _conexaoBancoDados.AddParametro("@Date_DataOperacao", operacaoDTO.Date_DataOperacao);

            _conexaoBancoDados.ExecutarSemRetorno();
        }

        public int Transferencia(OperacaoDTO operacaoDTO)
        {
            _conexaoBancoDados.ExecutarProcedure("InsTransferencia");
            _conexaoBancoDados.AddParametro("@Num_idConta1", operacaoDTO.Num_idConta1);
            _conexaoBancoDados.AddParametro("@Num_Valor", operacaoDTO.Num_Valor);
            _conexaoBancoDados.AddParametro("@Num_TipoOperacao", operacaoDTO.Num_TipoOperacao);
            _conexaoBancoDados.AddParametro("@Num_Operacao", operacaoDTO.Num_Operacao);
            _conexaoBancoDados.AddParametro("@Date_DataOperacao", operacaoDTO.Date_DataOperacao);
            _conexaoBancoDados.AddParametro("@Num_idConta2", operacaoDTO.Num_idConta2);

            int retornoBanco = _conexaoBancoDados.ExecuteNoQueryWithReturn();
            if (retornoBanco == 1)
            {
                _notification.adicionaErro("Conta do destino da transferência não encontrada");
            }

            return retornoBanco;
        }

        public void AtualizaSaldo(OperacaoDTO operacaoDTO)
        {
            _conexaoBancoDados.ExecutarProcedure("AltSaldo");
            _conexaoBancoDados.AddParametro("@Num_TipoOperacao", operacaoDTO.Num_TipoOperacao);
            _conexaoBancoDados.AddParametro("@Num_Valor", operacaoDTO.Num_Valor);
            _conexaoBancoDados.AddParametro("@Num_idConta1", operacaoDTO.Num_idConta1);
            _conexaoBancoDados.AddParametro("@Num_idConta2", operacaoDTO.Num_idConta2);

            _conexaoBancoDados.ExecutarSemRetorno();
        }

        public int VerificaSaldo(OperacaoDTO operacaoDTO)
        {
            _conexaoBancoDados.ExecutarProcedure("VerificaSaldoNaConta");
            _conexaoBancoDados.AddParametro("@Num_idConta1", operacaoDTO.Num_idConta1);
            _conexaoBancoDados.AddParametro("@Num_Valor", operacaoDTO.Num_Valor);

            int retornoBanco = _conexaoBancoDados.ExecuteNoQueryWithReturn();
            if (retornoBanco == 1)
            {
                _notification.adicionaErro("Saldo Insuficiente");
            }

            return retornoBanco;
        }
    }
}
