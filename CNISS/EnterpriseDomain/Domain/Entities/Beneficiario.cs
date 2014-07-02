using System;
using System.Collections.Generic;
using System.Linq;
using CNISS.CommonDomain.Domain;
using CNISS.EnterpriseDomain.Domain.ValueObjects;
using NHibernate.Linq;

namespace CNISS.EnterpriseDomain.Domain.Entities
{
    public class Beneficiario:Entity<Identidad>, IEquatable<Beneficiario>
    {
        public virtual Nombre nombre { get; protected set; }
        public virtual DateTime fechaNacimiento { get; protected set; }
        public virtual IList<Dependiente> dependientes { get; protected set; }
        public virtual Auditoria auditoria { get; set; }

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

        public virtual bool Equals(Beneficiario other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Equals(Id, other.Id);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Beneficiario) obj);
        }

        public override int GetHashCode()
        {
            return (nombre != null ? Id.GetHashCode() : 0);
        }
    }
}