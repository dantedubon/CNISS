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
    public class CommandUpdateEmpresa:CommandUpdateIdentity<Empresa>
    {
        private readonly IServiceSucursalesValidator _validadorSucursales;
        private readonly IGremioRepositoryReadOnly _repositoryGremiosRead;
        private readonly IActividadEconomicaRepositoryReadOnly _repositoryActividadesRead;
        private readonly IEmpresaRepositoryReadOnly _repositoryReadOnly;


        public CommandUpdateEmpresa(IServiceSucursalesValidator validadorSucursales,
            IGremioRepositoryReadOnly repositoryGremiosRead, 
            IActividadEconomicaRepositoryReadOnly repositoryActividadesRead, 
            IEmpresaRepositoryReadOnly repositoryReadOnly,
           IEmpresaRepositoryCommands repositoryCommand, 
            Func<IUnitOfWork> uof)
            : base(repositoryCommand, uof)
        {
            _validadorSucursales = validadorSucursales;
            _repositoryGremiosRead = repositoryGremiosRead;
            _repositoryActividadesRead = repositoryActividadesRead;
            _repositoryReadOnly = repositoryReadOnly;
           
        }

        public override bool isExecutable(Empresa identity)
        {
            return _repositoryReadOnly.exists(identity.Id)
                && _repositoryActividadesRead.existsAll(identity.ActividadesEconomicas)
                && _repositoryGremiosRead.exists(identity.Gremial.Id)
            && _validadorSucursales.isValid(identity.Sucursales);
        }
    }
}