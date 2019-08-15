using ProjTeste.Domain.Entities;

namespace ProjTeste.Domain.Operacao
{
    public interface IOperacaoService
    {
        void Saque(OperacaoDTO operacaoDTO);
        void Transferencia(OperacaoDTO operacaoDTO);
        void Estorno(OperacaoDTO operacaoDTO);
        void Deposito(OperacaoDTO operacaoDTO);

    }
}
