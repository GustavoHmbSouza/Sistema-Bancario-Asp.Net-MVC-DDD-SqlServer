using ProjTeste.Domain.Entities;
using ProjTeste.Domain.Notification;
using ProjTeste.Domain.Operacao;
using ProjTeste.Domain.Operacoes;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace ProjTeste.Api.Controllers
{
    public class OperacaoController : ApiController
    {
        public readonly IOperacaoRepository _operacaoRepository;
        public readonly IOperacaoService _operacaoService;
        public readonly INotification _notification;
        public OperacaoController(IOperacaoRepository operacaoRepository, IOperacaoService operacaoService, INotification notification)
        {
            _operacaoRepository = operacaoRepository;
            _operacaoService = operacaoService;
            _notification = notification;
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
                _operacaoService.Saque(operacaoDTO);
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
                _operacaoService.Deposito(operacaoDTO);
                List<string> ListErros = _notification.getErros();
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
                _operacaoService.Transferencia(operacaoDTO);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest("Erro ao transferir um valor: " + ex.Message);
            }
        }

        public IHttpActionResult Estorno(OperacaoDTO operacaoDTO)
        {
            try
            {
                _operacaoService.Estorno(operacaoDTO);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest("Erro ao transferir um valor: " + ex.Message);
            }
        }
    }
}