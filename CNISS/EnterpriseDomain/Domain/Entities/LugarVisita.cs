using System;
using CNISS.CommonDomain.Domain;

namespace CNISS.EnterpriseDomain.Domain.Entities
{
    public class LugarVisita:Entity<Guid>
    {
        public virtual Empresa empresa { get;  protected set; }
        public virtual Sucursal sucursal { get; protected set; }
        public virtual Auditoria auditoria { get;  set; }

        public LugarVisita(Empresa empresa, Sucursal sucursal): this()
        {
            this.empresa = empresa;
            this.sucursal = sucursal;
        }

        protected LugarVisita()
        {
            Id = Guid.NewGuid();
        }
    }
}