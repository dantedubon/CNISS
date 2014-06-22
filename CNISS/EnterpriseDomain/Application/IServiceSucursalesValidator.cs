using System.Collections.Generic;
using CNISS.EnterpriseDomain.Domain.Entities;
using CNISS.EnterpriseDomain.Domain.ValueObjects;

namespace CNISS.EnterpriseDomain.Application
{
    public interface IServiceSucursalesValidator
    {
        bool isValid(IEnumerable<Sucursal> sucursales);
    }
}