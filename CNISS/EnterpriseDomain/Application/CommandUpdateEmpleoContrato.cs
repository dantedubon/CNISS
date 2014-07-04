using System;
using CNISS.CommonDomain.Application;
using CNISS.CommonDomain.Domain;
using CNISS.EnterpriseDomain.Domain.Entities;
using CNISS.EnterpriseDomain.Domain.Repositories;
using CNISS.EnterpriseDomain.Domain.ValueObjects;

namespace CNISS.EnterpriseDomain.Application
{
    public class CommandUpdateEmpleoContrato:CommandUpdateIdentity<Empleo>,ICommandUpdateEmpleoContrato
    {
        private readonly IEmpleoRepositoryReadOnly _repositoryRead;
        private readonly IEmpleoRepositoryCommands _repositoryCommands;

        public CommandUpdateEmpleoContrato(IEmpleoRepositoryReadOnly repositoryRead, IEmpleoRepositoryCommands repository, Func<IUnitOfWork> unitOfWork) : base(repository, unitOfWork)
        {
            _repositoryRead = repositoryRead;
            _repositoryCommands = repository;
        }

        public void execute(Guid idEmpleo, ContentFile contrato)
        {
            var _uow = _factory();
            using (_uow)
            {
                _repositoryCommands.updateContratoEmpleo(idEmpleo, contrato);
                _uow.commit();
            }
        }

        public bool isExecutable(Guid idEmpleo)
        {
            return _repositoryRead.exists(idEmpleo);
        }
    }
}