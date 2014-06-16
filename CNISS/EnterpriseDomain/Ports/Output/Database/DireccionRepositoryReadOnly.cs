using System;
using CNISS.CommonDomain.Ports.Output.Database;
using CNISS.EnterpriseDomain.Domain.Repositories;
using CNISS.EnterpriseDomain.Domain.ValueObjects;
using NHibernate;

namespace CNISS.EnterpriseDomain.Ports.Output.Database
{
    public class DireccionRepositoryReadOnly:NHibernateReadOnlyRepository<Direccion,Guid>,IDireccionRepositoryReadOnly
    {
        public DireccionRepositoryReadOnly(ISession session) : base(session)
        {
        }
    }
}