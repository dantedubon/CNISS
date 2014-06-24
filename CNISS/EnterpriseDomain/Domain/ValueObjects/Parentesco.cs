using System;
using CNISS.CommonDomain.Domain;

namespace CNISS.EnterpriseDomain.Domain.ValueObjects
{
    public class Parentesco : ValueObject<Guid>
    {
        public virtual string descripcion { get; protected set; }

        public Parentesco(string descripcion):this()
        {
            this.descripcion = descripcion;
        }

        protected Parentesco()
        {
            Id = Guid.NewGuid();
        }
    }
}