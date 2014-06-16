using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CNISS.CommonDomain.Application;
using CNISS.CommonDomain.Domain;
using CNISS.EnterpriseDomain.Domain.Entities;

namespace CNISS.EnterpriseDomain.Application
{
    public class CommandInsertGremio:CommandInsertIdentity<Gremio>
    {
        public CommandInsertGremio(IRepositoryCommands<Gremio> repository, Func<IUnitOfWork> unitOfWork) : base(repository, unitOfWork)
        {
        }
    }
}