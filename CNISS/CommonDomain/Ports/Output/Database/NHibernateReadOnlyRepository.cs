using System.Collections.Generic;
using System.Linq;
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

        

        public virtual IEnumerable<T> getAll()
        {
            return Session.Query<T>().ToList();
        }

        public virtual bool exists(TKey id)
        {
            throw new System.NotImplementedException();
        }
    }
}