using ProjTeste.Web.Application.Conta.Model;
using ProjTeste.Web.Application.TipoConta.Model;
using System.Collections.Generic;

namespace ProjTeste.Web.Application.ViewModel
{
    public class ContaTipoContaViewModel
    {
        public ContaModel ContaModel { get; set; }
        public IEnumerable<TipoContaModel>TipoContaModel { get; set; }

    }
}
