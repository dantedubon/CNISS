using System;
using Autofac;
using CNISS.CommonDomain.Application;
using CNISS.EnterpriseDomain.Application;
using CNISS.EnterpriseDomain.Domain.Entities;
using CNISS.EnterpriseDomain.Domain.Repositories;
using CNISS.EnterpriseDomain.Ports.Output;
using CNISS.EnterpriseDomain.Ports.Output.Database;

namespace CNISS.Bootstraper
{
    public class ConfigureGremioDependencies : IBootstrapperTask<ContainerBuilder>
    {
        public Action<ContainerBuilder> Task
        {
            get
            {
                return builder =>
                {
                    builder.RegisterType<GremioRepositoryReadOnly>().As<IGremioRepositoryReadOnly>();
                    builder.RegisterType<GremioRepositoryCommands>().As<IGremioRepositoryCommands>();
                  
                    
                    builder.RegisterType<ServiceDireccionValidator>().As<IServiceDireccionValidator>();
                    builder.RegisterType<RepresentanteLegalRepositoryReadOnly>()
                        .As<IRepresentanteLegalRepositoryReadOnly>();
                    builder.RegisterType<CommandInsertGremio>().As<ICommandInsertIdentity<Gremio>>();
                    builder.RegisterType<CommandUpdateGremioRepresentante>().As<ICommandUpdateGremioRepresentante>();



                };
            }

        }
    }
}