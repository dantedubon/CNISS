using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CNISS.AutenticationDomain.Domain.Repositories;
using CNISS.AutenticationDomain.Domain.ValueObjects;
using CNISS.CommonDomain.Application;
using CNISS.CommonDomain.Domain;

namespace CNISS.AutenticationDomain.Application.Comandos
{
    public class CommandDeleteRol:CommandDeleteIdentity<Rol>
    {
        public CommandDeleteRol(IRolRepositoryCommands repository, Func<IUnitOfWork> unitOfWork) 
            : base(repository, unitOfWork)
        {
        }
    }
}