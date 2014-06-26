using System;
using CNISS.CommonDomain.Domain;

namespace CNISS.EnterpriseDomain.Domain.ValueObjects
{
    public class TipoEmpleo:ValueObject<Guid>
    {
        public virtual string descripcion { get; protected set; }

    }
}