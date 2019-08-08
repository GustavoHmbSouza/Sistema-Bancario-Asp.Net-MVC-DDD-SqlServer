using ProjTeste.Domain.Conta;
using ProjTeste.Domain.Entities;
using System;
using System.Web.Http;

namespace ProjTeste.Api.Controllers
{
    public class ContaController : ApiController
    {
        public readonly IContaRepository _contaRepository;

        public ContaController(IContaRepository contaRepository)
        {
            _contaRepository = contaRepository;
        }

        public IHttpActionResult Delete(int id)
        {
            try
            {
                _contaRepository.Delete(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest("Erro ao excluir conta: " + ex.Message);
            }
        }

        public IHttpActionResult Get()
        {
            try
            {
                return Ok(_contaRepository.Get());
            }
            catch (Exception ex)
            {
                return BadRequest("Erro ao buscar conta: " + ex.Message);
            }
        }

        public IHttpActionResult Get(int id)
        {
            try
            {
                return Ok(_contaRepository.Get(id));
            }
            catch (Exception ex)
            {
                return BadRequest("Erro ao buscar conta: " + ex.Message);
            }
        }

        public IHttpActionResult Post([FromBody] ContaDTO Conta)
        {
            try
            {
                _contaRepository.Post(Conta);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest("Erro ao inserir conta: " + ex.Message);
            }

        }

        public IHttpActionResult Put(ContaDTO Conta)
        {
            try
            {
                _contaRepository.Put(Conta);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest("Erro ao atualizar conta: " + ex.Message);
            }
        }
    }
}