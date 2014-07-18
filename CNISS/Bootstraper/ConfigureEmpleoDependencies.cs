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
using CNISS.EnterpriseDomain.Domain.ValueObjects;
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
                    builder.RegisterType<EmpleoRepositoryCommands>().As<IEmpleoRepositoryCommands>();
                    builder.RegisterType<CommandInsertEmpleo>().As<ICommandInsertIdentity<Empleo>>();
                    builder.RegisterType<CommandUpdateEmpleo>().As<ICommandUpdateIdentity<Empleo>>();
                    builder.RegisterType<CommandUpdateEmpleoContrato>().As<ICommandUpdateEmpleoContrato>();
                    builder.RegisterType<CommandUpdateEmpleoImagenComprobantePago>()
                        .As<ICommandUpdateEmpleoImagenComprobantePago>();
                    builder.RegisterType<TipoDeEmpleoRepositoryReadOnly>().As<ITipoDeEmpleoReadOnlyRepository>();

                    builder.RegisterType<MotivoDespidoRepositoryReadOnly>().As<IMotivoDespidoRepositoryReadOnly>();
                    builder.RegisterType<ProvideAllowedDaysForNewEmpleo>().As<IProvideAllowedDaysForNewEmpleo>();
                    builder.RegisterType<CommandInsertTipoEmpleo>().As<ICommandInsertIdentity<TipoEmpleo>>();
                    builder.RegisterType<CommandInsertMotivoDespido>().As<ICommandInsertIdentity<MotivoDespido>>();
                    builder.RegisterType<CommandUpdateTipoEmpleo>().As<ICommandUpdateIdentity<TipoEmpleo>>();
                    builder.RegisterType<CommandUpdateMotivoDespido>().As<ICommandUpdateIdentity<MotivoDespido>>();
                    builder.RegisterType<TipoDeEmpleoRepositoryCommand>().As<IRepositoryCommands<TipoEmpleo>>();
                    builder.RegisterType<MotivoDespidoRepositoryCommands>().As<IRepositoryCommands<MotivoDespido>>();
                    builder.RegisterType<CommandInsertFichaDeSupervision>().As<ICommandInsertFichaDeSupervision>();
                    builder.RegisterType<CommandInsertNotaDespido>().As<ICommandInsertNotaDespido>();
                };
            }
        }
    }


}