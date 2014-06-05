using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CNISS.CommonDomain.Domain;
using NHibernate;
using NHibernate.Linq;

namespace CNISS.CommonDomain.Ports.Output.Database
{
    public abstract  class NHibernateReadOnlyRepository<T, TKey>:IRepositoryReadOnly<T,TKey>
    {
        protected readonly ISession Session;

        public NHibernateReadOnlyRepository(ISession session)
        {
            Session = session;
        }



        public T get(TKey id)
        {
            return Session.Get<T>(id);
        }

        

        public IEnumerable<T> getAll()
        {
            return Session.Query<T>().ToList();
        }
    }
}