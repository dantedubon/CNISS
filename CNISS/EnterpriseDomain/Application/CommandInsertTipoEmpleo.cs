using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CNISS.CommonDomain.Application;
using CNISS.CommonDomain.Domain;
using CNISS.EnterpriseDomain.Domain.ValueObjects;

namespace CNISS.EnterpriseDomain.Application
{
    public class CommandInsertTipoEmpleo:CommandInsertIdentity<TipoEmpleo>
    {
        public CommandInsertTipoEmpleo(IRepositoryCommands<TipoEmpleo> repository, Func<IUnitOfWork> unitOfWork) : base(repository, unitOfWork)
        {
        }
    }
}