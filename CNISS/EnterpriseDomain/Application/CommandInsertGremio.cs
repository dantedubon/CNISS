using System;
using CNISS.CommonDomain.Application;
using CNISS.CommonDomain.Domain;
using CNISS.EnterpriseDomain.Domain.Entities;
using CNISS.EnterpriseDomain.Domain.Repositories;

namespace CNISS.EnterpriseDomain.Application
{
    public class CommandInsertGremio:CommandInsertIdentity<Gremio>
    {
        private IServiceDireccionValidator validatorDireccion;
        private readonly IGremioRepositoryReadOnly _repositoryReadOnly;
     

        public CommandInsertGremio(IServiceDireccionValidator validatorDireccion,
            IGremioRepositoryReadOnly repositoryReadOnly,
            IGremioRepositoryCommands repository, 
            Func<IUnitOfWork> unitOfWork) : base(repository, unitOfWork)
        {
            this.validatorDireccion = validatorDireccion;
            _repositoryReadOnly = repositoryReadOnly;
          
        }

        public override void execute(Gremio gremio)
        {
            
            if(!validatorDireccion.isValidDireccion(gremio.Direccion))
                throw new ArgumentException("Direccion mala");
            
            base.execute(gremio);
        }

        public override bool isExecutable(Gremio identity)
        {
            return !_repositoryReadOnly.exists(identity.Id);
        }
    }
}