using System;
using System.Collections.Generic;
using CNISS.AutenticationDomain.Domain.Repositories;
using CNISS.AutenticationDomain.Domain.ValueObjects;
using CNISS.CommonDomain.Ports.Output.Database;
using NHibernate;

namespace CNISS.AutenticationDomain.Ports.Output.Database
{
    public class RolRepositoryReadOnly:NHibernateReadOnlyRepository<Rol,Guid>,IRolRepositoryReadOnly
    {
        private ISession _session;
        public RolRepositoryReadOnly(ISession session):base(session)
        {
            _session = session;
        }
      

       

       
    }
}