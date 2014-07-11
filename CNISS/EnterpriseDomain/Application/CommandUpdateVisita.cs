using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CNISS.CommonDomain.Application;
using CNISS.CommonDomain.Domain;
using CNISS.EnterpriseDomain.Domain.Entities;
using CNISS.EnterpriseDomain.Domain.Repositories;

namespace CNISS.EnterpriseDomain.Application
{
    public class CommandUpdateVisita : CommandUpdateIdentity<Visita>
    {
        private readonly IVisitaRepositoryReadOnly _repositoryRead;

        public CommandUpdateVisita(IVisitaRepositoryReadOnly repositoryRead,
            IRepositoryCommands<Visita> repository, Func<IUnitOfWork> unitOfWork) : base(repository, unitOfWork)
        {
            _repositoryRead = repositoryRead;
        }

        public override bool isExecutable(Visita identity)
        {
            return _repositoryRead.exists(identity.Id) && identity.fechaInicial < identity.fechaFinal;
        }
    }
}