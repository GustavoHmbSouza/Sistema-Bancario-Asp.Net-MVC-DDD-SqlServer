using ProjTeste.Web.Application.Conta;
using ProjTeste.Web.Application.Conta.Model;
using System.Collections.Generic;
using System.Net.Http;
using System.Web.Mvc;

namespace ProjTeste.Web.Controllers
{
    public class HomeController : Controller
    {
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
            ContaApplication contaApplication = new ContaApplication();

            HttpResponseMessage response = contaApplication.GetConta();

            return View(response.Content.ReadAsAsync<List<ContaModel>>().Result);
        }

        public ActionResult Edit(int id)
        {
            ContaApplication contaApplication = new ContaApplication();

            HttpResponseMessage response = contaApplication.GetConta(id);

            return View(response.Content.ReadAsAsync<ContaModel>().Result);
        }

        public ActionResult Put(ContaModel contaModel)
        {
            ContaApplication contaApplication = new ContaApplication();

            HttpResponseMessage response = contaApplication.Put(contaModel);

            return View("Index");
        }

        public ActionResult Create()
        {
            ContaModel contaModel = new ContaModel();
            return View(contaModel);
        }

        public ActionResult Post(ContaModel contaModel)
        {
            ContaApplication contaApplication = new ContaApplication();

            HttpResponseMessage response = contaApplication.Post(contaModel);

            return View("Index");
        }

        public ActionResult Delete(int id)
        {
            ContaApplication contaApplication = new ContaApplication();

            HttpResponseMessage response = contaApplication.Delete(id);

            return View("Index");
        }

        public ActionResult Details(int id)
        {
            ContaApplication contaApplication = new ContaApplication();

            HttpResponseMessage response = contaApplication.GetConta(id);

            return View(response.Content.ReadAsAsync<ContaModel>().Result);
        }
    }
}