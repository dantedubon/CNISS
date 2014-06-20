using System;
using CNISS.CommonDomain.Ports.Output.Database;
using CNISS.EnterpriseDomain.Domain.Repositories;
using CNISS.EnterpriseDomain.Domain.ValueObjects;
using NHibernate;

namespace CNISS.EnterpriseDomain.Ports.Output.Database
{
    public class ActividadEconomicaRepositoryReadOnly :NHibernateReadOnlyRepository<ActividadEconomica,Guid>, IActividadEconomicaRepositoryReadOnly
    {
        public ActividadEconomicaRepositoryReadOnly(ISession session) : base(session)
        {
        }
    }
}