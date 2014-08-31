using System;
using CNISS.CommonDomain.Domain;
using CNISS.EnterpriseDomain.Domain.ValueObjects;

namespace CNISS.EnterpriseDomain.Domain.Entities
{
    public class Dependiente:Entity<Identidad>
    {
        public virtual Nombre Nombre { get; protected set; }
        public virtual Parentesco Parentesco { get; protected set; }
        public virtual DateTime FechaNacimiento { get; protected set; }
      
        public virtual Guid idGuid { get; set; }

        public virtual Auditoria auditoria { get; set; }

        public Dependiente(Identidad identidad, Nombre nombre, Parentesco parentesco, DateTime fechaNacimiento)
        {
            this.Id = identidad;
            this.Nombre = nombre;
            this.Parentesco = parentesco;
            this.FechaNacimiento = fechaNacimiento;
            idGuid = Guid.NewGuid();
        }

        protected Dependiente()
        {
            idGuid = Guid.NewGuid();
        }
    }
}