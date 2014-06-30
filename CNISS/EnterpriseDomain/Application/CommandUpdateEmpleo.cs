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
            return false;
        }
    }
}