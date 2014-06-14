using System.Collections.Generic;
using CNISS.CommonDomain.Domain;
using NHibernate;

namespace CNISS.CommonDomain.Ports.Output.Database
{
    public abstract  class NHibernateReadOnlyRepositoryCompoundKey<T,Tkey1,Tkey2>:IRepositoryReadOnlyCompoundKey<T,Tkey1,Tkey2>
    {
        protected readonly ISession Session;

        public NHibernateReadOnlyRepositoryCompoundKey(ISession session)
        {
            Session = session;
        }


        public abstract  T get(Tkey1 id1, Tkey2 id2);

        public abstract  IEnumerable<T> getAll();

        public abstract  bool exists(Tkey1 id1, Tkey2 id2);
    }
}