using System;
using CNISS.CommonDomain.Application;
using CNISS.CommonDomain.Domain;
using CNISS.EnterpriseDomain.Domain.Entities;
using CNISS.EnterpriseDomain.Domain.Repositories;

namespace CNISS.EnterpriseDomain.Application
{
    public class CommandUpdateGremioRepresentante:CommandUpdateIdentity<Gremio>
    {
        private readonly IGremioRepositoryReadOnly _repositoryRead;

        private readonly IRepresentanteLegalRepositoryReadOnly _representanteLegalRepositoryRead;

        public CommandUpdateGremioRepresentante(
            IGremioRepositoryReadOnly repositoryRead,
            IRepositoryCommands<Gremio> repository,
            IRepresentanteLegalRepositoryReadOnly representanteLegalRepositoryRead,
            Func<IUnitOfWork> unitOfWork) : base(repository, unitOfWork)
        {
            _repositoryRead = repositoryRead;
           
            _representanteLegalRepositoryRead = representanteLegalRepositoryRead;
        }

        public bool isExecutable(Gremio gremio)
        {
            return _repositoryRead.exists(gremio.Id);
        }

        public override void execute(Gremio identity)
        {
            var gremio = _repositoryRead.get(identity.Id);
            gremio.representanteLegal = identity.representanteLegal;
            base.execute(identity);
        }
    }
}