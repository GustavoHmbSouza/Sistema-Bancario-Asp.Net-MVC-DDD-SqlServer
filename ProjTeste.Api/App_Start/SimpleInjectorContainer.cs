﻿using ProjTeste.Domain.ConexaoBancoDados;
using ProjTeste.Domain.Conta;
using ProjTeste.Domain.Notification;
using ProjTeste.Domain.Operacao;
using ProjTeste.Domain.Operacoes;
using ProjTeste.Domain.TipoConta;
using ProjTeste.Repository.Infra;
using ProjTeste.Repository.Repositories;
using SimpleInjector;
using SimpleInjector.Lifestyles;

namespace ProjTeste.Api.App_Start
{
    public static class SimpleInjectorContainer
    {
        private static readonly Container container = new Container();

        public static Container Build()
        {
            container.Options.DefaultScopedLifestyle = new AsyncScopedLifestyle();

            container.Register<INotification, Notification>(Lifestyle.Scoped);

            RegisterRepositories();
            RegisterServices();

            container.Verify();
            return container;
        }

        private static void RegisterRepositories()
        {
            container.Register<IConexaoBancoDados, ConexaoBancoDados>();

            container.Register<IContaRepository, ContaRepository>();
            container.Register<IOperacaoRepository, OperacaoRepository>();
            container.Register<ITipoContaRepository, TipoContaRepository>();
            container.Register<IOperacaoService, OperacaoService>();
            //container.Register<INotification, Notification>();
        }

        private static void RegisterServices()
        {
        }
    }
}
