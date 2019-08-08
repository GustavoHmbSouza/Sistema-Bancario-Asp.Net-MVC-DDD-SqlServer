using ProjTeste.Domain.Entities;
using System.Collections.Generic;

namespace ProjTeste.Domain.Operacoes
{
    public interface IOperacaoRepository
    {
		void Saque(OperacaoDTO operacaoDTO);
		void Deposito(OperacaoDTO operacaoDTO);
		void Transferencia(OperacaoDTO operacaoDTO);
		void Estorno(OperacaoDTO operacaoDTO);
        IEnumerable<OperacaoDTO> GetExtrato(int id);
    }
}
