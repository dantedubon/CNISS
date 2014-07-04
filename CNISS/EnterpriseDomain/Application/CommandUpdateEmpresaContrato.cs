using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CNISS.CommonDomain.Application;
using CNISS.CommonDomain.Domain;
using CNISS.EnterpriseDomain.Domain;
using CNISS.EnterpriseDomain.Domain.Entities;
using CNISS.EnterpriseDomain.Domain.Repositories;
using CNISS.EnterpriseDomain.Domain.ValueObjects;

namespace CNISS.EnterpriseDomain.Application
{
    public class CommandUpdateEmpresaContrato:CommandUpdateIdentity<Empresa>, ICommandUpdateEmpresaContrato
    {
        private readonly IEmpresaRepositoryReadOnly _repositoryRead;
        private readonly IEmpresaRepositoryCommands _repositoryCommands;

        public CommandUpdateEmpresaContrato(IEmpresaRepositoryReadOnly repositoryRead,  IEmpresaRepositoryCommands repository, Func<IUnitOfWork> unitOfWork) : base(repository, unitOfWork)
        {
            _repositoryRead = repositoryRead;
            _repositoryCommands = repository;
        }

        public bool isExecutable(RTN empresa)
        {
            return _repositoryRead.exists(empresa);
        }

        public void execute(RTN empresa, ContentFile contentFile)
        {
             var _uow = _factory();
            using (_uow)
            {
                _repositoryCommands.updateContrato(empresa,contentFile);
                _uow.commit();
            }
        }
    }
}