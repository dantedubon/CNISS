using System.Linq;
using CNISS.CommonDomain.Ports.Output.Database;
using CNISS.EnterpriseDomain.Domain.Entities;
using CNISS.EnterpriseDomain.Domain.Repositories;
using CNISS.EnterpriseDomain.Domain.ValueObjects;
using NHibernate;
using NHibernate.Linq;

namespace CNISS.EnterpriseDomain.Ports.Output.Database
{
    public class RepresentanteLegalRepositoryReadOnly:NHibernateReadOnlyRepository<RepresentanteLegal,Identidad>,
        IRepresentanteLegalRepositoryReadOnly
    {
        public RepresentanteLegalRepositoryReadOnly(ISession session) : base(session)
        {
        }

        public override bool exists(Identidad id)
        {

            return (from representante in Session.Query<RepresentanteLegal>()
                where representante.Id == id
                select representante).Any();
        }
    }
}