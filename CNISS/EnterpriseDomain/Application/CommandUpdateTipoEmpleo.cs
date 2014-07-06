using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CNISS.CommonDomain.Application;
using CNISS.CommonDomain.Domain;
using CNISS.EnterpriseDomain.Domain.Repositories;
using CNISS.EnterpriseDomain.Domain.ValueObjects;

namespace CNISS.EnterpriseDomain.Application
{
    public class CommandUpdateTipoEmpleo:CommandUpdateIdentity<TipoEmpleo>
    {
        private readonly ITipoDeEmpleoReadOnlyRepository _repositoryRead;

        public CommandUpdateTipoEmpleo(ITipoDeEmpleoReadOnlyRepository repositoryRead,
            IRepositoryCommands<TipoEmpleo> repository, Func<IUnitOfWork> unitOfWork) : base(repository, unitOfWork)
        {
            _repositoryRead = repositoryRead;
        }

        public override bool isExecutable(TipoEmpleo identity)
        {
            return _repositoryRead.exists(identity.Id);
        }
    }
}