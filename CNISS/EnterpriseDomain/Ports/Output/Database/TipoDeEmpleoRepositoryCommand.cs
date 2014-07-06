using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CNISS.CommonDomain.Ports.Output.Database;
using CNISS.EnterpriseDomain.Domain.ValueObjects;
using NHibernate;

namespace CNISS.EnterpriseDomain.Ports.Output.Database
{
    public class TipoDeEmpleoRepositoryCommand:NHibernateCommandRepository<TipoEmpleo,Guid>
    {
        public TipoDeEmpleoRepositoryCommand(ISession session) : base(session)
        {
        }
    }
}