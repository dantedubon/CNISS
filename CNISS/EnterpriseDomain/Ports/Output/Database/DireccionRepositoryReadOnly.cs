using System;
using System.Linq;
using CNISS.CommonDomain.Ports.Output.Database;
using CNISS.EnterpriseDomain.Domain.Repositories;
using CNISS.EnterpriseDomain.Domain.ValueObjects;
using NHibernate;
using NHibernate.Hql.Ast.ANTLR;
using NHibernate.Linq;

namespace CNISS.EnterpriseDomain.Ports.Output.Database
{
    public class DireccionRepositoryReadOnly:NHibernateReadOnlyRepository<Direccion,Guid>,IDireccionRepositoryReadOnly
    {
        public DireccionRepositoryReadOnly(ISession session) : base(session)
        {
        }

        public override bool exists(Guid id)
        {
            return (from direccion in Session.Query<Direccion>()
                where direccion.Id == id
                select direccion.Id
                ).Any();
        }
    }
}