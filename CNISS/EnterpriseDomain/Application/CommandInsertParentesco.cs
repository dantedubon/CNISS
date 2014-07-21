using System;
using CNISS.CommonDomain.Application;
using CNISS.CommonDomain.Domain;
using CNISS.EnterpriseDomain.Domain.ValueObjects;

namespace CNISS.EnterpriseDomain.Application
{
    public class CommandInsertParentesco : CommandInsertIdentity<Parentesco>
    {
        public CommandInsertParentesco(IRepositoryCommands<Parentesco> repository, Func<IUnitOfWork> unitOfWork)
            : base(repository, unitOfWork)
        {
        }
    }
}