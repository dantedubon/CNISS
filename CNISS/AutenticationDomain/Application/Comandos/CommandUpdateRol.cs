using System;
using CNISS.AutenticationDomain.Domain.Repositories;
using CNISS.AutenticationDomain.Domain.ValueObjects;
using CNISS.CommonDomain.Application;
using CNISS.CommonDomain.Domain;

namespace CNISS.AutenticationDomain.Application.Comandos
{
    public class CommandUpdateRol:CommandUpdateIdentity<Rol>
    {
        public CommandUpdateRol(IRolRepositoryCommands repository, Func<IUnitOfWork> unitOfWork)
            : base( repository,unitOfWork)
        {
            
        }
     
    }
}