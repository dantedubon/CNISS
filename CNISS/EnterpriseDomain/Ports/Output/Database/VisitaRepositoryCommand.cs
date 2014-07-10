using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CNISS.CommonDomain.Ports.Output.Database;
using CNISS.EnterpriseDomain.Domain.Entities;
using CNISS.EnterpriseDomain.Domain.Repositories;
using NHibernate;

namespace CNISS.EnterpriseDomain.Ports.Output.Database
{
    public class VisitaRepositoryCommand:NHibernateCommandRepository<Visita,Guid>,IVisitaRepositoryCommand
    {
        public VisitaRepositoryCommand(ISession session) : base(session)
        {
        }
    }
}