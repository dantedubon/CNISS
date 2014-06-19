using System;
using CNISS.CommonDomain.Application;
using CNISS.CommonDomain.Domain;
using CNISS.EnterpriseDomain.Domain;
using CNISS.EnterpriseDomain.Domain.Entities;
using CNISS.EnterpriseDomain.Domain.Repositories;

namespace CNISS.EnterpriseDomain.Application
{
    public class CommandDeleteGremio:CommandDeleteIdentity<Gremio>,ICommandDeleteGremio
    {
        private readonly IGremioRepositoryReadOnly _repositoryRead;

        public CommandDeleteGremio(IGremioRepositoryReadOnly repositoryRead, IGremioRepositoryCommands repository, Func<IUnitOfWork> uow) : base(repository,uow)
        {
            _repositoryRead = repositoryRead;
            
        }

        public bool isExecutable(RTN rtn)
        {
            return _repositoryRead.exists(rtn);
        }

        public void execute(RTN identity)
        {
            var gremio = _repositoryRead.get(identity);
            if (gremio != null) execute(gremio);
        }
    }
}