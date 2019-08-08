using ProjTeste.Domain.Entities;
using System.Collections.Generic;

namespace ProjTeste.Domain.TipoConta
{
    public interface ITipoContaRepository
    {
        IEnumerable<TipoContaDTO> Get();
    }
}
