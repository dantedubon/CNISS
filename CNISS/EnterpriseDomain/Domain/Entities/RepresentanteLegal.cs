using System;
using CNISS.CommonDomain.Domain;
using CNISS.EnterpriseDomain.Domain.ValueObjects;

namespace CNISS.EnterpriseDomain.Domain.Entities
{
    public class RepresentanteLegal:Entity<Identidad>
    {
        public virtual string nombre { get; protected set; }

        protected RepresentanteLegal()
        {
            
        }

        public RepresentanteLegal(Identidad identidad,  string nombre)
        {
           
            if (identidad == null) throw new ArgumentException("Identidad no puede ser nula");
            if (string.IsNullOrEmpty(nombre))
                throw new ArgumentException("Nombre de Representante no puede ser nulo");
            Id = identidad;
            this.nombre = nombre;
        }
    }

    public class RepresentanteLegalNull:RepresentanteLegal
    {
        public virtual string nombre { get { return string.Empty; }  }
        public virtual Identidad Id { get { return new IdentidadNull(); } }
    }
}