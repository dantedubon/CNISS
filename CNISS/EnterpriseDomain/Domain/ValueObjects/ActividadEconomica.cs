using System;
using CNISS.CommonDomain.Domain;

namespace CNISS.EnterpriseDomain.Domain.ValueObjects
{
    public class ActividadEconomica:ValueObject<Guid>
    {
        public ActividadEconomica( string descripcion)
        {
            this.Descripcion = descripcion;
            Id = Guid.NewGuid();
        }

        public ActividadEconomica()
        {
            Id = Guid.NewGuid();
        }

        public virtual string Descripcion { get; set; }
        public virtual Auditoria Auditoria { get; set; }
    }
}