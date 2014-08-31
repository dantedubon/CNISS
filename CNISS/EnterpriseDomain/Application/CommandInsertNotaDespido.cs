using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CNISS.AutenticationDomain.Domain.Services;
using CNISS.CommonDomain.Domain;
using CNISS.EnterpriseDomain.Domain.Entities;
using CNISS.EnterpriseDomain.Domain.Repositories;

namespace CNISS.EnterpriseDomain.Application
{
    public class CommandInsertNotaDespido:ICommandInsertNotaDespido
    {
        private readonly IEmpleoRepositoryReadOnly _empleoRepositoryRead;
        private readonly IEmpleoRepositoryCommands _empleoRepositoryCommands;
        private readonly IAuthenticateUser _authenticateUser;
        private readonly Func<IUnitOfWork> _unitOfWork;

        public CommandInsertNotaDespido(IEmpleoRepositoryReadOnly empleoRepositoryRead, IEmpleoRepositoryCommands empleoRepositoryCommands,
            IAuthenticateUser authenticateUser
            , Func<IUnitOfWork> unitOfWork)
        {
            _empleoRepositoryRead = empleoRepositoryRead;
            _empleoRepositoryCommands = empleoRepositoryCommands;
            _authenticateUser = authenticateUser;
            _unitOfWork = unitOfWork;
        }

        public bool isExecutable(Guid idEmpleo, NotaDespido notaDespido)
        {
            var fechaNotaDespido = notaDespido.FechaDespido;
            var userFirma = notaDespido.FirmaAutorizada.User;
            const int nivelFirma = 1;
            return _authenticateUser.isValidUser(userFirma,nivelFirma) 
                && _empleoRepositoryRead.existsEmpleoForNotaDespido(idEmpleo, fechaNotaDespido);
        }

        public void execute(Guid idEmpleo, NotaDespido notaDespido)
        {

            var _uow = _unitOfWork();
            using (_uow)
            {
                _empleoRepositoryCommands.updateFromMovilNotaDespido(idEmpleo,notaDespido);
                _uow.commit();
            }
            
        }
    }
}