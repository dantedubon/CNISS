using System.Linq;
using CNISS.CommonDomain.Ports.Output.Database;
using CNISS.EnterpriseDomain.Domain;
using CNISS.EnterpriseDomain.Domain.Entities;
using CNISS.EnterpriseDomain.Domain.Repositories;
using NHibernate;
using NHibernate.Linq;

namespace CNISS.EnterpriseDomain.Ports.Output.Database
{
    public class GremioRepositoryReadOnly:NHibernateReadOnlyRepository<Gremio,RTN>,IGremioRepositoryReadOnly
    {
        public GremioRepositoryReadOnly(ISession session) : base(session)
        {
        }

        public bool exists(RTN id)
        {
            return (from gremio in Session.Query<Gremio>() 
                    where gremio.Id == id
                    select gremio.Id).Any();
                
            

        }
    }
}