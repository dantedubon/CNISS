using CNISS.CommonDomain.Ports.Output.Database;
using CNISS.EnterpriseDomain.Domain.Entities;
using CNISS.EnterpriseDomain.Domain.Repositories;
using CNISS.EnterpriseDomain.Domain.ValueObjects;
using NHibernate;

namespace CNISS.EnterpriseDomain.Ports.Output
{
    public class RepresentanteLegalRepositoryCommands : NHibernateCommandRepository<RepresentanteLegal, Identidad>,
        IRepresentanteLegalRepositoryCommands
    {
        public RepresentanteLegalRepositoryCommands(ISession session) : base(session)
        {
        }
    }
}