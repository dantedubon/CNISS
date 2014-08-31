using System;
using CNISS.CommonDomain.Domain;

namespace CNISS.EnterpriseDomain.Domain.ValueObjects
{
    public class TipoEmpleo:ValueObject<Guid>
    {
        public virtual string Descripcion { get; protected set; }
        public virtual Auditoria Auditoria { get; set; }

        protected TipoEmpleo()
        {
            Id = Guid.NewGuid();
        }

        public TipoEmpleo(string descripcion)
        {
            Id = Guid.NewGuid();
            this.Descripcion = descripcion;

        }
    }
}