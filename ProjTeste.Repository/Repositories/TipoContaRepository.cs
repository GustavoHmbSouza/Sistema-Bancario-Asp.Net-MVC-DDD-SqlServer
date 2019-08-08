using ProjTeste.Domain.ConexaoBancoDados;
using ProjTeste.Domain.Entities;
using ProjTeste.Domain.TipoConta;
using System.Collections.Generic;

namespace ProjTeste.Repository.Repositories
{
    public class TipoContaRepository : ITipoContaRepository
    {
        private readonly IConexaoBancoDados _conexaoBancoDados;

        public TipoContaRepository(IConexaoBancoDados conexaoBancoDados)
        {
            _conexaoBancoDados = conexaoBancoDados;
        }

        public IEnumerable<TipoContaDTO> Get()
        {
            _conexaoBancoDados.ExecutarProcedure("SelTipoConta");

            var linhas = new List<TipoContaDTO>();
            using (var leitor = _conexaoBancoDados.ExecuteReader())
            {
                while (leitor.Read())
                {
                    linhas.Add(new TipoContaDTO
                    {
                        Cod_Conta = leitor.GetByte(leitor.GetOrdinal("Cod_Conta")),
                        Nom_Nome = leitor.GetString(leitor.GetOrdinal("Nom_Nome"))
                    });
                }
            }
            return linhas;
        }
    }
}
