using ProjTeste.Web.Application;
using ProjTeste.Web.Application.Conta.Model;
using System.Collections.Generic;
using System.Net.Http;
using System.Web.Mvc;

namespace ProjTeste.Web.Controllers
{
    public class OperacaoController : Controller
    {
        // GET: Operacao
        public ActionResult ListExtrato(int id)
        {
            OperacaoApplication operacaoApplication = new OperacaoApplication();

            HttpResponseMessage response = operacaoApplication.GetExtrato(id);

            return View(response.Content.ReadAsAsync<List<OperacaoModel>>().Result);
        }

        public ActionResult CreateDeposito(int id)
        {
            OperacaoModel operacaoModel = new OperacaoModel
            {
                Num_idConta1 = id,
                Num_TipoOperacao = 2
            };
            return View(operacaoModel);
        }

        public ActionResult CreateSaque(int id)
        {
            OperacaoModel operacaoModel = new OperacaoModel
            {
                Num_idConta1 = id,
                Num_TipoOperacao = 1
            };
            return View(operacaoModel);
        }

        public ActionResult CreateTransferencia(int id)
        {
            OperacaoModel operacaoModel = new OperacaoModel
            {
                Num_idConta1 = id,
                Num_TipoOperacao = 3
            };
            return View(operacaoModel);
        }

        public ActionResult PostDeposito(OperacaoModel operacaoModel)
        {
            OperacaoApplication operacaoApplication = new OperacaoApplication();

            HttpResponseMessage response = operacaoApplication.Depositar(operacaoModel);

            return RedirectToAction("ListExtrato", new { id = operacaoModel.Num_idConta1 });
        }

        public ActionResult PostSaque(OperacaoModel operacaoModel)
        {
            OperacaoApplication operacaoApplication = new OperacaoApplication();

            HttpResponseMessage response = operacaoApplication.Sacar(operacaoModel);

            return RedirectToAction("ListExtrato", new { id = operacaoModel.Num_idConta1 });
        }

        public ActionResult PostTransferencia(OperacaoModel operacaoModel)
        {
            OperacaoApplication operacaoApplication = new OperacaoApplication();

            HttpResponseMessage response = operacaoApplication.Transferencia(operacaoModel);

            return RedirectToAction("ListExtrato", new { id = operacaoModel.Num_idConta1 });
        }
    }
}