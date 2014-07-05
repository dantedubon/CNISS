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
    public class CommandUpdateActividadEconomica:CommandUpdateIdentity<ActividadEconomica>
    {
        private readonly IActividadEconomicaRepositoryReadOnly _repositoryRead;

        public CommandUpdateActividadEconomica(IActividadEconomicaRepositoryReadOnly repositoryRead,
            IRepositoryCommands<ActividadEconomica> repository, Func<IUnitOfWork> unitOfWork) : base(repository, unitOfWork)
        {
            _repositoryRead = repositoryRead;
        }

        public override bool isExecutable(ActividadEconomica identity)
        {
            return _repositoryRead.exists(identity.Id);
        }
    }
}