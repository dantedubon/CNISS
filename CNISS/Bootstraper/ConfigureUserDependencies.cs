using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Autofac;
using CNISS.AutenticationDomain.Application.Comandos;
using CNISS.AutenticationDomain.Domain.Entities;
using CNISS.AutenticationDomain.Domain.Repositories;
using CNISS.AutenticationDomain.Ports.Output.Database;
using CNISS.CommonDomain.Application;

namespace CNISS.Bootstraper
{
    public class ConfigureUserDependencies : IBootstrapperTask<ContainerBuilder>
    {
        public Action<ContainerBuilder> Task
        {
            get
            {
                return builder =>
                {
                    builder.RegisterType<UserRepositoryReadOnly>().As<IUserRepositoryReadOnly>();
                    builder.RegisterType<UserRepositoryCommands>().As<IUserRepositoryCommands>();
                    builder.RegisterType<CommandInsertUser>().As<ICommandInsertIdentity<User>>();
                
                };
            }
        }
    }
}