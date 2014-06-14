using System;
using CNISS.CommonDomain.Domain;

namespace CNISS.EnterpriseDomain.Domain.ValueObjects
{
    public class Direccion:ValueObject<Guid>
    {
        public virtual Municipio municipio { get; protected set; }
        public virtual string referenciaDireccion { get; protected set; }

        protected Direccion()
        {
            Id = Guid.NewGuid();
        }

        public Direccion(Municipio municipio, string referenciaDireccion):this()
        {
            if (municipio == null) throw new ArgumentNullException("El municipio no puede ser nulo");
            if (string.IsNullOrEmpty(referenciaDireccion)) throw new ArgumentException("Referencia no puede ser nula");
            this.municipio = municipio;
            this.referenciaDireccion = referenciaDireccion;
        }
    }
}