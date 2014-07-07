using System;
using CNISS.CommonDomain.Domain;

namespace CNISS.EnterpriseDomain.Domain.Entities
{
    public class LugarVisita:Entity<Guid>
    {
        public virtual Empresa empresa { get;  protected set; }
        public virtual Sucursal sucursal { get; protected set; }
        public virtual Auditoria auditoria { get; protected set; }
    }
}