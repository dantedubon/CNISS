using System;
using Autofac;
using CNISS.AutenticationDomain.Domain.Entities;
using CNISS.CommonDomain.Domain;
using CNISS.CommonDomain.Ports.Output.Database;
using CNISS.EnterpriseDomain.Domain.ValueObjects;
using FluentNHibernate.Automapping;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
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
              
                log4net.Config.XmlConfigurator.Configure();
                return builder =>
                {
                    builder.RegisterInstance(SessionFactory).As<ISessionFactory>().SingleInstance();
                    builder.Register(c => c.Resolve<ISessionFactory>().OpenSession()).InstancePerLifetimeScope(); 
                    builder.RegisterType<NHibernateUnitOfWork>().As<IUnitOfWork>().InstancePerLifetimeScope();
                };

            }
            
        }



        private ISessionFactory SessionFactory
        {
            get
            {
                var storeConfig = new StoreConfiguration();
                Configuration config = Fluently.Configure()
               .Database(
                   MsSqlConfiguration
                   .MsSql2012.ShowSql()
                    .ConnectionString(c => c.FromConnectionStringWithKey("CENSS_SQL")))
              //     .Mappings(m => m.FluentMappings.AddFromAssemblyOf<RolMapping>())
           //     .Mappings(m => m.FluentMappings.AddFromAssemblyOf<UserMappings>())
              .Mappings(m => m.AutoMappings.Add(AutoMap.AssemblyOf<User>(storeConfig)))
              .Mappings(m => m.FluentMappings.AddFromAssemblyOf<Departamento>())
                   .ExposeConfiguration(cfg => new SchemaUpdate(cfg).Execute(false, true)
                   )
               .BuildConfiguration();

                return config.BuildSessionFactory();
            }
            
        }

     
    }
}