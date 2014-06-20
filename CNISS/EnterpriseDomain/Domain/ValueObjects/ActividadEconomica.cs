using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CNISS.CommonDomain.Domain;

namespace CNISS.EnterpriseDomain.Domain.ValueObjects
{
    public class ActividadEconomica:ValueObject<Guid>
    {
        public virtual string descripcion { get; set; }
    }
}