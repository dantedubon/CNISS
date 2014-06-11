using System;
using Autofac;
using CNISS.EnterpriseDomain.Application;
using CNISS.EnterpriseDomain.Domain.Service;

namespace CNISS.Bootstraper
{
    public class ConfigureEnterpriseServices : IBootstrapperTask<ContainerBuilder>
    {
        public Action<ContainerBuilder> Task
        {
            get
            {
                return builder =>
                {
                    builder.Register(x => new ContribuyenteDomainService());
                    builder.RegisterType<ServiceValidatorRTN>().As<IServiceValidatorRTN>();
                };
            }
        }
    }
}