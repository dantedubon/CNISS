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
        public virtual Nombre Nombre { get; protected set; }
        public virtual DateTime FechaNacimiento { get; protected set; }
        public virtual IList<Dependiente> Dependientes { get; protected set; }
        public virtual Auditoria Auditoria { get; set; }
        public virtual string TelefonoFijo { get; set; }
        public virtual string  TelefonoCelular { get; set; }
        public virtual ContentFile FotografiaBeneficiario { get; set; }
        public virtual Direccion Direccion { get; set; }

        public Beneficiario(Identidad identidad,Nombre nombre, DateTime fechaNacimiento)
        {
            this.Id = identidad;
            this.Nombre = nombre;
            this.FechaNacimiento = fechaNacimiento;
            Dependientes = new List<Dependiente>();
        }

        public Beneficiario()
        {
            Dependientes = new List<Dependiente>();
        }

        public virtual void addDependiente(Dependiente dependiente)
        {
            Dependientes.Add(dependiente);
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
            return (Nombre != null ? Id.GetHashCode() : 0);
        }
    }
}