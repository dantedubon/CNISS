using System;
using CNISS.CommonDomain.Domain;

namespace CNISS.EnterpriseDomain.Domain.ValueObjects
{
    public class ActividadEconomica:ValueObject<Guid>
    {
        public ActividadEconomica( string descripcion)
        {
            this.descripcion = descripcion;
            Id = Guid.NewGuid();
        }

        public ActividadEconomica()
        {
            Id = Guid.NewGuid();
        }

        public virtual string descripcion { get; set; }
        public virtual Auditoria auditoria { get; set; }
    }
}