using CNISS.AutenticationDomain.Domain.Entities;
using CNISS.AutenticationDomain.Domain.Repositories;
using CNISS.CommonDomain.Ports.Output.Database;
using NHibernate;

namespace CNISS.AutenticationDomain.Ports.Output.Database
{
    public class UserRepositoryCommands : NHibernateCommandRepository<User, string>, IUserRepositoryCommands
    {
        public UserRepositoryCommands(ISession session)
            : base(session)
        {

        }


    }
}