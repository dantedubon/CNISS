using System;
using System.Reflection;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;

namespace CNISS_Integration_Test
{
    /// <summary>
    /// http://ayende.com/blog/3983/nhibernate-unit-testing
    /// </summary>
    public class InMemoryDatabaseTest : IDisposable
    {
        private  Configuration Configuration;
        private  ISessionFactory SessionFactory;
        public ISession session;

        public InMemoryDatabaseTest(Assembly assemblyContainingMapping)
        {
            if (Configuration == null)
            {
                Configuration = buildConfiguration( assemblyContainingMapping);

                SessionFactory = Configuration.BuildSessionFactory();

            }

      

            
        }

        public void openSession()
        {
            session = SessionFactory.OpenSession();
            SchemeExport();
        }

        public void SchemeExport()
        {
            new SchemaExport(Configuration).Execute(true, true, false, session.Connection, Console.Out);
        }

        public void SchemeExport(ISession sessionOther)
        {
            new SchemaExport(Configuration).Execute(true, true, false, sessionOther.Connection, Console.Out);
        }


        public  ISessionFactory sessionFactory
        {
            get { return SessionFactory; }
        }

        private Configuration buildConfiguration(Assembly assemblyContainingMapping)
        {
            return Fluently.Configure()
                .Database(SQLiteConfiguration
                    .Standard
                    .InMemory())
                .Mappings(m => m.FluentMappings.AddFromAssembly(assemblyContainingMapping))
                .BuildConfiguration();
        }


        public void Dispose()
        {
            session.Dispose();
        }
    }
}