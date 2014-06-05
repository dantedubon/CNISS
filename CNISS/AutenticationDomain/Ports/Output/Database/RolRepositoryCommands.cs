using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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