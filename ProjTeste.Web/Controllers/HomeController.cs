using ProjTeste.Web.Application;
using ProjTeste.Web.Application.Conta;
using ProjTeste.Web.Application.Conta.Model;
using ProjTeste.Web.Application.TipoConta.Model;
using ProjTeste.Web.Application.ViewModel;
using System.Collections.Generic;
using System.Net.Http;
using System.Web.Mvc;

namespace ProjTeste.Web.Controllers
{
    public class HomeController : Controller
    {
        ContaApplication _contaApplication;
        TipoContaApplication _tipoContaApplication;

        public HomeController()
        {
            ContaApplication contaApplication = new ContaApplication();
            TipoContaApplication tipoContaApplication = new TipoContaApplication();
            _contaApplication = contaApplication;
            _tipoContaApplication = tipoContaApplication;
        }
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult List()
        {
            HttpResponseMessage response = _contaApplication.GetConta();

            return View(response.Content.ReadAsAsync<List<ContaModel>>().Result);
        }

        public ActionResult Edit(int id)
        {
            HttpResponseMessage response = _contaApplication.GetConta(id);

            return View(response.Content.ReadAsAsync<ContaModel>().Result);
        }

        public ActionResult Put(ContaModel contaModel)
        {
            HttpResponseMessage response = _contaApplication.Put(contaModel);

            return View("Index");
        }

        public ActionResult Create()
        {
            HttpResponseMessage response = _tipoContaApplication.Get();
            ContaTipoContaViewModel contaTipoContaViewModel = new ContaTipoContaViewModel
            {
                ContaModel = new ContaModel(),
                TipoContaModel = response.Content.ReadAsAsync<TipoContaModel>().Result
            };
            return View(contaTipoContaViewModel);
        }

        public ActionResult Post(ContaModel contaModel)
        {
            HttpResponseMessage response = _contaApplication.Post(contaModel);

            return View("Index");
        }

        public ActionResult Delete(int id)
        {
            HttpResponseMessage response = _contaApplication.Delete(id);

            return View("Index");
        }

        public ActionResult Details(int id)
        {
            HttpResponseMessage response = _contaApplication.GetConta(id);

            return View(response.Content.ReadAsAsync<ContaModel>().Result);
        }
    }
}