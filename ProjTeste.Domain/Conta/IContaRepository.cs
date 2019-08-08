using ProjTeste.Domain.Entities;
using System.Collections.Generic;

namespace ProjTeste.Domain.Conta
{
    public interface IContaRepository
    {
        IEnumerable<ContaDTO> Get();
        ContaDTO Get(int id);
        void Post(ContaDTO Conta);
        void Put(ContaDTO conta);
        void Delete(int id);
    }
}
