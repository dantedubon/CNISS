using System;
using CNISS.CommonDomain.Application;
using CNISS.CommonDomain.Domain;
using CNISS.EnterpriseDomain.Domain.Entities;
using CNISS.EnterpriseDomain.Domain.Repositories;

namespace CNISS.EnterpriseDomain.Application
{
    public class CommandInsertEmpresa:CommandInsertIdentity<Empresa>
    {
        private readonly IServiceSucursalesValidator _validadorSucursales;
        private readonly IGremioRepositoryReadOnly _repositoryGremiosRead;
        private readonly IActividadEconomicaRepositoryReadOnly _repositoryActividadesRead;
        private readonly IEmpresaRepositoryReadOnly _repositoryRead;
      
      

        public CommandInsertEmpresa(IServiceSucursalesValidator validadorSucursales, 
            IGremioRepositoryReadOnly repositoryGremiosRead, 
            IActividadEconomicaRepositoryReadOnly repositoryActividadesRead, 
            IEmpresaRepositoryReadOnly repositoryRead,
            IEmpresaRepositoryCommands repositoryCommand, Func<IUnitOfWork> uof) : base(repositoryCommand, uof)
        {
            _validadorSucursales = validadorSucursales;
            _repositoryGremiosRead = repositoryGremiosRead;
            _repositoryActividadesRead = repositoryActividadesRead;
            _repositoryRead = repositoryRead;
        }

        public override bool isExecutable(Empresa identity)
        {

            return !_repositoryRead.exists(identity.Id) 
                && _repositoryActividadesRead.existsAll(identity.ActividadesEconomicas)
                && _repositoryGremiosRead.exists(identity.Gremial.Id)
               
                && _validadorSucursales.isValid(identity.Sucursales);
        }
    }
}