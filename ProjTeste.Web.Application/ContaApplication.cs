using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using ProjTeste.Web.Application.Conta.Model;
using System.Net.Http;
using System.Net.Http.Formatting;

namespace ProjTeste.Web.Application.Conta
{
    public class ContaApplication
    {
        static HttpClient client = new HttpClient();

        public HttpResponseMessage GetConta()
        {
            string URL = "http://localhost:19868/api/Conta";
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = client.GetAsync(URL).Result;

            return response;
        }

        public HttpResponseMessage GetConta(int id)
        {
            string URL = "http://localhost:19868/api/Conta" + "?id=" + id.ToString();
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = client.GetAsync(URL).Result;

            return response;
        }

        public HttpResponseMessage Put(ContaModel contaModel)
        {
            string URL = "http://localhost:19868/api/Conta/Put";
            //client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
           
            HttpResponseMessage response = client.PutAsync(URL, contaModel, new JsonMediaTypeFormatter
            {
                SerializerSettings = new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Include,
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                    ContractResolver = new DefaultContractResolver
                    {
                        IgnoreSerializableAttribute = true
                    }
                }
            }).Result; 


            return response;
        }

        public HttpResponseMessage Post(ContaModel contaModel)
        {
            string URL = "http://localhost:19868/api/Conta/Post";
            //client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = client.PostAsync(URL, contaModel, new JsonMediaTypeFormatter
            {
                SerializerSettings = new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Include,
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                    ContractResolver = new DefaultContractResolver
                    {
                        IgnoreSerializableAttribute = true
                    }
                }
            }).Result;


            return response;
        }

        public HttpResponseMessage Delete(int id)
        {
            string URL = "http://localhost:19868/api/Conta" + "?id=" + id.ToString();
            //client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = client.DeleteAsync(URL).Result;


            return response;
        }
    }
}
