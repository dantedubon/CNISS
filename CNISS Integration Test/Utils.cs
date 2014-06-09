using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CNISS.CommonDomain.Domain;
using CNISS.CommonDomain.Ports.Output.Database;
using NHibernate;

namespace CNISS_Integration_Test
{
    public class Utils
    {
        public static void insertEntity<T,K>(K key,T entity, ISessionFactory sessionFactory)
        {
            var session = sessionFactory.OpenSession();

            using (var tr = session.BeginTransaction())
            {
                session.Save(entity,key);
                tr.Commit();
            }
            session.Close();
        }
    }
}
