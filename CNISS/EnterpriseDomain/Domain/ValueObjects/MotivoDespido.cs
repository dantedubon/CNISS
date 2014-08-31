using System;
using CNISS.CommonDomain.Domain;

namespace CNISS.EnterpriseDomain.Domain.ValueObjects
{
    public class MotivoDespido : ValueObject<Guid>
    {
        public MotivoDespido(string descripcion):this()
        {
            this.Descripcion = descripcion;
          
        }

        public MotivoDespido()
        {
            Id = Guid.NewGuid();
        }

        public virtual string Descripcion { get; set; }
        public virtual Auditoria Auditoria { get; set; }
    }
}