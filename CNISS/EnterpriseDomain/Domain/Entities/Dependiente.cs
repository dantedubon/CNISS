using System;
using CNISS.CommonDomain.Domain;
using CNISS.EnterpriseDomain.Domain.ValueObjects;

namespace CNISS.EnterpriseDomain.Domain.Entities
{
    public class Dependiente:Entity<Identidad>
    {
        public virtual Nombre nombre { get; protected set; }
        public virtual Parentesco parentesco { get; protected set; }
        public virtual int edad { get; protected set; }
      
        public virtual Guid idGuid { get; set; }

        public Dependiente(Identidad identidad, Nombre nombre, Parentesco parentesco, int edad)
        {
            this.Id = identidad;
            this.nombre = nombre;
            this.parentesco = parentesco;
            this.edad = edad;
            idGuid = Guid.NewGuid();
        }

        protected Dependiente()
        {
            idGuid = Guid.NewGuid();
        }
    }
}