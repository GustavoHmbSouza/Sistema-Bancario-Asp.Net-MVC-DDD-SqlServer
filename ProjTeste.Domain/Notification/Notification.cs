using System.Collections.Generic;

namespace ProjTeste.Domain.Notification
{
    public class Notification : INotification
    {
        private List<string> Erros;

        public Notification()
        {
            this.Erros = new List<string>();
        }

        public void adicionaErro(string Erro)
        {
            Erros.Add(Erro);
        }

        public List<string> getErros()
        {
            return this.Erros;
        }
    }
}
