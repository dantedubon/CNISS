using System;
using CNISS.CommonDomain.Domain;

namespace CNISS.EnterpriseDomain.Domain.ValueObjects
{
    public class Direccion:ValueObject<Guid>
    {
        public virtual Municipio Municipio { get;  set; }
        public virtual string ReferenciaDireccion { get;  set; }
        public virtual Departamento Departamento { get;  set; }
       

        protected Direccion()
        {
            
        }

        public Direccion( Departamento departamento, Municipio municipio, string referenciaDireccion):this()
        {
            if (departamento == null) throw new ArgumentNullException("departamento");

            if (municipio == null) throw new ArgumentNullException("El municipio no puede ser nulo");
            if (string.IsNullOrEmpty(referenciaDireccion)) throw new ArgumentException("Referencia no puede ser nula");
            Id = Guid.NewGuid();
            this.Departamento = departamento;
            this.Municipio = municipio;
            this.ReferenciaDireccion = referenciaDireccion;
        }
    }

    public class DireccionNull:Direccion
    {
        public virtual Municipio municipio { get{return new MunicipioNull();}  }
        public virtual string referenciaDireccion { get { return string.Empty; }  }
        public  virtual Departamento departamento { get{return new DepartamentoNull();}  }
        public virtual Guid Id { get { return Guid.Empty; }  }
       
    }

}