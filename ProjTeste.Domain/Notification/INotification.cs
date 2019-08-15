using System;
using System.Collections.Generic;

namespace ProjTeste.Domain.Notification
{
    public interface  INotification 
    {
        void adicionaErro(string Erro);
        List<string> getErros();
    }
}
