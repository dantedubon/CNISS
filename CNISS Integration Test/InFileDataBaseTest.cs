using System.IO;
using CNISS.AutenticationDomain.Domain.ValueObjects;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;

namespace CNISS_Integration_Test.Unit_Of_Work
{
    class InFileDataBaseTest
    {
        private Configuration Configuration;
        private ISessionFactory SessionFactory;
        public ISession session;

        public InFileDataBaseTest()
        {
            if (Configuration == null)
            {
                Configuration = buildConfiguration();

                SessionFactory = Configuration.BuildSessionFactory();

            }

        }

        public ISessionFactory sessionFactory
        {
            get { return SessionFactory; }
        }


        private Configuration buildConfiguration()
        {
            
            return Fluently.Configure()
                .Database(SQLiteConfiguration
                    .Standard
                    .UsingFile("dataTest.db")
                    .ShowSql)
                .Mappings(m => m.FluentMappings.AddFromAssemblyOf<Rol>())
                .ExposeConfiguration( BuildSchema)
                .BuildConfiguration();
        }

        private void BuildSchema(Configuration config)
        {

            if (File.Exists("dataTest.db"))
                File.Delete("dataTest.db");

            new SchemaExport(config).Create(true,true);
        }
    }
}
