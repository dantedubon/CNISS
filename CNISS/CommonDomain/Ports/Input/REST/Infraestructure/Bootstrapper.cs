using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Autofac;
using CNISS.Bootstraper;
using Nancy.Bootstrappers.Autofac;

namespace CNISS.CommonDomain.Ports.Input.REST.Infraestructure
{
    public abstract class Bootstrapper : AutofacNancyBootstrapper
    {
        readonly List<IBootstrapperTask<ContainerBuilder>> _tasks;

        protected Bootstrapper()
        {
            _tasks = new List<IBootstrapperTask<ContainerBuilder>>
            {

            };
        }

        protected void addBootstrapperTask(IBootstrapperTask<ContainerBuilder> bootstrapper)
        {
            _tasks.Add(bootstrapper);
        }

        protected override void ConfigureApplicationContainer(ILifetimeScope existingContainer)
        {
            var builder = new ContainerBuilder();
            _tasks.ForEach(task => task.Task.Invoke(builder));
            builder.Update(existingContainer.ComponentRegistry);
            base.ConfigureApplicationContainer(existingContainer);
        }
    }
}