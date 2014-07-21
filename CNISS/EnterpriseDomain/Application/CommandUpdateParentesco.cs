using System;
using CNISS.CommonDomain.Application;
using CNISS.CommonDomain.Domain;
using CNISS.EnterpriseDomain.Domain.Repositories;
using CNISS.EnterpriseDomain.Domain.ValueObjects;

namespace CNISS.EnterpriseDomain.Application
{
    public class CommandUpdateParentesco : CommandUpdateIdentity<Parentesco>
    {
        private readonly IParentescoReadOnlyRepository _repositoryRead;

        public CommandUpdateParentesco(IParentescoReadOnlyRepository repositoryRead,
            IRepositoryCommands<Parentesco> repository, Func<IUnitOfWork> unitOfWork)
            : base(repository, unitOfWork)
        {
            _repositoryRead = repositoryRead;
        }

        public override bool isExecutable(Parentesco identity)
        {
            return _repositoryRead.exists(identity.Id);
        }
    }
}