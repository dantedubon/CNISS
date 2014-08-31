using System.Collections.Generic;
using System.Linq;
using CNISS.AutenticationDomain.Domain.Repositories;
using CNISS.EnterpriseDomain.Domain.Entities;

namespace CNISS.EnterpriseDomain.Application
{
    public class ServiceSucursalesValidator:IServiceSucursalesValidator
    {
        
        private readonly IServiceDireccionValidator _direccionValidator;
        private readonly IUserRepositoryReadOnly _userRepository;

        public ServiceSucursalesValidator(
            IServiceDireccionValidator direccionValidator, 
            IUserRepositoryReadOnly userRepository
            )
        {
            
            _direccionValidator = direccionValidator;
            _userRepository = userRepository;
        }


        public bool isValid(IEnumerable<Sucursal> sucursales)
        {
            var enumerable = sucursales as Sucursal[] ?? sucursales.ToArray();
            return enumerable.All(x => _direccionValidator.isValidDireccion(x.Direccion))
                   && enumerable.All(x => _userRepository.exists(x.Firma.User.Id));
        }
    }
}