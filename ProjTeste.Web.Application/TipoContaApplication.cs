using System.Net.Http;

namespace ProjTeste.Web.Application
{
    public class TipoContaApplication
    {
        static HttpClient client = new HttpClient();

        public HttpResponseMessage Get()
        {
            string URL = "http://localhost:19868/api/TipoConta";
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = client.GetAsync(URL).Result;

            return response;
        }
    }
}

