using System;
using CNISS.CommonDomain.Ports.Output.Database;
using CNISS.EnterpriseDomain.Domain.ValueObjects;
using NHibernate;

namespace CNISS.EnterpriseDomain.Ports.Output.Database
{
    public class ParentescoRepositoryCommand : NHibernateCommandRepository<Parentesco, Guid>
    {
        public ParentescoRepositoryCommand(ISession session)
            : base(session)
        {
        }
    }
}