using System;
using Autofac;
using CNISS.CommonDomain.Ports.Input.REST;
using CNISS.EnterpriseDomain.Domain.Repositories;
using CNISS.EnterpriseDomain.Ports.Output.Database;

namespace CNISS.Bootstraper
{
    public class ConfigureEmpresaDependencies : IBootstrapperTask<ContainerBuilder>
    {
        public Action<ContainerBuilder> Task
        {
            get
            {
                return builder =>
                {
                    builder.RegisterType<FilePersister>().As<IFilePersister>();
                    builder.RegisterType<ActividadEconomicaRepositoryReadOnly>()
                        .As<IActividadEconomicaRepositoryReadOnly>();


                };
            }

        }
    }
}