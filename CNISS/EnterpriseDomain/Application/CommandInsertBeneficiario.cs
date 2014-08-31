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
    public class CommandInsertBeneficiario:CommandInsertIdentity<Beneficiario>
    {
        private readonly IBeneficiarioRepositoryReadOnly _repositoryReadBeneficiario;
        private readonly IParentescoReadOnlyRepository _repositoryReadParentesco;


        public CommandInsertBeneficiario(IBeneficiarioRepositoryReadOnly repositoryReadBeneficiario, 
            IParentescoReadOnlyRepository repositoryReadParentesco, 
            IBeneficiarioRepositoryCommands repositoryCommandBeneficiario,
            Func<IUnitOfWork> uow) : base(repositoryCommandBeneficiario, uow)
        {
            _repositoryReadBeneficiario = repositoryReadBeneficiario;
            _repositoryReadParentesco = repositoryReadParentesco;
          
        }

        public override bool isExecutable(Beneficiario identity)
        {
            return !_repositoryReadBeneficiario.exists(identity.Id) && identity.Dependientes.All(x => existsParentesco(x.Parentesco));
        }

        private bool existsParentesco(Parentesco parentesco)
        {
            return _repositoryReadParentesco.exists(parentesco.Id);
        }
    }
}