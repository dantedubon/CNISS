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
using NHibernate.Action;

namespace CNISS.Bootstraper
{
    public class ConfigureEmpleoDependencies : IBootstrapperTask<ContainerBuilder>
    {
        public Action<ContainerBuilder> Task
        {
            get
            {
                return builder =>
                {
                    builder.RegisterType<EmpleoRepositoryReadOnly>().As<IEmpleoRepositoryReadOnly>();
                    builder.RegisterType<EmpleoRepositoryCommands>().As<IRepositoryCommands<Empleo>>();
                    builder.RegisterType<CommandInsertEmpleo>().As<ICommandInsertIdentity<Empleo>>();
                    builder.RegisterType<TipoDeEmpleoRepositoryReadOnly>().As<ITipoDeEmpleoReadOnlyRepository>();
                    builder.RegisterType<ProvideAllowedDaysForNewEmpleo>().As<IProvideAllowedDaysForNewEmpleo>();

                };
            }
        }
    }


}