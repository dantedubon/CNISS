using System;
using CNISS.CommonDomain.Domain;

namespace CNISS.EnterpriseDomain.Domain.ValueObjects
{
    public class MotivoDespido : ValueObject<Guid>
    {
        public MotivoDespido(string descripcion)
        {
            this.descripcion = descripcion;
            Id = Guid.NewGuid();
        }

        public MotivoDespido()
        {
            Id = Guid.NewGuid();
        }

        public virtual string descripcion { get; set; }
        public virtual Auditoria auditoria { get; set; }
    }
}