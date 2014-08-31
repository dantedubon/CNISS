using System;
using CNISS.CommonDomain.Domain;

namespace CNISS.EnterpriseDomain.Domain.Entities
{
    public class LugarVisita:Entity<Guid>
    {
        public virtual Empresa Empresa { get;  protected set; }
        public virtual Sucursal Sucursal { get; protected set; }
        public virtual Auditoria Auditoria { get;  set; }

        public LugarVisita(Empresa empresa, Sucursal sucursal): this()
        {
            this.Empresa = empresa;
            this.Sucursal = sucursal;
        }

        protected LugarVisita()
        {
            Id = Guid.NewGuid();
        }
    }
}