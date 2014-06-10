using System;
using CNISS.AutenticationDomain.Domain.Entities;
using CNISS.AutenticationDomain.Domain.Repositories;
using CNISS.CommonDomain.Application;
using CNISS.CommonDomain.Domain;

namespace CNISS.AutenticationDomain.Application.Comandos
{
    public class CommandDeleteUser : CommandDeleteIdentity<User>
    {
        public CommandDeleteUser(IUserRepositoryCommands repository, Func<IUnitOfWork> unitOfWork)
            : base(repository, unitOfWork)
        {
        }
    }
}