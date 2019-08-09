using ProjTeste.Domain.ConexaoBancoDados;
using ProjTeste.Domain.Entities;
using ProjTeste.Domain.Operacoes;
using System;
using System.Collections.Generic;

namespace ProjTeste.Repository.Repositories
{
    public class OperacaoRepository : IOperacaoRepository
    {
        private readonly IConexaoBancoDados _conexaoBancoDados;

        public OperacaoRepository(IConexaoBancoDados conexaoBancoDados)
        {
            _conexaoBancoDados = conexaoBancoDados;
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
                        Nom_Destinatario = leitor.GetString(leitor.GetOrdinal("Nom_Destinatario"))
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

            if (_conexaoBancoDados.ExecuteNoQueryWithReturn() == 0)
            {
                AtualizaSaldo(operacaoDTO);
            }
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

            if (_conexaoBancoDados.ExecuteNoQueryWithReturn() == 0)
            {
                AtualizaSaldo(operacaoDTO);
            }
        }

        public void Transferencia(OperacaoDTO operacaoDTO)
        {
            _conexaoBancoDados.ExecutarProcedure("InsTransferencia");
            _conexaoBancoDados.AddParametro("@Num_idConta1", operacaoDTO.Num_idConta1);
            _conexaoBancoDados.AddParametro("@Num_Valor", operacaoDTO.Num_Valor);
            _conexaoBancoDados.AddParametro("@Num_TipoOperacao", operacaoDTO.Num_TipoOperacao);
            _conexaoBancoDados.AddParametro("@Num_Operacao", operacaoDTO.Num_Operacao);
            _conexaoBancoDados.AddParametro("@Date_DataOperacao", operacaoDTO.Date_DataOperacao);
            _conexaoBancoDados.AddParametro("@Num_idConta2", operacaoDTO.Num_idConta2);

            if (_conexaoBancoDados.ExecuteNoQueryWithReturn() == 0)
            {
                AtualizaSaldo(operacaoDTO);
            }
        }

        /* @Num_TipoOperacao TINYINT, @Num_Valor DECIMAL, @Num_idConta1 INT, @Num_idConta2 INT*/
        public void AtualizaSaldo(OperacaoDTO operacaoDTO)
        {
            _conexaoBancoDados.ExecutarProcedure("AltSaldo");
            _conexaoBancoDados.AddParametro("@Num_TipoOperacao", operacaoDTO.Num_TipoOperacao);
            _conexaoBancoDados.AddParametro("@Num_Valor", operacaoDTO.Num_Valor);
            _conexaoBancoDados.AddParametro("@Num_idConta1", operacaoDTO.Num_idConta1);
            _conexaoBancoDados.AddParametro("@Num_idConta2", operacaoDTO.Num_idConta2);

            _conexaoBancoDados.ExecutarSemRetorno();
        }
    }
}
