using System;
using CNISS.CommonDomain.Application;
using CNISS.CommonDomain.Domain;
using CNISS.EnterpriseDomain.Domain.Entities;
using CNISS.EnterpriseDomain.Domain.Repositories;

namespace CNISS.EnterpriseDomain.Application
{
    public class CommandUpdateGremioRepresentante : CommandUpdateIdentity<Gremio>, ICommandUpdateGremioRepresentante
    {
        private readonly IGremioRepositoryReadOnly _repositoryRead;
        private readonly IGremioRepositoryCommands _repositoryCommands;

        private readonly IRepresentanteLegalRepositoryReadOnly _representanteLegalRepositoryRead;

        public CommandUpdateGremioRepresentante(
            IGremioRepositoryReadOnly repositoryRead,
            IGremioRepositoryCommands repository,
            IRepresentanteLegalRepositoryReadOnly representanteLegalRepositoryRead,
            Func<IUnitOfWork> unitOfWork) : base(repository, unitOfWork)
        {
            _repositoryRead = repositoryRead;
           
            _representanteLegalRepositoryRead = representanteLegalRepositoryRead;
            _repositoryCommands = repository;
        }

        public bool isExecutable(Gremio gremio)
        {
            return _repositoryRead.exists(gremio.Id);
        }

        public override void execute(Gremio identity)
        {
            var gremio = _repositoryRead.get(identity.Id);
            gremio.representanteLegal = identity.representanteLegal;

            var _uow = _factory();

            using (_uow)
            {
                _repositoryCommands.updateRepresentante(gremio);
                _uow.commit();
            }
           
        }
    }
}