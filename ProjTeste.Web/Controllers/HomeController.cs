using ProjTeste.Web.Application;
using ProjTeste.Web.Application.Conta;
using ProjTeste.Web.Application.Conta.Model;
using ProjTeste.Web.Application.TipoConta.Model;
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

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {            
                return View(response.Content.ReadAsAsync<List<ContaModel>>().Result);               
            }
            else
            {
                return View("Index");
            }
        }

        public ActionResult Edit(int id)
        {
            HttpResponseMessage response = _tipoContaApplication.Get();
            List<TipoContaModel> tipoContaModel = response.Content.ReadAsAsync<List<TipoContaModel>>().Result;
            List<SelectListItem> TipoContaDdl = new List<SelectListItem>();
            ContaModel contaModel = _contaApplication.GetConta(id).Content.ReadAsAsync<ContaModel>().Result;

            foreach (var TipoConta in tipoContaModel)
            {
                TipoContaDdl.Add(new SelectListItem
                {
                    Text = TipoConta.Nom_Nome,
                    Value = TipoConta.Cod_Conta.ToString(),
                    Selected = (TipoConta != null) && (TipoConta.Cod_Conta == contaModel.TipoConta)
                });
            }
            ViewBag.TipoConta = TipoContaDdl;

            return View(contaModel);
        }

        public ActionResult Put(ContaModel contaModel)
        {
            HttpResponseMessage response = _contaApplication.Put(contaModel);

            return View("Index");
        }

        public ActionResult Create()
        {
            HttpResponseMessage response = _tipoContaApplication.Get();
            List<TipoContaModel> tipoContaModel = response.Content.ReadAsAsync<List<TipoContaModel>>().Result;

            List<SelectListItem> TipoContaDdl = new List<SelectListItem>();

            foreach(var TipoConta in tipoContaModel)
            {
                TipoContaDdl.Add(new SelectListItem {
                    Text = TipoConta.Nom_Nome,
                    Value = TipoConta.Cod_Conta.ToString()
                });
            }

            ViewBag.TipoConta = TipoContaDdl;

            return View(new ContaModel());
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