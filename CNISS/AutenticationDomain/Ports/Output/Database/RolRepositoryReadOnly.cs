using System;
using System.Linq;
using CNISS.AutenticationDomain.Domain.Repositories;
using CNISS.AutenticationDomain.Domain.ValueObjects;
using CNISS.CommonDomain.Ports.Output.Database;
using NHibernate;
using NHibernate.Linq;

namespace CNISS.AutenticationDomain.Ports.Output.Database
{
    public class RolRepositoryReadOnly:NHibernateReadOnlyRepository<Rol,Guid>, IRolRepositoryReadOnly
    {
      
        public RolRepositoryReadOnly(ISession session):base(session)
        {
            
        }

     

        public override bool exists(Guid id)
        {

            return Session.Query<Rol>().Where(x => x.Id == id)
                .Select(x => x.Id)
                .Any();
        }
    }
}