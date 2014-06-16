using System;
using CNISS.CommonDomain.Domain;

namespace CNISS.EnterpriseDomain.Domain.ValueObjects
{
    public class Direccion:ValueObject<Guid>
    {
        public virtual Municipio municipio { get; protected set; }
        public virtual string referenciaDireccion { get; protected set; }
        public virtual Departamento departamento { get; protected set; }
       

        protected Direccion()
        {
            Id = Guid.NewGuid();
        }

        public Direccion( Departamento departamento, Municipio municipio, string referenciaDireccion):this()
        {
            if (departamento == null) throw new ArgumentNullException("departamento");

            if (municipio == null) throw new ArgumentNullException("El municipio no puede ser nulo");
            if (string.IsNullOrEmpty(referenciaDireccion)) throw new ArgumentException("Referencia no puede ser nula");

            this.departamento = departamento;
            this.municipio = municipio;
            this.referenciaDireccion = referenciaDireccion;
        }
    }
}