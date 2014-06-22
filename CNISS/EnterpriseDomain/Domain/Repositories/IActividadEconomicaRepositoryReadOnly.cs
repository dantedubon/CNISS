using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using CNISS.CommonDomain.Domain;
using CNISS.EnterpriseDomain.Domain.ValueObjects;

namespace CNISS.EnterpriseDomain.Domain.Repositories
{
    public interface IActividadEconomicaRepositoryReadOnly : IRepositoryReadOnly<ActividadEconomica, Guid>
    {
         bool existsAll(IEnumerable<ActividadEconomica> actividades);
    }
}