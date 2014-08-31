using System;
using CNISS.CommonDomain.Domain;
using CNISS.EnterpriseDomain.Domain.ValueObjects;

namespace CNISS.EnterpriseDomain.Domain.Entities
{
    public class Sucursal:Entity<Guid>, IEquatable<Sucursal>
    {
        public Sucursal(string nombre, Direccion direccion, FirmaAutorizada firmaAutorizada):this()
        {
            
            this.Nombre = nombre;
            this.Direccion = direccion;
            this.Firma = firmaAutorizada;
        }

        protected Sucursal()
        {
            Id = Guid.NewGuid();
            
        }
        public virtual string Nombre { get; set; }
        public virtual Direccion Direccion { get; set; }
        public virtual FirmaAutorizada Firma { get; set; }
        public virtual Auditoria Auditoria { get; set; }

        public virtual bool Equals(Sucursal other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return string.Equals(Id, other.Id);
        }

        public  override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Sucursal) obj);
        }

        public override int GetHashCode()
        {
            return (Nombre != null ? Id.GetHashCode() : 0);
        }
    }
}