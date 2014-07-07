using System;
using CNISS.CommonDomain.Domain;

namespace CNISS.EnterpriseDomain.Domain.Entities
{
    public class LugarVisita:Entity<Guid>
    {
        public virtual Empresa empresa { get; set; }
        public virtual Sucursal sucursal { get; set; }
        public Auditoria auditoria { get; set; }
    }
}