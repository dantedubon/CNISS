using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Autofac;
using CNISS.CommonDomain.Application;
using CNISS.CommonDomain.Domain;
using CNISS.EnterpriseDomain.Application;
using CNISS.EnterpriseDomain.Domain.Entities;
using CNISS.EnterpriseDomain.Domain.Repositories;
using CNISS.EnterpriseDomain.Ports.Output.Database;

namespace CNISS.Bootstraper
{
    public class ConfigureVisitaDependencies : IBootstrapperTask<ContainerBuilder>
    {
        public Action<ContainerBuilder> Task
        {
            get
            {
                return builder =>
                {
                    builder.RegisterType<VisitaRepositoryReadOnly>().As<IVisitaRepositoryReadOnly>();
                    builder.RegisterType<VisitaRepositoryCommand>().As<IRepositoryCommands<Visita>>();
                    builder.RegisterType<CommandInsertVisita>().As<ICommandInsertIdentity<Visita>>();
                    builder.RegisterType<CommandUpdateVisita>().As<ICommandUpdateIdentity<Visita>>();
                };
            } 
        }
    }
}