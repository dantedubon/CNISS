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
    public class CommandInsertGremio:CommandInsertIdentity<Gremio>
    {
        private IServiceDireccionValidator validatorDireccion;
        public CommandInsertGremio(IServiceDireccionValidator validatorDireccion,
            IGremioRespositoryCommands repository, 
            Func<IUnitOfWork> unitOfWork) : base(repository, unitOfWork)
        {
            this.validatorDireccion = validatorDireccion;
        }

        public override void execute(Gremio gremio)
        {
            
            if(!validatorDireccion.isValidDireccion(gremio.direccion))
                throw new ArgumentException("Direccion mala");
            
            base.execute(gremio);
        }
    }
}