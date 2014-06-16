using CNISS.CommonDomain.Ports.Output.Database;
using CNISS.EnterpriseDomain.Domain;
using CNISS.EnterpriseDomain.Domain.Entities;
using CNISS.EnterpriseDomain.Domain.Repositories;
using NHibernate;

namespace CNISS.EnterpriseDomain.Ports.Output
{
    public class GremioRepositoryCommands : NHibernateCommandRepository<Gremio, RTN>, IGremioRespositoryCommands
    {
        public GremioRepositoryCommands(ISession session) : base(session)
        {
        }
    }
}