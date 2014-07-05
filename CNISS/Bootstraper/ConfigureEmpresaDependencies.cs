using System;
using Autofac;
using CNISS.CommonDomain.Application;
using CNISS.CommonDomain.Ports.Input.REST;
using CNISS.CommonDomain.Ports.Input.REST.Modules.BeneficiarioModule;
using CNISS.CommonDomain.Ports.Input.REST.Modules.EmpresaModule.Commands;
using CNISS.EnterpriseDomain.Application;
using CNISS.EnterpriseDomain.Domain.Entities;
using CNISS.EnterpriseDomain.Domain.Repositories;
using CNISS.EnterpriseDomain.Ports.Output;
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
                    builder.RegisterType<FileGetter>().As<IFileGetter>();
                    builder.RegisterType<ServiceSucursalesValidator>().As<IServiceSucursalesValidator>();
                    builder.RegisterType<EmpresaRespositoryReadOnly>().As<IEmpresaRepositoryReadOnly>();
                    builder.RegisterType<EmpresaRepositoryCommands>().As<IEmpresaRepositoryCommands>();
                    builder.RegisterType<CommandUpdateEmpresaContrato>().As<ICommandUpdateEmpresaContrato>();
                    builder.RegisterType<CommandInsertEmpresa>().As<ICommandInsertIdentity<Empresa>>();
                    builder.RegisterType<CommandUpdateEmpresa>().As<ICommandUpdateIdentity<Empresa>>();
                    builder.RegisterType<ParentescoRepositoryReadOnly>().As<IParentescoReadOnlyRepository>();
                    builder.RegisterType<CommandInsertBeneficiario>().As<ICommandInsertIdentity<Beneficiario>>();
                    builder.RegisterType<CommandUpdateBeneficiario>().As<ICommandUpdateIdentity<Beneficiario>>();
                    builder.RegisterType<BeneficiarioRepositoryReadOnly>().As<IBeneficiarioRepositoryReadOnly>();
                    builder.RegisterType<BeneficiarioRepositoryCommands>().As<IBeneficiarioRepositoryCommands>();
                    builder.RegisterType<ParentescoRepositoryReadOnly>().As<IParentescoReadOnlyRepository>();

                    builder.RegisterType<ContentFileRepositoryReadOnly>().As<IContentFileRepositoryReadOnly>();



                };
            }

        }
    }
}