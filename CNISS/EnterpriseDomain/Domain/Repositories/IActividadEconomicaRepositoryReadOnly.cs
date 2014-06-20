using System;
using CNISS.CommonDomain.Domain;
using CNISS.EnterpriseDomain.Domain.ValueObjects;

namespace CNISS.EnterpriseDomain.Domain.Repositories
{
    public interface IActividadEconomicaRepositoryReadOnly : IRepositoryReadOnly<ActividadEconomica, Guid>
    {
    }
}