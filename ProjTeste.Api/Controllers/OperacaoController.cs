using ProjTeste.Domain.Entities;
using ProjTeste.Domain.Operacoes;
using System;
using System.Web.Http;

namespace ProjTeste.Api.Controllers
{
    public class OperacaoController : ApiController
    {
        public readonly IOperacaoRepository _operacaoRepository;

        public OperacaoController(IOperacaoRepository operacaoRepository)
        {
            _operacaoRepository = operacaoRepository;
        }
        public IHttpActionResult GetExtrato(int id)
        {
            try
            {
                return Ok(_operacaoRepository.GetExtrato(id));
            }
            catch (Exception ex)
            {
                return BadRequest("Erro ao buscar extrato: " + ex.Message);
            }
        }

        public IHttpActionResult Saque(OperacaoDTO operacaoDTO)
        {
            try
            {
                _operacaoRepository.AtualizaSaldo(operacaoDTO);
                _operacaoRepository.Saque(operacaoDTO);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest("Erro ao sacar um valor: " + ex.Message);
            }
        }

        public IHttpActionResult Deposito(OperacaoDTO operacaoDTO)
        {
            try
            {
                _operacaoRepository.AtualizaSaldo(operacaoDTO);
                _operacaoRepository.Deposito(operacaoDTO);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest("Erro ao depositar um valor: " + ex.Message);
            }
        }

        public IHttpActionResult Transferencia(OperacaoDTO operacaoDTO)
        {
            try
            {
                _operacaoRepository.AtualizaSaldo(operacaoDTO);
                _operacaoRepository.Transferencia(operacaoDTO);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest("Erro ao transferir um valor: " + ex.Message);
            }
        }
    }
}