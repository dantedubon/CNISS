using System;
using Autofac;
using CNISS.AutenticationDomain.Application.Comandos;
using CNISS.AutenticationDomain.Domain.Repositories;
using CNISS.AutenticationDomain.Domain.ValueObjects;
using CNISS.AutenticationDomain.Ports.Output.Database;
using CNISS.CommonDomain.Application;

namespace CNISS.Bootstraper
{
    public class ConfigureRoleDependencies : IBootstrapperTask<ContainerBuilder>
    {
        public Action<ContainerBuilder> Task
        {
            get
            {
                return builder =>
                {
                    builder.RegisterType<RolRepositoryReadOnly>().As<IRolRepositoryReadOnly>();
                    builder.RegisterType<RolRepositoryCommands>().As<IRolRepositoryCommands>();
                    builder.RegisterType<CommandInsertRol>().As<ICommandInsertIdentity<Rol>>();
                    builder.RegisterType<CommandUpdateRol>().As<ICommandUpdateIdentity<Rol>>();
                };
            }
            
        }
    }
}