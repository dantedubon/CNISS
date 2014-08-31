using System;
using CNISS.CommonDomain.Domain;

namespace CNISS.EnterpriseDomain.Domain.ValueObjects
{
    public class Parentesco : ValueObject<Guid>
    {
        public virtual string Descripcion { get; protected set; }
        public virtual Auditoria Auditoria { get; set; }

        public Parentesco(string descripcion):this()
        {
            this.Descripcion = descripcion;
        }

        protected Parentesco()
        {
            Id = Guid.NewGuid();
        }
    }
}