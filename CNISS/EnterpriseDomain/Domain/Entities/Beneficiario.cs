using System;
using System.Collections.Generic;
using System.Linq;
using CNISS.CommonDomain.Domain;
using CNISS.EnterpriseDomain.Domain.ValueObjects;
using NHibernate.Linq;

namespace CNISS.EnterpriseDomain.Domain.Entities
{
    public class Beneficiario:Entity<Identidad>
    {
        public virtual Nombre nombre { get; protected set; }
        public virtual DateTime fechaNacimiento { get; protected set; }
        public virtual IList<Dependiente> dependientes { get; protected set; }

        public Beneficiario(Identidad identidad,Nombre nombre, DateTime fechaNacimiento)
        {
            this.Id = identidad;
            this.nombre = nombre;
            this.fechaNacimiento = fechaNacimiento;
            dependientes = new List<Dependiente>();
        }

        public Beneficiario()
        {
            dependientes = new List<Dependiente>();
        }

        public virtual void addDependiente(Dependiente dependiente)
        {
            dependientes.Add(dependiente);
        }
    }
}