using System;
using CNISS.CommonDomain.Application;
using CNISS.CommonDomain.Domain;
using CNISS.EnterpriseDomain.Domain.ValueObjects;

namespace CNISS.EnterpriseDomain.Application
{
    public class CommandInsertMotivoDespido : CommandInsertIdentity<MotivoDespido>
    {
        public CommandInsertMotivoDespido(IRepositoryCommands<MotivoDespido> repository, Func<IUnitOfWork> unitOfWork)
            : base(repository, unitOfWork)
        {
        }
    }
}