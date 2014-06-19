using System;
using CNISS.CommonDomain.Domain;

namespace CNISS.EnterpriseDomain.Domain.ValueObjects
{
    public class Direccion:ValueObject<Guid>
    {
        public virtual Municipio municipio { get;  set; }
        public virtual string referenciaDireccion { get;  set; }
        public virtual Departamento departamento { get;  set; }
       

        protected Direccion()
        {
            
        }

        public Direccion( Departamento departamento, Municipio municipio, string referenciaDireccion):this()
        {
            if (departamento == null) throw new ArgumentNullException("departamento");

            if (municipio == null) throw new ArgumentNullException("El municipio no puede ser nulo");
            if (string.IsNullOrEmpty(referenciaDireccion)) throw new ArgumentException("Referencia no puede ser nula");
            Id = Guid.NewGuid();
            this.departamento = departamento;
            this.municipio = municipio;
            this.referenciaDireccion = referenciaDireccion;
        }
    }
}