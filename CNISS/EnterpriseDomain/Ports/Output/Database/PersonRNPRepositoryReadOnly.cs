using CNISS.CommonDomain.Ports.Output.Database;
using CNISS.EnterpriseDomain.Domain.Entities;
using CNISS.EnterpriseDomain.Domain.Repositories;
using NHibernate;

namespace CNISS.EnterpriseDomain.Ports.Output.Database
{
    public class PersonRNPRepositoryReadOnly : NHibernateReadOnlyRepository<PersonRNP, string>,
        IPersonRNPRepositoryReadOnly
    {
        public PersonRNPRepositoryReadOnly(ISession session) : base(session)
        {
        }
    }
}