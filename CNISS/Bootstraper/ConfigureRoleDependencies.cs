using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Autofac;
using CNISS.AutenticationDomain.Domain.Repositories;
using CNISS.AutenticationDomain.Ports.Output.Database;

namespace CNISS.Bootstraper
{
    public class ConfigureRoleDependencies : IBootstrapperTask<ContainerBuilder>
    {
        public Action<ContainerBuilder> Task
        {
            get
            {
                return builder => builder.RegisterType<RolRepositoryReadOnly>().As<IRolRepositoryReadOnly>();
            }
            
        }
    }
}