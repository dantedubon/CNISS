using System;
using System.Reflection;
using CNISS.AutenticationDomain.Domain.ValueObjects;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Dialect;
using NHibernate.Driver;
using NHibernate.Tool.hbm2ddl;
using Environment = NHibernate.Cfg.Environment;

namespace CNISS_Integration_Test
{

    public class SessionFactoryTest 
    {
         private static ISessionFactory _sessionFactory;
        private static ISessionFactory SessionFactory
        {
            get
            {
                return _sessionFactory ?? (_sessionFactory = Fluently.Configure()
                    .Database(SQLiteConfiguration
                        .Standard
                        .InMemory()
                   
                    )
                    .Mappings(m => m.FluentMappings.AddFromAssemblyOf<Rol>())
                    .ExposeConfiguration(cfg => new SchemaExport(cfg).Create(true, true))
                    .BuildSessionFactory());
            }
        }
        public static ISession OpenSession()
        {
            return SessionFactory.OpenSession();
        }

      
    }
    

       
    
}