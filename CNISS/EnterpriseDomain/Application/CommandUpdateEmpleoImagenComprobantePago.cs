using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CNISS.CommonDomain.Application;
using CNISS.CommonDomain.Domain;
using CNISS.EnterpriseDomain.Domain.Entities;
using CNISS.EnterpriseDomain.Domain.Repositories;
using CNISS.EnterpriseDomain.Domain.ValueObjects;

namespace CNISS.EnterpriseDomain.Application
{
    public class CommandUpdateEmpleoImagenComprobantePago:CommandUpdateIdentity<Empleo>,ICommandUpdateEmpleoImagenComprobantePago
    {
        private readonly IEmpleoRepositoryReadOnly _repositoryRead;
        private readonly IEmpleoRepositoryCommands _repositoryCommands;

        public CommandUpdateEmpleoImagenComprobantePago( IEmpleoRepositoryReadOnly repositoryRead,IEmpleoRepositoryCommands repository, Func<IUnitOfWork> unitOfWork) : base(repository, unitOfWork)
        {
            _repositoryRead = repositoryRead;
            _repositoryCommands = repository;
        }


        public bool isExecutable(Guid empleoid, Guid comprobanteId)
        {
            return  _repositoryRead.existsComprobante(empleoid,comprobanteId);
        }

        public void execute(Guid empleoid, Guid comprobanteId, ContentFile contentFile)
        {
            var _uow = _factory();
            using (_uow)
            {
                _repositoryCommands.updateImagenComprobante(empleoid,comprobanteId,contentFile);
                _uow.commit();
            }
        }
    }
}