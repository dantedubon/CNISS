using CNISS.CommonDomain.Ports.Output.Database;
using CNISS.EnterpriseDomain.Domain;
using CNISS.EnterpriseDomain.Domain.Entities;
using CNISS.EnterpriseDomain.Domain.Repositories;
using NHibernate;

namespace CNISS.EnterpriseDomain.Ports.Output.Database
{
    public class GremioRepositoryReadOnly:NHibernateReadOnlyRepository<Gremio,RTN>,IGremioRepositoryReadOnly
    {
        public GremioRepositoryReadOnly(ISession session) : base(session)
        {
        }
    }
}