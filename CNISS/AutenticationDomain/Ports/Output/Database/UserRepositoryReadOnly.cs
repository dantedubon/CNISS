using System.Linq;
using CNISS.AutenticationDomain.Domain.Entities;
using CNISS.AutenticationDomain.Domain.Repositories;
using CNISS.CommonDomain.Ports.Output.Database;
using NHibernate;
using NHibernate.Linq;

namespace CNISS.AutenticationDomain.Ports.Output.Database
{
    public class UserRepositoryReadOnly : NHibernateReadOnlyRepository<User, string>, IUserRepositoryReadOnly
    {

        public UserRepositoryReadOnly(ISession session)
            : base(session)
        {

        }



        public override bool exists(string id)
        {

            return Session.Query<User>().Where(x => x.Id == id)
                .Select(x => x.Id)
                .Any();
        }

      
    }
}