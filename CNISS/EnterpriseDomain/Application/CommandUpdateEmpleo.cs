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
    public class CommandUpdateEmpleo:CommandUpdateIdentity<Empleo>
    {
        private readonly IEmpleoRepositoryReadOnly _repositoryRead;
        private readonly IBeneficiarioRepositoryReadOnly _beneficiarioRepositoryRead;
        private readonly IProvideAllowedDaysForNewEmpleo _providerDays;
        private readonly IEmpresaRepositoryReadOnly _empresaRepositoryRead;
        private readonly ITipoDeEmpleoReadOnlyRepository _tipoDeEmpleoReadOnlyRepository;

        public CommandUpdateEmpleo(IRepositoryCommands<Empleo> repository, 
            Func<IUnitOfWork> unitOfWork, IEmpleoRepositoryReadOnly repositoryRead, 
            IBeneficiarioRepositoryReadOnly beneficiarioRepositoryRead,
            IProvideAllowedDaysForNewEmpleo providerDays,
            IEmpresaRepositoryReadOnly empresaRepositoryRead,
            ITipoDeEmpleoReadOnlyRepository tipoDeEmpleoReadOnlyRepository) : base(repository, unitOfWork)
        {
            _repositoryRead = repositoryRead;
            _beneficiarioRepositoryRead = beneficiarioRepositoryRead;
            _providerDays = providerDays;
            _empresaRepositoryRead = empresaRepositoryRead;
            _tipoDeEmpleoReadOnlyRepository = tipoDeEmpleoReadOnlyRepository;
        }


        public override bool isExecutable(Empleo identity)
        {
            var existeEmpleo = _repositoryRead.exists(identity.Id);
            var days = _providerDays.getDays();
            var empleoReciente = !_repositoryRead.existsEmpleoRecienteParaBeneficiario(identity.Id,identity.FechaDeInicio, days,
                identity.Beneficiario.Id);

            var beneficiarioExiste = _beneficiarioRepositoryRead.exists(identity.Beneficiario.Id);
            var empleoExiste = _empresaRepositoryRead.exists(identity.Empresa.Id);
            var tipoEmpleoExiste = _tipoDeEmpleoReadOnlyRepository.exists(identity.TipoEmpleo.Id);

            return existeEmpleo && empleoReciente && beneficiarioExiste&&empleoExiste&&tipoEmpleoExiste;
        }
    }
}