using ProjTeste.Domain.ConexaoBancoDados;
using ProjTeste.Domain.Conta;
using ProjTeste.Domain.Entities;
using System.Collections.Generic;

namespace ProjTeste.Repository.Repositories
{
    public class ContaRepository : IContaRepository
    {
        private readonly IConexaoBancoDados _conexaoBancoDados;

        public ContaRepository(IConexaoBancoDados conexaoBancoDados)
        {
            _conexaoBancoDados = conexaoBancoDados;
        }
        public void Delete(int id)
        {
            _conexaoBancoDados.ExecutarProcedure("DelConta");
            _conexaoBancoDados.AddParametro("@Num_SeqlConta", id);
            _conexaoBancoDados.ExecutarSemRetorno();
        }

        public IEnumerable<ContaDTO> Get()
        {
            _conexaoBancoDados.ExecutarProcedure("SelConta");

            var contas = new List<ContaDTO>();
            using (var leitor = _conexaoBancoDados.ExecuteReader())
            {
                while (leitor.Read())
                {
                    contas.Add(new ContaDTO
                    {
                        ContaId = leitor.GetInt32(leitor.GetOrdinal("Num_SeqlConta")),
                        NumeroConta = leitor.GetByte(leitor.GetOrdinal("Num_Conta")),
                        Nome = leitor.GetString(leitor.GetOrdinal("Nom_Nome")),
                        Saldo = leitor.GetDecimal(leitor.GetOrdinal("Num_Saldo")),
                        DataCriacao = leitor.GetDateTime(leitor.GetOrdinal("Date_DataCriacao")),
                        TipoConta = leitor.GetByte(leitor.GetOrdinal("Num_TipoConta"))
                    });
                }
            }

            return contas;
        }

        public ContaDTO Get(int id)
        {
            _conexaoBancoDados.ExecutarProcedure("SelContaId");
            _conexaoBancoDados.AddParametro("@Num_SeqlConta", id);

            var contas = new ContaDTO();

            using (var leitor = _conexaoBancoDados.ExecuteReader())
            {
                if (leitor.Read())
                {
                    contas = new ContaDTO
                    {
                        ContaId = leitor.GetInt32(leitor.GetOrdinal("Num_SeqlConta")),
                        NumeroConta = leitor.GetByte(leitor.GetOrdinal("Num_Conta")),
                        Nome = leitor.GetString(leitor.GetOrdinal("Nom_Nome")),
                        Saldo = leitor.GetDecimal(leitor.GetOrdinal("Num_Saldo")),
                        DataCriacao = leitor.GetDateTime(leitor.GetOrdinal("Date_DataCriacao")),
                        TipoConta = leitor.GetByte(leitor.GetOrdinal("Num_TipoConta"))
                    };
                }
            }
            return contas;
        }

        public void Post(ContaDTO Conta)
        {
            _conexaoBancoDados.ExecutarProcedure("InsConta");
            _conexaoBancoDados.AddParametro("@Num_Conta", Conta.NumeroConta);
            _conexaoBancoDados.AddParametro("@Nom_Nome", Conta.Nome);
            _conexaoBancoDados.AddParametro("@Num_Saldo", Conta.Saldo);  
            _conexaoBancoDados.AddParametro("@Date_DataCriacao", Conta.DataCriacao);
             _conexaoBancoDados.AddParametro("@Num_TipoConta", Conta.TipoConta);

            _conexaoBancoDados.ExecutarSemRetorno();
        }

        public void Put(ContaDTO Conta)
        {
            _conexaoBancoDados.ExecutarProcedure("UpdConta");
            _conexaoBancoDados.AddParametro("@Num_SeqlConta", Conta.ContaId);
            _conexaoBancoDados.AddParametro("@Num_Conta", Conta.NumeroConta);
            _conexaoBancoDados.AddParametro("@Nom_Nome", Conta.Nome);
            _conexaoBancoDados.AddParametro("@Num_Saldo", Conta.Saldo);
            _conexaoBancoDados.AddParametro("@Date_DataCriacao", Conta.DataCriacao);
             _conexaoBancoDados.AddParametro("@Num_TipoConta", Conta.TipoConta);

            _conexaoBancoDados.ExecutarSemRetorno();
        }
    }
}
