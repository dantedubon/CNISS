using System;
using CNISS.CommonDomain.Ports.Output.Database;
using CNISS.EnterpriseDomain.Domain.Repositories;
using CNISS.EnterpriseDomain.Domain.ValueObjects;
using NHibernate;

namespace CNISS.EnterpriseDomain.Ports.Output.Database
{
    public class MotivoDespidoRepositoryCommands : NHibernateCommandRepository<MotivoDespido, Guid>, IMotivoDespidoRepositoryCommands
    {
        public MotivoDespidoRepositoryCommands(ISession session)
            : base(session)
        {
        }


    }
}