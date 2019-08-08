using ProjTeste.Domain.TipoConta;
using System;
using System.Web.Http;

namespace ProjTeste.Api.Controllers
{
    public class TipoContaController : ApiController
    {
        public readonly ITipoContaRepository _tipoContaRepository;

        public TipoContaController(ITipoContaRepository tipoContaRepository)
        {
            _tipoContaRepository = tipoContaRepository;
        }

        public IHttpActionResult Get()
        {
            try
            {
                return Ok(_tipoContaRepository.Get());
            }
            catch (Exception ex)
            {
                return BadRequest("Erro ao buscar extrato: " + ex.Message);
            }
        }
    }
}