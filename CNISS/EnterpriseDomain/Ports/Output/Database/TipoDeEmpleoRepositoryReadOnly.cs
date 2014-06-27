using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CNISS.CommonDomain.Ports.Output.Database;
using CNISS.EnterpriseDomain.Domain.Repositories;
using CNISS.EnterpriseDomain.Domain.ValueObjects;
using NHibernate;

namespace CNISS.EnterpriseDomain.Ports.Output.Database
{
    public class TipoDeEmpleoRepositoryReadOnly:NHibernateReadOnlyRepository<TipoEmpleo,Guid>,ITipoDeEmpleoReadOnlyRepository
    {
        public TipoDeEmpleoRepositoryReadOnly(ISession session) : base(session)
        {
        }
    }
}