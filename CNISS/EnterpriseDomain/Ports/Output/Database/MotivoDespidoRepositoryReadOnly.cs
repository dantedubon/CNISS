using System;
using System.Linq;
using CNISS.CommonDomain.Ports.Output.Database;
using CNISS.EnterpriseDomain.Domain.Repositories;
using CNISS.EnterpriseDomain.Domain.ValueObjects;
using NHibernate;
using NHibernate.Linq;

namespace CNISS.EnterpriseDomain.Ports.Output.Database
{
    public class MotivoDespidoRepositoryReadOnly : NHibernateReadOnlyRepository<MotivoDespido, Guid>, IMotivoDespidoRepositoryReadOnly
    {
        public MotivoDespidoRepositoryReadOnly(ISession session)
            : base(session)
        {
        }


     

        public override bool exists(Guid id)
        {
            return (from motivo in Session.Query<MotivoDespido>()
                where motivo.Id == id
                select motivo.Id
                ).Any()
                ;
        }
    }
}