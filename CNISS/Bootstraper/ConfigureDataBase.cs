using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using Autofac;
using CNISS.AutenticationDomain.Ports.Output.Database.Mappings;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using Iesi.Collections;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;

namespace CNISS.Bootstraper
{
    public class ConfigureDataBase : IBootstrapperTask<ContainerBuilder>
    {
        public Action<ContainerBuilder> Task
        {
            get
            {

                return builder =>
                {
                    builder.RegisterInstance(SessionFactory).As<ISessionFactory>().SingleInstance();
                    builder.Register(c => c.Resolve<ISessionFactory>().OpenSession())
                        .As<ISession>()
                        .InstancePerLifetimeScope();
                };

            }
            
        }



        private ISessionFactory SessionFactory
        {
            get
            {
                Configuration config = Fluently.Configure()
               .Database(
                   MsSqlConfiguration
                   .MsSql2012
                    .ConnectionString(c => c.FromConnectionStringWithKey("CENSS_SQL")))
                   .Mappings(m => m.FluentMappings.AddFromAssemblyOf<RolMapping>())
                   .ExposeConfiguration(cfg => new SchemaUpdate(cfg).Execute(false, true))
               .BuildConfiguration();

                return config.BuildSessionFactory();
            }
            
        }

     
    }
}