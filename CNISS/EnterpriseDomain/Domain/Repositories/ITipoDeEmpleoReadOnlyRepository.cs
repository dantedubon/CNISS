using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CNISS.CommonDomain.Domain;
using CNISS.EnterpriseDomain.Domain.ValueObjects;

namespace CNISS.EnterpriseDomain.Domain.Repositories
{
    public interface ITipoDeEmpleoReadOnlyRepository : IRepositoryReadOnly<TipoEmpleo, Guid>
    {
    }
}
