using System.Collections.Generic;
using CNISS.EnterpriseDomain.Domain.Entities;

namespace CNISS.EnterpriseDomain.Application
{
    public interface IServiceSucursalesValidator
    {
        bool isValid(IEnumerable<Sucursal> sucursales);
    }
}