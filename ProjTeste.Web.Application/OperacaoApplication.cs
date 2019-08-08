using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using ProjTeste.Web.Application.Conta.Model;
using System.Net.Http;
using System.Net.Http.Formatting;

namespace ProjTeste.Web.Application
{
    public class OperacaoApplication
    {
        static HttpClient client = new HttpClient();

        public HttpResponseMessage GetExtrato(int id)
        {
            string URL = "http://localhost:19868/api/Operacao/GetExtrato" + "?id="+id.ToString();
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = client.GetAsync(URL).Result;

            return response;
        }

        public HttpResponseMessage Depositar(OperacaoModel operacaoModel)
        {
            string URL = "http://localhost:19868/api/Operacao/Deposito";

            HttpResponseMessage response = client.PostAsync(URL, operacaoModel, new JsonMediaTypeFormatter
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

        public HttpResponseMessage Sacar(OperacaoModel operacaoModel)
        {
            string URL = "http://localhost:19868/api/Operacao/Saque";

            HttpResponseMessage response = client.PostAsync(URL, operacaoModel, new JsonMediaTypeFormatter
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

        public HttpResponseMessage Transferencia(OperacaoModel operacaoModel)
        {
            string URL = "http://localhost:19868/api/Operacao/Transferencia";

            HttpResponseMessage response = client.PostAsync(URL, operacaoModel, new JsonMediaTypeFormatter
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
    }
}
