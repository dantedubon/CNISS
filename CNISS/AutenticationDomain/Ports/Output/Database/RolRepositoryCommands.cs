using System;
using System.Security.Cryptography.X509Certificates;
using CNISS.AutenticationDomain.Domain.Repositories;
using CNISS.AutenticationDomain.Domain.ValueObjects;
using CNISS.CommonDomain.Ports.Output.Database;
using NHibernate;


namespace CNISS.AutenticationDomain.Ports.Output.Database
{
    public class RolRepositoryCommands: NHibernateCommandRepository<Rol,Guid>, IRolRepositoryCommands
    {
        public RolRepositoryCommands(ISession session):base(session)
        {
            
        }

      
    }
}