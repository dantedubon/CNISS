using System;
using CNISS.CommonDomain.Ports.Output.Database;
using CNISS.EnterpriseDomain.Domain.Repositories;
using CNISS.EnterpriseDomain.Domain.ValueObjects;
using NHibernate;

namespace CNISS.EnterpriseDomain.Ports.Output
{
    public class DireccionRepositoryCommands:NHibernateCommandRepository<Direccion,Guid>,IDireccionRepositoryCommands
    {
        public DireccionRepositoryCommands(ISession session) : base(session)
        {
        }
    }
}