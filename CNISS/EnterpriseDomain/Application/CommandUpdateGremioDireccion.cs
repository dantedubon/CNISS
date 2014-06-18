using System;
using CNISS.CommonDomain.Application;
using CNISS.CommonDomain.Domain;
using CNISS.EnterpriseDomain.Domain.Entities;
using CNISS.EnterpriseDomain.Domain.Repositories;

namespace CNISS.EnterpriseDomain.Application
{
    public class CommandUpdateGremioDireccion:CommandUpdateIdentity<Gremio>,ICommandUpdateGremioDireccion
    {
        private readonly IServiceDireccionValidator _validatorDireccion;
        private readonly IGremioRepositoryReadOnly _repositoryReadOnly;


        public CommandUpdateGremioDireccion(IServiceDireccionValidator validatorDireccion,
            IGremioRepositoryReadOnly repositoryReadOnly, 
            IGremioRepositoryCommands repository,
            Func<IUnitOfWork> uow) : base(repository,uow)
        {
            _validatorDireccion = validatorDireccion;
            _repositoryReadOnly = repositoryReadOnly;
        }

        public void execute(Gremio identity)
        {
            var gremio = _repositoryReadOnly.get(identity.Id);
            gremio.direccion = identity.direccion;
            base.execute(gremio);
        }

        public bool isExecutable(Gremio identity)
        {

            return _repositoryReadOnly.exists(identity.Id) && _validatorDireccion.isValidDireccion(identity.direccion);
        }
    }
}