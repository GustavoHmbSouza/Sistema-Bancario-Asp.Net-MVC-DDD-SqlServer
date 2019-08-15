using ProjTeste.Domain.Entities;
using ProjTeste.Domain.Notification;
using ProjTeste.Domain.Operacoes;

namespace ProjTeste.Domain.Operacao
{
    public class OperacaoService : IOperacaoService
    {
        private readonly IOperacaoRepository _operacaoRepository;
        private readonly INotification _notification;

        public OperacaoService(IOperacaoRepository operacaoRepository, INotification notification)
        {
            _operacaoRepository = operacaoRepository;
            _notification = notification;
        }

        public void Deposito(OperacaoDTO operacaoDTO)
        {
            _operacaoRepository.Deposito(operacaoDTO);
            _operacaoRepository.AtualizaSaldo(operacaoDTO);
        }

        public void Estorno(OperacaoDTO operacaoDTO)
        {
            //_operacaoRepository.Estorno(operacaoDTO);
        }

        public void Saque(OperacaoDTO operacaoDTO)
        {
            if (_operacaoRepository.VerificaSaldo(operacaoDTO) == 0)
            {
                _operacaoRepository.Saque(operacaoDTO);
                _operacaoRepository.AtualizaSaldo(operacaoDTO);
            }
        }

        public void Transferencia(OperacaoDTO operacaoDTO)
        {
            
            

            if (_operacaoRepository.VerificaSaldo(operacaoDTO) != 0)
            {
                //desfaz os comandos
                return;
            }
            if (_operacaoRepository.Transferencia(operacaoDTO) != 0)
            {
                //desfaz os comandos
                return;
            }

            _operacaoRepository.AtualizaSaldo(operacaoDTO);
            
        }
    }
}
