using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CNISS.AutenticationDomain.Domain.Services;
using CNISS.CommonDomain.Application;
using CNISS.CommonDomain.Domain;
using CNISS.EnterpriseDomain.Domain.Entities;
using CNISS.EnterpriseDomain.Domain.Repositories;

namespace CNISS.EnterpriseDomain.Application
{
    public class CommandInsertFichaDeSupervision:ICommand<FichaSupervisionEmpleo>
    {
        private readonly IEmpleoRepositoryReadOnly _empleoRepositoryRead;
        private readonly IEmpleoRepositoryCommands _empleoRepositoryCommands;
        private readonly IBeneficiarioRepositoryReadOnly _beneficiarioRepositoryRead;
        private readonly IBeneficiarioRepositoryCommands _beneficiarioRepositoryCommands;
        private readonly IAuthenticateUser _authenticateUser;
        private readonly Func<IUnitOfWork> _unitOfWork;

        public CommandInsertFichaDeSupervision(
            IEmpleoRepositoryReadOnly empleoRepositoryRead, 
            IEmpleoRepositoryCommands empleoRepositoryCommands,
            IBeneficiarioRepositoryReadOnly beneficiarioRepositoryRead,
            IBeneficiarioRepositoryCommands beneficiarioRepositoryCommands,
            IAuthenticateUser authenticateUser
            , Func<IUnitOfWork> unitOfWork)
        {
            _empleoRepositoryRead = empleoRepositoryRead;
            _empleoRepositoryCommands = empleoRepositoryCommands;
            _beneficiarioRepositoryRead = beneficiarioRepositoryRead;
            _beneficiarioRepositoryCommands = beneficiarioRepositoryCommands;
            _authenticateUser = authenticateUser;
            _unitOfWork = unitOfWork;
        }

        public void execute(FichaSupervisionEmpleo ficha, Beneficiario beneficiario, Guid idEmpleo)
        {
            var _uow = _unitOfWork();
            using (_uow)
            {
                _beneficiarioRepositoryCommands.updateInformationFromMovil(beneficiario);
                _empleoRepositoryCommands.updateFromMovilVisitaSupervision(idEmpleo,ficha);
                _uow.commit();
            }
        }

        public void execute(FichaSupervisionEmpleo identity)
        {
            throw new NotImplementedException();
        }

        public bool isExecutable(FichaSupervisionEmpleo identity)
        {
            throw new NotImplementedException();
        }

        public bool isExecutable(FichaSupervisionEmpleo ficha, Beneficiario beneficiario, Guid idEmpleo)
        {
            var nivelUsuarioFirma = 1;
            var userFirma = ficha.firma.user;
            var validUser = _authenticateUser.isValidUser(userFirma, nivelUsuarioFirma);
            return validUser && _beneficiarioRepositoryRead.exists(beneficiario.Id)&& _empleoRepositoryRead.exists(idEmpleo);
        }



        public string message { get; set; }
    }
}