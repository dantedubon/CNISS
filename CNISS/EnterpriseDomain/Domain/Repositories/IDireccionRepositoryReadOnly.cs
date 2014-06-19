using System;
using CNISS.CommonDomain.Domain;
using CNISS.EnterpriseDomain.Domain.ValueObjects;

namespace CNISS.EnterpriseDomain.Domain.Repositories
{
    public interface IDireccionRepositoryReadOnly:IRepositoryReadOnly<Direccion,Guid>
    {
    }
}