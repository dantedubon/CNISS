using CNISS.AutenticationDomain.Domain.ValueObjects;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Tool.hbm2ddl;

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