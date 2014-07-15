using System;
using CNISS.CommonDomain.Application;
using CNISS.CommonDomain.Domain;
using CNISS.EnterpriseDomain.Domain.Repositories;
using CNISS.EnterpriseDomain.Domain.ValueObjects;

namespace CNISS.EnterpriseDomain.Application
{
    public class CommandUpdateMotivoDespido : CommandUpdateIdentity<MotivoDespido>
    {
        private readonly IMotivoDespidoRepositoryReadOnly _repositoryRead;

        public CommandUpdateMotivoDespido(IMotivoDespidoRepositoryReadOnly repositoryRead,
            IRepositoryCommands<MotivoDespido> repository, Func<IUnitOfWork> unitOfWork) : base(repository, unitOfWork)
        {
            _repositoryRead = repositoryRead;
        }

        public override bool isExecutable(MotivoDespido identity)
        {
            return _repositoryRead.exists(identity.Id);
        }
    }
}